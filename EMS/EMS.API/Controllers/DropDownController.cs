using EMS.API.Helpers;
using EMS.BAL;
using EMS.BAL.Interfaces;
using EMS.DAL;
using EMS.DAL.Interfaces;
using EMS.DB.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers;

[Route("/Api/DropDown")]
[Authorize]
[ApiController]
public class DropDownController : ControllerBase
{
    private readonly IDropdownBAL _dropdownBal;
    private readonly Serilog.ILogger _logger;

    public DropDownController(Serilog.ILogger logger, IDropdownBAL dropdownBAL)
    {
        _logger = logger;
        _dropdownBal = dropdownBAL;
    }

    [HttpGet("Departments")]
    
    public async Task<IActionResult> GetDepartmentsAsync()
    {
        try
        {
            var departments = await _dropdownBal.GetDepartmentsAsync();
            if(departments == null || departments.Count == 0)
            {
                return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.DEPARTMENTS_NOT_FOUND.ToString());
            }
            return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), departments);;
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }
    }

    [HttpGet("Locations")]
    
    public async Task<IActionResult>GetLocationsAsync()
    {
        try
        {
            var locations = await _dropdownBal.GetLocationsAsync();
            if(locations == null || locations.Count == 0)
            {
                return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.LOCATIONS_NOT_FOUND.ToString());
            }
            return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), locations);;
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }
    }

    [HttpGet("Managers")]
    
    public async Task<IActionResult>GetManagersAsync()
    {
        try
        {
            var managers = await _dropdownBal.GetManagersAsync();
            if(managers == null || managers.Count == 0)
            {
                return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.MANAGERS_NOT_FOUND.ToString());
            }
            return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), managers);;
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }
    }

    [HttpGet("Projects")]
    
    public async Task<IActionResult> GetProjectsAsync()
    {
        try
        {
            var projects = await _dropdownBal.GetProjectsAsync();
            if(projects == null || projects.Count == 0)
            {
                return ResponseHelper.WrapResponse(404, StatusMessage.ERROR.ToString(), null, ErrorCodes.PROJECTS_NOT_FOUND.ToString());
            }
            return ResponseHelper.WrapResponse(200, StatusMessage.SUCCESS.ToString(), projects);;
        }
        catch (Exception)
        {
            return ResponseHelper.WrapResponse(500, StatusMessage.FAILURE.ToString(), null, ErrorCodes.INTERNAL_SERVER_ERROR.ToString());
        }
    }
}
