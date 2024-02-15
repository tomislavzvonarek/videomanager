using System.ComponentModel.DataAnnotations;

namespace VideoManager.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
}