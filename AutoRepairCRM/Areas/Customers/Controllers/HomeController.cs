using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Extensions;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Areas.Customers.Controllers;

[Area("Customers")]
[Route("/Customers/[Controller]/[Action]/{id?}")]
[Authorize(Roles = "Customer, Manager, Owner")]
public class HomeController : Controller
{
    private ICarService _carService;
    private ICustomerService _customerService;

    public HomeController(ICarService carService, ICustomerService customerService)
    {
        _carService = carService;
        _customerService = customerService;
    }

    public async Task<IActionResult> Personal()
    {
        var query = new AllForCustomerModel();
        var customerId = await _customerService.GetCustomerId(User.Id());
        if (!await _customerService.Exists(customerId))
        {
            return RedirectToAction("Index", "Home");
        }
        var models = await _carService.GetAllForCustomer(customerId, query.CurrentPage, AllForCustomerModel.CarsPerPage);
        query.Cars = models.Cars;
        query.TotalCars = models.TotalCars;
        return View(query);
    }

    public async Task<IActionResult> ViewServices(int carId, int customerId)
    {
        if (!await _customerService.Exists(customerId))
        {
            return RedirectToAction("Personal");
        }

        var model = await _carService.GetServicesById(carId, customerId);
        
        return View(model);
    }
}