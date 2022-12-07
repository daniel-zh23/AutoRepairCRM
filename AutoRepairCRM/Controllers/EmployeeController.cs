using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

[Authorize(Roles = "Owner, Manager")]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllEmployeeQueryModel query)
    {
        var models = await _employeeService.All(query.SearchTerm, query.Sorting, query.CurrentPage,
            AllEmployeeQueryModel.PeoplePerPage);

        query.People = models.People;
        query.Total = models.Total;
        return View(query);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.Roles = await _employeeService.GetAllRoles();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(EmployeeInputModel model)
    {
        if (!await _employeeService.RolesExist(model.Roles))
        {
            ModelState.AddModelError(string.Empty, "Invalid role!");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await _employeeService.GetAllRoles();
            return View(model);
        }

        var empId = await _employeeService.Add(model);
        if (!string.IsNullOrEmpty(empId))
        {
            return RedirectToAction(nameof(All));
        }

        ViewBag.Roles = await _employeeService.GetAllRoles();
        ModelState.AddModelError(nameof(model), "Server error!");
        return View(model);
    }

    public IActionResult Details(int id)
    {
        throw new NotImplementedException();
    }
}