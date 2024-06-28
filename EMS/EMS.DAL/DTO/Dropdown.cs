using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DAL.DTO;

public class Dropdown
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    [Required]
    public string? Name { get; set; }
}