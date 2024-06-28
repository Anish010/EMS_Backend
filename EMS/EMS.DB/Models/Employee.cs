using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DB.Models;

public class Employee
{
    [Key]
    [Column(TypeName = "int")]
    public int Id { get; set; }

    
    [MaxLength(10)]
    [Column(TypeName = "varchar(10)")]
    [Index(IsUnique = true)]
    public string UID { get; set; }

    
    [MaxLength(16)]
    [Column(TypeName = "varchar(16)")]
    public string Password { get; set; } = string.Empty;

    public bool Status { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string? FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string? LastName { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? Dob { get; set; }

    [MaxLength(255)]
    [Column(TypeName = "varchar(255)")]
    public string? Email { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string? MobileNumber { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? JoiningDate { get; set; }
    public int? LocationId { get; set; }
    public int? RoleId { get; set; }
    public int? DepartmentId { get; set; }
    public int? ManagerId { get; set; }
    public bool IsManager { get; set; } = false;
    public int? ProjectId { get; set; }

    public int? ModeStatusId { get; set; } = 1;

    public string? ProfileImagePath { get; set; } = string.Empty;


    // Navigation properties
    [ForeignKey("LocationId")]
    public Location? Location { get; set; }

    [ForeignKey("RoleId")]
    public Role? Role { get; set; }

    [ForeignKey("DepartmentId")]
    public Department? Department { get; set; }

    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }

    [ForeignKey("ManagerId")]
    public Employee? Manager { get; set; }

    [ForeignKey("ModeStatusId")]
    public Mode? Mode { get; set; }



    public override string ToString()
    {
        return $"Employee ID: {UID}\n" +
               $"Name: {FirstName} {LastName}\n" +
               $"Date of Birth: {(Dob.HasValue ? Dob.Value.ToShortDateString() : string.Empty)}\n" +
               $"Email: {Email}\n" +
               $"Mobile Number: {MobileNumber}\n" +
               $"Joining Date: {(JoiningDate.HasValue ? JoiningDate.Value.ToShortDateString() : string.Empty)}\n" +
               $"LocationId: {LocationId}\n" +
               $"RoleId: {RoleId}\n" +
               $"DepartmentId: {DepartmentId}\n";
    }
}
