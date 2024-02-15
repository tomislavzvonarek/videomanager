using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VideoManager.Models.ViewModels;

public class VideoVM
{
    public Video Video { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> GenreList { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> TagList { get; set; }
}