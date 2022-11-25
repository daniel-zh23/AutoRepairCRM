using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Areas.Admin.Controllers;


[Area("Admin")]
[Route("/Admin/[Controller]/[Action]/{id?}")]
[Authorize(Roles = "Owner")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}