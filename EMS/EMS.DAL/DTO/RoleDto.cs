namespace EMS.DAL.DTO;

public class RoleDto
{
    public int? RoleId { get; set; }
    public string? RoleName { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public int? LocationId {get; set; }
    public string? LocationName { get; set;}
    public string? Description { get; set; } = string.Empty;

    public int EmployeesCount { get; set; }
    public List<EmployeeDto> Employees { get; set; } = [];
}