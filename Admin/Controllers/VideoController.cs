using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Models;
using VideoManager.Models.ViewModels;
using VideoManager.Utility;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VideoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VideoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Video> videoList = _unitOfWork.VideoRepository.GetAll(includeProperties: "Genre,Tag").ToList();
            return View(videoList);
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Upsert(int? id)
        {
            VideoVM videoVM = new()
            {
                GenreList = _unitOfWork.GenreRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                TagList = _unitOfWork.TagRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Video = new()
            };
            //if (id == null) 
            if (id is null || id.Equals(0))
            {
                return View(videoVM);
            }
            else
            {
                videoVM.Video = _unitOfWork.VideoRepository.Get(x => x.Id == id);
                return View(videoVM);
            }
        }
        
        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin)]
        public IActionResult Upsert(VideoVM videoVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string videoPath = Path.Combine(wwwRootPath, @"images/video");
                    //delete old image when updating
                    if (!string.IsNullOrEmpty(videoVM.Video.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, videoVM.Video.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(videoPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    videoVM.Video.ImageUrl = @"/images/video/" + fileName;
                }

                if (videoVM.Video.Id.Equals(0))
                {
                    _unitOfWork.VideoRepository.Add(videoVM.Video);
                }
                else
                {
                    _unitOfWork.VideoRepository.Update(videoVM.Video);
                }

                _unitOfWork.Save();
                TempData["success"] = "Video created successfully";

                return RedirectToAction("Index");
            }

            //to populate dropdowns if !ModelState
            videoVM.GenreList = _unitOfWork.GenreRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            videoVM.TagList = _unitOfWork.TagRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(videoVM);

        }

        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Video> videoList = _unitOfWork.VideoRepository.GetAll(includeProperties: "Genre,Tag").ToList();
            return Json(new { data = videoList });
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var videoToDelete = _unitOfWork.VideoRepository.Get(x => x.Id == id);
            if (videoToDelete is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, videoToDelete.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.VideoRepository.Remove(videoToDelete);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = "Delete Successfull" });
        }
    

        #endregion
    }
}

