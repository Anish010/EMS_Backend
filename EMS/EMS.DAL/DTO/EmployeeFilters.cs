namespace EMS.DAL.DTO;

public class EmployeeFilters
{
    public List<char>? Alphabet { get; set; }
    public List<int>? Locations { get; set; }
    public List<int>? Departments { get; set; }
    public List<int>? Status { get; set; }
    public string? Search { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = int.MaxValue;
    public int? RoleId { get; set; } = null;
    public int? DepartmentId { get; set; } = null;
    public int? EmployeeId { get; set; } = null;
    public string? GroupBy { get; set; }

}
