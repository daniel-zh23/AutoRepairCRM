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
    private readonly IAccountService _accountService;

    public EmployeeController(IEmployeeService employeeService, IAccountService accountService)
    {
        _employeeService = employeeService;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllEmployeeQueryModel query)
    {
        var models = await _employeeService.All(query.SearchTerm, query.Sorting, query.Filter, query.CurrentPage,
            AllEmployeeQueryModel.PeoplePerPage);

        query.People = models.Items;
        query.Total = models.Total;
        return View(query);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.Roles = await _employeeService.GetAllRoles();
        
        ViewBag.Title = "Add Employee";
        ViewBag.IsEdit = false;
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
            
            ViewBag.Title = "Add Employee";
            ViewBag.IsEdit = false;
            return View(model);
        }
        
        var user = await _accountService.CreateEmployee(model);
        string? empId = null;
        
        if (user != null)
        {
            empId = await _employeeService.Add(model, user);
        }

        if (!string.IsNullOrEmpty(empId))
        {
            return RedirectToAction(nameof(All));
        }

        ViewBag.Roles = await _employeeService.GetAllRoles();
        ModelState.AddModelError(nameof(model), "Server error!");
        ViewBag.Title = "Add Employee";
        ViewBag.IsEdit = false;
        return View(model);
    }

    public async Task<IActionResult> Fire(int id)
    {
        if (!await _employeeService.Exists(id))
        {
            return RedirectToAction(nameof(All));
        }

        await _employeeService.Fire(id);

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!await _employeeService.Exists(id))
        {
            return RedirectToAction("All");
        }

        var model = await _employeeService.GetEmployeeEdit(id);

        ViewBag.Title = "Edit Employee";
        ViewBag.Roles = await _employeeService.GetAllRoles();
        ViewBag.IsEdit = true;
        return View("Add", model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, EmployeeInputModel model)
    {
        if (!await _employeeService.RolesExist(model.Roles))
        {
            ModelState.AddModelError(string.Empty, "Invalid role!");
        }
        
        if (!await _employeeService.Exists(id))
        {
            return RedirectToAction(nameof(All));
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await _employeeService.GetAllRoles();
            
            ViewBag.Title = "Edit Employee";
            ViewBag.IsEdit = true;
            return View(nameof(Add), model);
        }

        var isFinished = await _employeeService.Update(id, model);
        if (isFinished)
        {
            return RedirectToAction(nameof(All));
        }

        ViewBag.Roles = await _employeeService.GetAllRoles();
        ModelState.AddModelError(nameof(model), "Server error!");
        ViewBag.Title = "Edit Employee";
        ViewBag.IsEdit = true;
        return View(nameof(Add), model);
    }
}