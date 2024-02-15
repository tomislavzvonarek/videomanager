using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoManager.DAL.Repository;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Models;

namespace VideoManager.Areas.User.Controllers;
[Area("User")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Video> videoList = _unitOfWork.VideoRepository.GetAll(includeProperties: "Genre,Tag");
        return View(videoList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

