using System.Diagnostics;
using AutoRepairCRM.Core.Contracts;
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
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}