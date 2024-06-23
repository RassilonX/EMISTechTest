using DAL;
using DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FullStackTechTest.Controllers;

public class ImportFileController : Controller
{
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IDataImportRepository _dataImportRepository;

    public ImportFileController(IPersonRepository personRepository, IAddressRepository addressRepository, IDataImportRepository dataImportRepository)
    {
        _personRepository = personRepository;
        _addressRepository = addressRepository;
        _dataImportRepository = dataImportRepository;
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

            List<ImportJsonDto> data = JsonConvert.DeserializeObject<List<ImportJsonDto>>(jsonData);

            var success = await _dataImportRepository.SaveJson(data);

            if (!success)
            {
                return StatusCode(500, new { success = false, message = "Failed to import data" });
            }

            return Ok(new
            {
                success = true,
                message = "Data imported successfully"
            });
        }
    }
}
