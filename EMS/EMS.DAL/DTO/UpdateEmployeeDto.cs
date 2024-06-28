namespace EMS.DAL.DTO;

public class UpdateEmployeeDto
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public DateTime? Dob { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; } = string.Empty;
    public DateTime? JoiningDate { get; set; }
    public int? LocationId { get; set; }
    public int? RoleId { get; set; }
    public int? DepartmentId { get; set; }
    public int? ManagerId { get; set; }
    public bool IsManager { get; set; } = false;
    public int? ProjectId { get; set; }
    public int? ModeStatusId { get; set; }
    public string? ProfileImagePath { get; set; } = string.Empty;


    public override string ToString()
    {
        return 
               $"Name: {FirstName} {LastName}\n" +
               $"Date of Birth: {(Dob.HasValue ? Dob.Value.ToShortDateString() : string.Empty)}\n" +
               $"Email: {Email}\n" +
               $"Mobile Number: {MobileNumber}\n" +
               $"Joining Date: {(JoiningDate.HasValue ? JoiningDate.Value.ToShortDateString() : string.Empty)}\n";
    }
}