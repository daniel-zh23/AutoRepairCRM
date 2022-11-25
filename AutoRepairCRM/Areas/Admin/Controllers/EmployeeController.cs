using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Areas.Admin.Controllers;

[Area("Admin")]
[Route("/Admin/[Controller]/[Action]/{id?}")]
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