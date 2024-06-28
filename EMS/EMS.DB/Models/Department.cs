using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DB.Models;

public class Department
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public required string Name { get; set; }

    public virtual ICollection<Employee> Employee { get; set; } = [];
}