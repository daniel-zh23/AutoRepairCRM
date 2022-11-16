using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

[Authorize(Roles = "Customer, Manager, Owner")]
public class CustomerController : Controller
{
    public IActionResult GetPersonal()
    {
        throw new NotImplementedException();
    }
}