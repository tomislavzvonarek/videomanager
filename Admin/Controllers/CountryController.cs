using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Models;
using VideoManager.Utility;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // upsert country
        public IActionResult Upsert(int? id)
        {
            Country country = new();
            if (id is null || id.Equals(0))
            {
                return View(country);
            }

            country = _unitOfWork.CountryRepository.Get(x => x.Id == id);
            
            return View(country);
        }

        [HttpPost]
        public IActionResult Upsert(Country country)
        {
            if (ModelState.IsValid)
            {
                if (country.Id.Equals(0))
                {
                    _unitOfWork.CountryRepository.Add(country);
                }
                else
                {
                    _unitOfWork.CountryRepository.Update(country);
                }
                _unitOfWork.Save();
                TempData["success"] = "Country created successfully";

                return RedirectToAction("Index");
            }

            return View(country);
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Country> countyList = _unitOfWork.CountryRepository.GetAll().ToList();
            return View(countyList);
        }
    
        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Country> countryList = _unitOfWork.CountryRepository.GetAll().ToList();
            return Json(new { data = countryList });
        }
        
        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var countryToDelete = _unitOfWork.CountryRepository.Get(x => x.Id == id);
            if (countryToDelete is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitOfWork.CountryRepository.Remove(countryToDelete);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = "Delete Successfull" });
        }
    

        #endregion

    }
}

