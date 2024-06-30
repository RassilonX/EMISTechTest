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
        var model = new AdminViewModel();
        if (viewModel.AddNewSpecialty)
        {
            model.SpecialtyList = await _personSpecialtyRepository.ListAllSpecialtiesAsync();
            model.IsEditing = false;
        }
        else
        {
            model.SpecialtyList = await _personSpecialtyRepository.ListAllSpecialtiesAsync();
            model.IsEditing = true;
        }

        return View("Index", model);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AdminViewModel viewModel)
    {
        viewModel.SpecialtyList.Add(
            new Specialty { SpecialtyName = "" }
            );

        return View("Index", viewModel);
    }

    public async Task<IActionResult> Remove(int id)
    {
        //This is stub logic to be implemented later
        var model = new AdminViewModel();
        model.SpecialtyList = await _personSpecialtyRepository.ListAllSpecialtiesAsync();
        model.IsEditing = false;

        return View("Index", model);
    }
}
