using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DB.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    [Required]
    public required string Name { get; set; }

    [Required]
    public int? DepartmentId { get; set; }

    public int? LocationId { get; set; }

    // Navigation properties
    [ForeignKey("DepartmentId")]
    public Department? Department { get; set; }

    [ForeignKey("LocationId")]
    public Location? Location { get; set; }
    public string? Description { get; set; } = String.Empty;

    public virtual ICollection<Employee> Employee { get; set; } = [];
}