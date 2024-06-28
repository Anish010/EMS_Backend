using EMS.API.Helpers;
using EMS.BAL.Interfaces;
using EMS.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EMS.DAL.Models;
using Newtonsoft.Json;
using EMS.DB.Models;

namespace EMS.API.Controllers;

[Route("/Api/Employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeBAL _employeeBal;
    private readonly Serilog.ILogger _logger;
    private readonly string ImageUploadDirectory;
    private readonly string folderPath;
    private readonly ProfileImagesHelper _profileImagesHelper;

    public EmployeeController(Serilog.ILogger logger, IEmployeeBAL employeeBAL, IConfiguration configuration, ProfileImagesHelper profileImagesHelper)
    {
        _logger = logger;
        _profileImagesHelper = profileImagesHelper;
        _employeeBal = employeeBAL;
        folderPath = Path.Combine(configuration.GetValue<string>("AppSettings:ImageUploadDirectory") ?? "StaticFiles", "Images");
        ImageUploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
        if (!Directory.Exists(ImageUploadDirectory))
        {
            Directory.CreateDirectory(ImageUploadDirectory);
        }
    }

    [HttpPost, DisableRequestSizeLimit]
    [Authorize]
    public async Task<IActionResult> AddEmployee()
    {
        try
        {

            var form = await Request.ReadFormAsync();
            var employeeJson = form["employeeData"];

            if (string.IsNullOrEmpty(employeeJson))
            {
                return BadRequest("Employee data is missing.");
            }

            var employee = JsonConvert.DeserializeObject<EmployeeDto>(employeeJson!);

            if (employee == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the uploaded file
            var profileImage = form.Files.GetFile("profileImage");


            var relativePath = await _profileImagesHelper.HandleProfileImageUpload(profileImage, employee.Id);
            if (relativePath != null)
            {
                employee.ProfileImagePath = relativePath;
            }

            var result = await _employeeBal.AddEmployeeAsync(employee);
            return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), 1);
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.FAILED_TO_ADD_EMPLOYEE.ToString());
        }
    }

    [HttpPut("{id}"), DisableRequestSizeLimit]
    [Authorize]
    public async Task<IActionResult> UpdateEmployee(int id)
    {
        try
        {
            var form = await Request.ReadFormAsync();
            var employeeJson = form["employeeData"];
            bool isModified = true;

            if (string.IsNullOrEmpty(employeeJson))
            {
                return BadRequest("Employee data is missing.");
            }

            var employee = JsonConvert.DeserializeObject<UpdateEmployeeDto>(employeeJson!);

            if (employee == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileImage = form.Files.GetFile("profileImage");

            if (profileImage != null && profileImage.Length > 0)
            {
                // Delete previous image if exists
                var existingImagePath = Path.Combine(this.ImageUploadDirectory, employee.ProfileImagePath!);
                if (System.IO.File.Exists(existingImagePath))
                {
                    isModified = true;
                    System.IO.File.Delete(existingImagePath);
                }

                var relativePath = await _profileImagesHelper.HandleProfileImageUpload(profileImage, id);
                employee.ProfileImagePath = relativePath;
            }

            var result = await _employeeBal.UpdateEmployeeAsync(id, employee);

            if (isModified || result > 0)
            {
                return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), result);
            }

            return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEE_NOT_FOUND.ToString());


        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.FAILED_TO_UPDATE_EMPLOYEE.ToString());
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteEmployee([FromQuery] IEnumerable<int> ids)
    {
        try
        {
            var result = await _employeeBal.DeleteEmployeeAsync(ids);
            if (result == 0)
            {
                return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEE_NOT_FOUND.ToString());
            }
            return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), result);
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.FAILED_TO_DELETE_EMPLOYEE.ToString());
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilters? filters, [FromQuery] int? modeStatusId)
    {
        try
        {

            if (filters != null)
            {
                if (modeStatusId != null)
                {
                    if (filters.EmployeeId == null)
                    {
                        return ResponseHelper.WrapResponse(400, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEE_ID_IS_REQUIRED.ToString());
                    }
                    var currentEmployeeMode = await _employeeBal.UpdateEmployeeModeAsync(filters.EmployeeId, modeStatusId);
                    if (currentEmployeeMode == null)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEE_NOT_FOUND.ToString());
                    }
                    string message = $"Employee is in '{currentEmployeeMode}' mode.";
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), message);
                }
                if (filters.RoleId != null)
                {
                    var employeesByRole = await _employeeBal.GetEmployeeByRoleAsync(filters.RoleId);
                    if (employeesByRole == null)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEES_NOT_FOUND.ToString());
                    }
                    await _profileImagesHelper.LoadProfileImages(employeesByRole);
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), employeesByRole);
                }
                if (filters.DepartmentId != null)
                {
                    var employeesByDept = await _employeeBal.GetEmployeeByDepartmentIdAsync(filters.DepartmentId);
                    if (employeesByDept == null)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEES_NOT_FOUND.ToString());
                    }
                    await _profileImagesHelper.LoadProfileImages(employeesByDept);
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), employeesByDept);
                }
                if (filters.EmployeeId != null)
                {
                    var employee = await _employeeBal.GetEmployeeByIdAsync(filters.EmployeeId);
                    if (employee == null)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEE_NOT_FOUND.ToString());
                    }
                    await _profileImagesHelper.LoadProfileImages(employee);
                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), employee);
                }
                if (!string.IsNullOrWhiteSpace(filters.GroupBy) && filters.GroupBy.Equals("department", StringComparison.OrdinalIgnoreCase))
                {
                    var groupedEmployees = await _employeeBal.GetEmployeesGroupedByDepartmentsAsync();
                    if (groupedEmployees == null)
                    {
                        return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEES_NOT_FOUND.ToString());
                    }

                    return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), groupedEmployees);
                }


                var employees = await _employeeBal.FilterEmployeesAsync(filters);
                if (employees == null)
                {
                    return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.FAILED_TO_FILTER_EMPLOYEES.ToString());
                }

                await _profileImagesHelper.LoadProfileImages(employees);

                return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), employees);

            }
            else
            {
                var employees = await _employeeBal.GetAllAsync(filters!);
                if (employees == null)
                {
                    return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.EMPLOYEES_NOT_FOUND.ToString());
                }
                await _profileImagesHelper.LoadProfileImages(employees);
                return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), employees);
            }
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }
    }

    [HttpGet("Count")]
    [Authorize]
    public async Task<IActionResult> CountEmployees()
    {
        try
        {
            var count = await _employeeBal.CountEmployeesAsync();
            return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), count);
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.FAILED_TO_COUNT_EMPLOYEES.ToString());
        }
    }

    private AuthenticateResponse? GetCurrentEmployee()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var claims = identity.Claims;
            return new AuthenticateResponse
            {
                Id = int.TryParse(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int id) ? id : -1,
                FirstName = claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                LastName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                UID = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value
            };
        }
        return null;
    }

}