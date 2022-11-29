using AutoRepairCRM.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Areas.Admin.Controllers;


[Area("Admin")]
[Route("/Admin/[Controller]/[Action]/{id?}")]
[Authorize(Roles = "Owner")]
public class HomeController : Controller
{
    private readonly IServiceService _serviceService;

    public HomeController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public async Task<IActionResult> Dashboard()
    {
        var model = await _serviceService.GetActiveServices();
        return View(model);
    }
}