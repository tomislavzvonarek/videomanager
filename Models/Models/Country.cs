using System.ComponentModel.DataAnnotations;

namespace VideoManager.Models;

public class Country
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(2)]
    public string? Code { get; set; }
    [Required]
    public string? Name { get; set; }
}