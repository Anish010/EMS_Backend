using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMS.DB.Models;

public class Mode
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public required string Name { get; set; } = "Active";

    public virtual ICollection<Employee> Employee { get; set; } = [];
}

