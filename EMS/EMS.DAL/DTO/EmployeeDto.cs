using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EMS.DAL.DTO;

public class EmployeeDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "UID is required!")]
    public string? UID { get; set; }
    public bool Status { get; set; } = false;
    [Required(ErrorMessage = "Password is required!")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "First Name is required!")]
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public DateTime? Dob { get; set; }
    [Required(ErrorMessage = "Email is required!")]
    public string? Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; } = string.Empty;
    public DateTime? JoiningDate { get; set; }
    public int? LocationId { get; set; }
    public int? RoleId { get; set; }
    public int? DepartmentId { get; set; }
    public int? ManagerId { get; set; }
    public bool IsManager { get; set; } = false;
    public int? ProjectId { get; set; }
    public string? LocationName { get; set; } = string.Empty;
    public string? DepartmentName { get; set; } = string.Empty;
    public string? StatusName { get; set; } = string.Empty;
    public string? ManagerName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? RoleName { get; set; } = string.Empty;
    public int? ModeStatusId { get; set; }
    public string ModeStatusName { get; set; } = string.Empty;
    public string? ProfileImagePath { get; set; } = string.Empty;

    public byte[]? ProfileImageData { get; set; }
    public override string ToString()
    {
        return $"Employee ID: {UID}\n" +
               $"Name: {FirstName} {LastName}\n" +
               $"Date of Birth: {(Dob.HasValue ? Dob.Value.ToShortDateString() : string.Empty)}\n" +
               $"Email: {Email}\n" +
               $"Mobile Number: {MobileNumber}\n" +
               $"Joining Date: {(JoiningDate.HasValue ? JoiningDate.Value.ToShortDateString() : string.Empty)}\n" +
               $"Location: {LocationName}\n" +
               $"Role: {RoleName}\n" +
               $"Department: {DepartmentName}\n" +
               $"Assign Manager: {ManagerName}\n" +
               $"Assign Project: {ProjectName}\n" +
               $"Status: {StatusName}\n";
    }
}
