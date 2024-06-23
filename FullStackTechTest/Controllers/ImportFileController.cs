using DAL;
using FullStackTechTest.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FullStackTechTest.Controllers;

public class ImportFileController : Controller
{
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRepository _addressRepository;

    public ImportFileController(IPersonRepository personRepository, IAddressRepository addressRepository)
    {
        _personRepository = personRepository;
        _addressRepository = addressRepository;
    }

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

            // Need to get the count of people, and save that into a variable
            var personCount = await _personRepository.PersonCountAsync();

            // Use the variable to set the PersonId fields
            // Have the nulls or missing fields register as empty strings
            // Need to check if the database has any existing records, they need to be ignored or updated
            // Store the data in your database

            return Json(new { success = true });
        }
    }
}
