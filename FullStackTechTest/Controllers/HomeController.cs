﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FullStackTechTest.Models.Home;
using FullStackTechTest.Models.Shared;
using DAL.Interfaces;

namespace FullStackTechTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IDoctorSpecialtyRepository _personSpecialtyRepository;

    public HomeController(ILogger<HomeController> logger, IPersonRepository personRepository, IAddressRepository addressRepository, IDoctorSpecialtyRepository personSpecialtyRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
        _addressRepository = addressRepository;
        _personSpecialtyRepository = personSpecialtyRepository;
    }

    public async Task<IActionResult> Index()
    {
        var model = await IndexViewModel.CreateAsync(_personRepository);
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, false, _personRepository, _addressRepository, _personSpecialtyRepository);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, true, _personRepository, _addressRepository, _personSpecialtyRepository);
        return View("Details", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [FromForm] DetailsViewModel model)
    {
        await _personRepository.SaveAsync(model.Person);
        await _addressRepository.SaveAsync(model.Address);
        await _personSpecialtyRepository.SaveAsync(model.SpecialtyList, model.Person.Id);
        return RedirectToAction("Details", new { id = model.Person.Id });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}