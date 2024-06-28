using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using EMS.DB.Models;

namespace EMS.DAL.Mapper;

public class EmployeeMapper : IEmployeeMapper
{
    public EmployeeDto? ToEmployeeDto(Employee employee)
    {
        if (employee == null)
        {
            return null;
        }

        return new EmployeeDto
        {
            Id = employee.Id,
            Status = employee.Status,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            UID = employee.UID,
            Dob = employee.Dob,
            Email = employee.Email,
            MobileNumber = employee.MobileNumber,
            JoiningDate = employee.JoiningDate,
            LocationId = employee.LocationId,
            LocationName = employee.Location?.Name,
            RoleId = employee.RoleId,
            RoleName = employee.Role?.Name,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name,
            ProjectId = employee.ProjectId,
            ProjectName = employee.Project?.Name,
            ManagerId = employee.ManagerId,
            IsManager = employee.IsManager,
            ManagerName = employee.Manager != null ? $"{employee.Manager.FirstName} {employee.Manager.LastName}" : null,
            ModeStatusId = employee.ModeStatusId,
            ModeStatusName = employee.Mode!.Name,
            ProfileImagePath = employee.ProfileImagePath,
        };
    }

    public List<EmployeeDto> ToEmployeeDto(List<Employee> employees)
    {
        return employees.Select(ToEmployeeDto).ToList()!;
    }

    public Employee ToEmployeeModel(EmployeeDto EmployeeDto)
    {
        return new Employee
        {
            UID = EmployeeDto.UID,
            FirstName = EmployeeDto.FirstName,
            LastName = EmployeeDto.LastName,
            Password = EmployeeDto.Password!,
            Dob = EmployeeDto.Dob,
            Email = EmployeeDto.Email,
            MobileNumber = EmployeeDto.MobileNumber,
            JoiningDate = EmployeeDto.JoiningDate,
            LocationId = EmployeeDto.LocationId,
            RoleId = EmployeeDto.RoleId,
            DepartmentId = EmployeeDto.DepartmentId,
            ProjectId = EmployeeDto.ProjectId,
            ManagerId = EmployeeDto.ManagerId,
            ProfileImagePath = EmployeeDto.ProfileImagePath,
        };
    }
}