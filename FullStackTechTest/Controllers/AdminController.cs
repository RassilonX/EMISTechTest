using DAL.Interfaces;
using Database.Models;
using FullStackTechTest.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace FullStackTechTest.Controllers;

public class AdminController : Controller
{
    private readonly ISpecialtyRepository _specialtyRepository;

    public AdminController(ISpecialtyRepository specialtyRepository)
    {
        _specialtyRepository = specialtyRepository;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminViewModel() 
        { 
            SpecialtyList = await _specialtyRepository.ListAllSpecialtiesAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AdminViewModel viewModel)
    {
        await _specialtyRepository.SaveNewSpecialtyAsync(viewModel.NewSpecialty);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        await _specialtyRepository.DeleteSpecialtyAsync(id);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> AddSpecialty()
    {
        var model = new AdminViewModel() { AddNewSpecialty = true };

        return View("Index", model);
    }
}
