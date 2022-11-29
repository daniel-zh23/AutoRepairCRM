using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoRepairCRM.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.IsInRole("Owner"))
        {
            return RedirectToAction("Dashboard", "Home", new {Area = "Admin"});
        }
        else if (User.IsInRole("Customer"))
        {
            return RedirectToAction("Personal", "Customer", new {Area = ""});
        }

        return View();
    }
    
    public IActionResult Test()
    {
        return View("Privacy");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}