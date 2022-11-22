using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

[Authorize(Roles = "Customer, Manager, Owner")]
public class CustomerController : Controller
{
    private ICarService _carService;
    private ICustomerService _customerService;

    public CustomerController(ICarService carService, ICustomerService customerService)
    {
        _carService = carService;
        _customerService = customerService;
    }

    public async Task<IActionResult> GetPersonal()
    {
        var customerId = await _customerService.GetCustomerId(User.Id());
        var models = await _carService.GetAllForCustomer(customerId);

        return View(models);
    }

    public async Task<IActionResult> ViewServices(int carId, int customerId)
    {
        if (!await _customerService.Exists(customerId))
        {
            return RedirectToAction("GetPersonal");
        }

        var model = await _carService.GetServicesById(carId, customerId);
        
        return View(model);
    }
}