using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Employee;
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
    public IActionResult All()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.Roles = await _employeeService.GetRoles();
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(EmployeeInputModel model)
    {
        if (!await _employeeService.RolesExist(model.Roles.ToArray()))
        {
            ModelState.AddModelError(string.Empty, "Invalid role!");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await _employeeService.GetRoles();
            return View(model);
        }

        var empId = await _employeeService.Add(model);
        if (!string.IsNullOrEmpty(empId))
        {
            return RedirectToAction(nameof(All));
        }
        
        ViewBag.Roles = await _employeeService.GetRoles();
        ModelState.AddModelError(nameof(model), "Server error!");
        return View(model);
    }
}