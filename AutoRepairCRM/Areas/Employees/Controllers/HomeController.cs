using AutoRepairCRM.Areas.Employees.Models;
using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Extensions;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Areas.Employees.Controllers;

[Area("Employees")]
[Route("/Employees/[Controller]/[Action]/{id?}")]
[Authorize(Roles = "Owner, Worker")]
public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public HomeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<IActionResult> Services([FromQuery] AllForEmployeeModel query)
    {
        var employeeId = await _employeeService.GetEmployeeId(User.Id());
        if (!await _employeeService.Exists(employeeId))
        {
            return RedirectToAction("Index", "Home");
        }
        var models = await _employeeService.GetServices(employeeId, query.Sorting, query.CurrentPage, AllForEmployeeModel.ServicesPerPage);
        query.Services = models.People;
        query.TotalServices = models.Total;
        return View(query);
    }
}