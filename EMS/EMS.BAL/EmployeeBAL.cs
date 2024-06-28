using EMS.BAL.Interfaces;
using EMS.DAL.Models;
using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using EMS.DB.Models;
using Serilog;

namespace EMS.BAL;

public class EmployeeBAL : IEmployeeBAL
{
    private readonly IEmployeeDAL _employeeDal;
    private readonly ILogger _logger;

    public EmployeeBAL(ILogger logger, IEmployeeDAL employeeDal)
    {
        _employeeDal = employeeDal;
        _logger = logger;
    }

    public async Task<List<EmployeeDto>?> GetAllAsync(EmployeeFilters? filters)
    {
        try
        {
            var employees = await _employeeDal.RetrieveAllAsync(filters) ?? [];
            return employees;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error retrieving employee details: {ex.Message}");
            throw;
        }
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int? id)
    {
        try
        {
            var employees = await _employeeDal.RetrieveByIdAsync(id);
            return employees!;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error retrieving employee details: {ex.Message}");
            throw;
        }
    }

    public async Task<int> AddEmployeeAsync(EmployeeDto employee)
    {
        try
        {
            return await _employeeDal.InsertAsync(employee);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while inserting employee: {ex.Message}");
            throw;
        }

    }

    public async Task<int> DeleteEmployeeAsync(IEnumerable<int> ids)
    {
        try
        {
            return await _employeeDal.DeleteAsync(ids);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error deleting employees: {ex.Message}");
            throw;
        }
    }


    public async Task<int> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee)
    {
        try
        {
            return await _employeeDal.UpdateAsync(id, employee);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while updating employee: {ex.Message}");
            throw;
        }
    }

    public async Task<List<EmployeeDto>?> FilterEmployeesAsync(EmployeeFilters? filters)
    {
        try
        {
            List<EmployeeDto> filteredEmployees;
            filteredEmployees = await _employeeDal.FilterAsync(filters) ?? [];
            if (filteredEmployees != null && filteredEmployees.Count > 0)
            {
                return filteredEmployees;
            }
            return [];
        }
        catch (Exception ex)
        {
            _logger.Error($"Error Filtering: {ex.Message}");
            throw;
        }
    }

    public async Task<int> CountEmployeesAsync()
    {
        try
        {
            return await _employeeDal.CountAsync();
        }
        catch (Exception ex)
        {
            _logger.Error($"Error counting employees: {ex.Message}");
            throw;
        }
    }

    public async Task<List<EmployeeDto>?> GetEmployeeByDepartmentIdAsync(int? id)
    {
        try
        {

            List<EmployeeDto> employees =await _employeeDal.RetrieveByDepartmentIdAsync(id) ?? [];
            return employees;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error retrieving employee details: {ex.Message}");
            throw;
        }
    }

    public async Task<List<EmployeeDto>?> GetEmployeeByRoleAsync(int? id)
    {
        try
        {
            List<EmployeeDto> employees = await _employeeDal.RetrieveByRoleIdAsync(id) ?? [];
            return employees;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while retrieving employees by role: {ex.Message}");
            throw;
        }
    }

    public async Task<string?> UpdateEmployeeModeAsync(int? id, int? modeStatusId)
    {
        try
        {
            string? updatedModeStatusId = await _employeeDal.UpdateEmployeeModeAsync(id, modeStatusId);
            return updatedModeStatusId;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occured while updating mode status :  {ex.Message}");
            throw;
        }
    }

    public async Task<List<DepartmentEmployeeDto>> GetEmployeesGroupedByDepartmentsAsync()
    {
        try
        {
            List<DepartmentEmployeeDto> employees = await _employeeDal.RetrieveGroupedByDepartmentsAsync() ?? new List<DepartmentEmployeeDto>();
            return employees;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while retrieving employees grouped by departments: {ex.Message}");
            throw;
        }
    }
}

