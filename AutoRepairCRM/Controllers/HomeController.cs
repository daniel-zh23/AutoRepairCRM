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
        return User.IsInRole("Owner")
            ? RedirectToAction("Dashboard")
            : RedirectToAction("Personal", "Home", new {Area = "Customers"});
    }
    
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