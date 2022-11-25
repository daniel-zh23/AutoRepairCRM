using AutoRepairCRM.Areas.Admin.Models;
using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Areas.Admin.Controllers;

[Area("Admin")]
[Route("/Admin/[Controller]/[Action]/{id?}")]
[Authorize(Roles = "Owner")]
public class CustomerController : Controller
{
    private ICarService _carService;
    private ICustomerService _customerService;

    public CustomerController(ICarService carService, ICustomerService customerService)
    {
        _carService = carService;
        _customerService = customerService;
    }
    
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllCustomerQueryModel query)
    {
        var models = await _customerService.All(
            query.SearchTerm,
            query.CurrentPage,
            AllCustomerQueryModel.CustomersPerPage);

        query.Customers = models.Customers;
        query.TotalCustomers = models.TotalCustomers;
        return View(query);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new CustomerInputModel();
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CustomerInputModel model)
    {
        if (!await _carService.CarExists(model.CarId))
        {
            ModelState.AddModelError(nameof(model.CarId), "Car does not exist!");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Cars = await _carService.GetAllCarsAsync();
            return View(model);
        }
        
        var isCreated = await _customerService.Add(model);

        if (isCreated) return RedirectToAction("All");
        
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        ModelState.AddModelError(nameof(model), "Error creating user!");
        return View(model);

    }

}