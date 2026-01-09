using Microsoft.AspNetCore.Mvc;

namespace GazelServices.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}