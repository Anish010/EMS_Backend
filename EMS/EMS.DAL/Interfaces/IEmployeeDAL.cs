using EMS.DAL.DTO;
using EMS.DAL.Models;

namespace EMS.DAL.Interfaces;

public interface IEmployeeDAL
{
    public Task<int> InsertAsync(EmployeeDto employee);
    public Task<List<EmployeeDto>> RetrieveAllAsync(EmployeeFilters? filters);
    public Task<EmployeeDto?> RetrieveByIdAsync(int? id);
    public Task<List<EmployeeDto>?> RetrieveByDepartmentIdAsync(int? id);
    public Task<List<DepartmentEmployeeDto>> RetrieveGroupedByDepartmentsAsync();
    public Task<int> UpdateAsync(int id, UpdateEmployeeDto employee);
    public Task<int> DeleteAsync(IEnumerable<int> ids);
    public Task<List<EmployeeDto>?> FilterAsync(EmployeeFilters? filters);
    public Task<int> CountAsync();
    public Task<List<EmployeeDto>?> RetrieveByRoleIdAsync(int? id);
    public Task<string?> UpdateEmployeeModeAsync(int? Id, int? modeStatusId);
}

