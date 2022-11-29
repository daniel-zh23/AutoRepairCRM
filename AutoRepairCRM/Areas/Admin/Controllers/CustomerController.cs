using AutoRepairCRM.Areas.Admin.Models;
using AutoRepairCRM.Core.Contracts;
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
    public IActionResult Add()
    {
        var model = new CustomerInputModel();
        ViewBag.Title = "Add Customer";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CustomerInputModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = "Add Customer";
            return View(model);
        }
        
        var customerId = await _customerService.Add(model);

        if (customerId != -1) return RedirectToAction("Details", customerId);
        
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        ModelState.AddModelError(nameof(model), "Error creating user!");
        ViewBag.Title = "Add Customer";
        return View(model);

    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!await _customerService.Exists(id))
        {
            return RedirectToAction("All");
        }

        var customer = await _customerService.GetCustomerDetails(id);
        var model = new CustomerInputModel()
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone
        };

        ViewBag.Title = "Edit Customer";
        return View("Add", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CustomerInputModel model)
    {
        if (!await _customerService.Exists(id))
        {
            return RedirectToAction("All");
        }
        
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var isSucceeded = await _customerService.Update(id, model);

        if (isSucceeded) return RedirectToAction("All");
        
        ModelState.AddModelError(nameof(model), "Error updating user!");
        ViewBag.Title = "Edit Customer";
        return View("Add", model);

    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (!await _customerService.Exists(id))
        {
            return RedirectToAction("All");
        }

        var customer = await _customerService.GetCustomerDetails(id);

        return View(customer);
    }
    
    [HttpGet]
    public async Task<IActionResult> AddCar(int id)
    {
        var model = new CustomerCarInputModel
        {
            CustomerId = id
        };
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        ViewBag.FuelTypes = await _carService.GetFuelTypes();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddCar(CustomerCarInputModel model)
    {
        if (!await _carService.CarExists(model.CarId))
        {
            ModelState.AddModelError(string.Empty, "Car is not valid.");
        }

        if (!await _carService.FuelExists(model.FuelTypeId))
        {
            ModelState.AddModelError(string.Empty, "Fuel is not valid.");
        }

        if (!await _customerService.Exists(model.CustomerId))
        {
            ModelState.AddModelError(string.Empty, "Customer is not valid.");
        }
        
        if (!ModelState.IsValid)
        {
            ViewBag.Cars = await _carService.GetAllCarsAsync();
            ViewBag.FuelTypes = await _carService.GetFuelTypes();
            return View(model);
        }

        var isFinished = await _customerService.AddCustomerCar(model);
        if (isFinished)
        {
            return RedirectToAction("Details", new{id = model.CustomerId});
        }
        
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        ViewBag.FuelTypes = await _carService.GetFuelTypes();
        ModelState.AddModelError(nameof(model), "Server error!");
        return View(model);

    }

}