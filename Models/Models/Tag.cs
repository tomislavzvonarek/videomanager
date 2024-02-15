using System;
using System.ComponentModel.DataAnnotations;

namespace VideoManager.Models
{
	public class Tag
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name = "name")]
		public string? Name { get; set; }
	}
}

