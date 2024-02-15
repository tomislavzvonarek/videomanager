using System;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VideoManager.Models
{
	public class Video
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Range(1, 100)]
        [Display(Name = "Total Time")]
        public int TotalTime { get; set; }
        [Display(Name = "Streaming URL")]
        public string? StreamingUrl { get; set; }
		[Display(Name = "Genre")]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        [ValidateNever]
        public Genre? Genre { get; set; }
        [Display(Name = "Tag")]
        public int TagId { get; set; }
        [ForeignKey("TagId")] 
        [ValidateNever]
        public Tag? Tag { get; set; }
		//TODO: make this required!
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}

