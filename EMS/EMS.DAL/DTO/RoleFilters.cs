namespace EMS.DAL.DTO;

public class RoleFilters
{
    public List<int>? Departments { get; set; }
    public List<int>? Locations { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = int.MaxValue;
    public int? RoleId { get; set; } = null;
    public int? DepartmentId { get; set; } = null;
    public int? LocationId { get; set; } = null;
    public string? Search { get; set; }
}
