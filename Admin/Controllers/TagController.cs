using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Models;
using VideoManager.Utility;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Tag> tagList = _unitOfWork.TagRepository.GetAll().ToList();
            return View(tagList);
        }
        
        
          public IActionResult Upsert(int? id)
        {
            Tag tag = new();
            if (id is null || id.Equals(0))
            {
                return View(tag);
            }

            tag = _unitOfWork.TagRepository.Get(x => x.Id == id);

            return View(tag);
        }
        
        [HttpPost]
        public IActionResult Upsert(Tag tag)
        {
            if (ModelState.IsValid)
            {
                if (tag.Id.Equals(0))
                {
                    _unitOfWork.TagRepository.Add(tag);
                }
                else
                {
                    _unitOfWork.TagRepository.Update(tag);
                }
                _unitOfWork.Save();
                TempData["success"] = "Country created successfully";

                return RedirectToAction("Index");
            }

            return View(tag);
        }
        
        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Tag> tagList = _unitOfWork.TagRepository.GetAll().ToList();
            return Json(new { data = tagList });
        }
        
        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var tagtoDelete = _unitOfWork.TagRepository.Get(x => x.Id == id);
            if (tagtoDelete is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitOfWork.TagRepository.Remove(tagtoDelete);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = "Delete Successfull" });
        }
    

        #endregion

    }
}

