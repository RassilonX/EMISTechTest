using DAL.Interfaces;
using FullStackTechTest.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace FullStackTechTest.Controllers;

public class AdminController : Controller
{
    private readonly IPersonSpecialtyRepository _personSpecialtyRepository;

    public AdminController(IPersonSpecialtyRepository personSpecialtyRepository)
    {
        _personSpecialtyRepository = personSpecialtyRepository;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminViewModel() 
        { 
            SpecialtyList = await _personSpecialtyRepository.ListAllSpecialtiesAsync(),
            IsEditing = false
        };

        return View(model);
    }

    public async Task<IActionResult> Edit()
    {
        var model = new AdminViewModel()
        {
            SpecialtyList = await _personSpecialtyRepository.ListAllSpecialtiesAsync(),
            IsEditing = true
        };

        return View("Index", model);
    }
}
