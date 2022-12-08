using System.Diagnostics;
using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Services;
using Microsoft.AspNetCore.Mvc;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoRepairCRM.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IServiceService serviceService)
    {
        _serviceService = serviceService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.IsInRole("Owner") || User.IsInRole("OfficeEmployee"))
        {
            return RedirectToAction("Dashboard"); 
        }
        else if (User.IsInRole("Worker"))
        {
            return RedirectToAction("Services", "Home", new {Area = "Employees"});
        }
        else
        {
            return RedirectToAction("Personal", "Home", new {Area = "Customers"});
        }
    }
    
    [Authorize(Roles = "Owner, OfficeEmployee")]
    public async Task<IActionResult> Dashboard()
    {
        var model = await _serviceService.GetActiveServices();
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Owner, OfficeEmployee")]
    public IActionResult CompleteService(int id)
    {
        var model = new ServiceCompleteModel()
        {
            Id = id
        };

        return View(model);
    }
    
    [HttpPost]
    [Authorize(Roles = "Owner, OfficeEmployee")]
    public async Task<IActionResult> CompleteService(ServiceCompleteModel model)
    {
        if (!await _serviceService.Exists(model.Id))
        {
            ModelState.AddModelError(nameof(model), "Invalid service id!");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        bool isFinished = await _serviceService.CompleteService(model);
        if (isFinished)
        {
            return RedirectToAction(nameof(Dashboard));
        }
        return RedirectToAction(nameof(Dashboard));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}