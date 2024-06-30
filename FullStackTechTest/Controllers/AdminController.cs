using DAL.Interfaces;
using Database.Models;
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
            SpecialtyList = await _personSpecialtyRepository.ListAllSpecialtiesAsync()
        };

        return View(model);
    }

    public async Task<IActionResult> Edit([FromForm] AdminViewModel viewModel)
    {
        var model = new AdminViewModel()
        {
            SpecialtyList = await _personSpecialtyRepository.ListAllSpecialtiesAsync(),
            IsEditing = true
        };

        return View("Index", model);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AdminViewModel viewModel)
    {
        viewModel.SpecialtyList.Add(
            new Specialty { SpecialtyName = "" }
            );

        var dummy = 6;

        return View("Index", viewModel);
    }
}
