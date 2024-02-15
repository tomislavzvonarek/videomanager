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
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Genre> genreList = _unitOfWork.GenreRepository.GetAll().ToList();
            return View(genreList);
        }

        public IActionResult Upsert(int? id)
        {
            Genre genre = new();
            if (id is null || id.Equals(0))
            {
                return View(genre);
            }

            genre = _unitOfWork.GenreRepository.Get(x => x.Id == id);

            return View(genre);
        }
        
        [HttpPost]
        public IActionResult Upsert(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (genre.Id.Equals(0))
                {
                    _unitOfWork.GenreRepository.Add(genre);
                }
                else
                {
                    _unitOfWork.GenreRepository.Update(genre);
                }
                _unitOfWork.Save();
                TempData["success"] = "Country created successfully";

                return RedirectToAction("Index");
            }

            return View(genre);
        }
        
        //BEFORE UPSERT 
        
        // public IActionResult Create()
        // {
        //     return View();
        //     
        // }
        //
        // [HttpPost]
        // public IActionResult Create(Genre obj)
        // {
        //
        //     if (!ModelState.IsValid)
        //     {
        //         return View();
        //     }
        //     _unitOfWork.GenreRepository.Add(obj);
        //     _unitOfWork.Save();
        //     TempData["success"] = "Genre added successfully";
        //
        //     return RedirectToAction("Index");
        // }
        //
        // public IActionResult Edit(int? id)
        // {
        //     if(id.Equals(null) || id == 0)
        //     {
        //         return NotFound();
        //     }
        //     Genre? GenreFromDb = _unitOfWork.GenreRepository.Get(x => x.Id == id);
        //
        //     return GenreFromDb.Equals(null) ? NotFound() : View(GenreFromDb);
        // }
        //
        // [HttpPost]
        // public IActionResult Edit(Genre obj)
        // {
        //     if(!ModelState.IsValid)
        //     {
        //         return View();
        //     }
        //     _unitOfWork.GenreRepository.Update(obj);
        //     _unitOfWork.Save();
        //     TempData["success"] = "Genre updated successfully";
        //
        //     return RedirectToAction("Index");
        // }
        //
        // public IActionResult Delete(int? id)
        // {
        //     if (id.Equals(null) || id == 0)
        //     {
        //         return NotFound();
        //     }
        //     Genre? GenreFromDb = _unitOfWork.GenreRepository.Get(x => x.Id == id);
        //
        //     return GenreFromDb.Equals(null) ? NotFound() : View(GenreFromDb);
        // }
        //
        // [HttpPost, ActionName("Delete")]
        // public IActionResult DeletePOST(int? id)
        // {
        //     Genre? Genre = _unitOfWork.GenreRepository.Get(x => x.Id == id);
        //     if(Genre.Equals(null))
        //     {
        //         return NotFound();
        //     }
        //     _unitOfWork.GenreRepository.Remove(Genre);
        //     _unitOfWork.Save();
        //     TempData["success"] = "Genre removed successfully";
        //
        //     return RedirectToAction("Index");
        // }
        
        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Genre> genreList = _unitOfWork.GenreRepository.GetAll().ToList();
            return Json(new { data = genreList });
        }
        
        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var genreToDelete = _unitOfWork.GenreRepository.Get(x => x.Id == id);
            if (genreToDelete is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitOfWork.GenreRepository.Remove(genreToDelete);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = "Delete Successfull" });
        }
    

        #endregion

    }
}

