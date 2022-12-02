using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

[Authorize(Roles = "Owner, Manager")]
public class EmployeeController : Controller
{
    [HttpGet]
    public IActionResult All()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
}