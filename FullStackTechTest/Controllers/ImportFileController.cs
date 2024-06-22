using FullStackTechTest.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FullStackTechTest.Controllers;

public class ImportFileController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImportJson(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file was uploaded.");
        }

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            var jsonData = await reader.ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(jsonData);

            // Store the data in your database or perform any other necessary actions

            return Json(new { success = true });
        }
    }
}
