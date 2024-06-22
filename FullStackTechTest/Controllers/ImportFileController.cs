using FullStackTechTest.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace FullStackTechTest.Controllers
{
    public class ImportFileController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
