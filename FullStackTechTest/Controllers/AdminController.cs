using Microsoft.AspNetCore.Mvc;

namespace FullStackTechTest.Controllers;

public class AdminController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
