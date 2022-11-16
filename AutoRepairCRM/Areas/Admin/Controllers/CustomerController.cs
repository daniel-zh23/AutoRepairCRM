using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Areas.Admin.Controllers;

[Area("Admin")]
[Route("/Admin/[Controller]/[Action]/{id?}")]
[Authorize(Roles = "Owner")]
public class CustomerController : Controller
{

}