using EMS.DAL.DTO;
using EMS.DAL.Interfaces;
using EMS.DB.Models;

namespace EMS.DAL.Mapper;

public class RoleMapper : IRoleMapper
{
    public List<RoleDto> ToRoleDto(IEnumerable<Role> roles)
    {
        return roles.Select(ToRoleDto).ToList()!;
    }

    public RoleDto? ToRoleDto(Role role)
    {
        if(role == null)
        {
            return null;
        }
        string? departmentName = role.Department != null ? role.Department.Name : null;
        string? locationName = role.Location != null ? role.Location.Name : null;

        var employees = role.Employee.Select(e => new EmployeeDto
        {
            Id = e.Id,
            UID = e.UID,
            Email = e.Email,
            FirstName = e.FirstName,
            LastName = e.LastName,
        }).ToList();

        return new RoleDto
        {
            RoleId = role.Id,
            RoleName = role.Name,
            DepartmentId = role.DepartmentId,
            DepartmentName = departmentName,
            LocationId = role.LocationId,
            LocationName = locationName,
            Description = role.Description,
            EmployeesCount = employees.Count
        };
    }

    public Role ToRoleModel(RoleDto RoleDto)
    {
        return new Role
        {
            Name = RoleDto.RoleName!,
            DepartmentId = RoleDto.DepartmentId,
            LocationId = RoleDto.LocationId,
            Description = RoleDto.Description,
        };
    }
}
