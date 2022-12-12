using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

[Authorize(Roles = "Owner, OfficeEmployee")]
public class CustomerController : Controller
{
    private ICarService _carService;
    private ICustomerService _customerService;
    private IEmployeeService _employeeService;
    private IAccountService _accountService;

    public CustomerController(ICarService carService, ICustomerService customerService, IEmployeeService employeeService, IAccountService accountService)
    {
        _carService = carService;
        _customerService = customerService;
        _employeeService = employeeService;
        _accountService = accountService;
    }
    
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllCustomerQueryModel customerQuery)
    {
        var models = await _customerService.All(
            customerQuery.SearchTerm,
            customerQuery.CurrentPage,
            AllCustomerQueryModel.PeoplePerPage);

        customerQuery.People = models.People;
        customerQuery.Total = models.Total;
        return View(customerQuery);
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

        var user = await _accountService.CreateCustomer(model);
        var customerId = -1;
        
        if (user != null)
        {
            customerId = await _customerService.Add(user);
        }
        
        if (customerId != -1) return RedirectToAction(nameof(Details), customerId);
        
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
            return RedirectToAction(nameof(All));
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
        return View(nameof(Add), model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CustomerInputModel model)
    {
        if (!await _customerService.Exists(id))
        {
            return RedirectToAction(nameof(All));
        }
        
        if (!ModelState.IsValid)
        {
            return View(nameof(Add), model);
        }

        var isSucceeded = await _customerService.Update(id, model);

        if (isSucceeded) return RedirectToAction(nameof(All));
        
        ModelState.AddModelError(nameof(model), "Error updating user!");
        ViewBag.Title = "Edit Customer";
        return View(nameof(Add), model);

    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (!await _customerService.Exists(id))
        {
            return RedirectToAction(nameof(All));
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
            return RedirectToAction(nameof(Details), new{id = model.CustomerId});
        }
        
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        ViewBag.FuelTypes = await _carService.GetFuelTypes();
        ModelState.AddModelError(nameof(model), "Server error!");
        return View(model);

    }
    
    [HttpGet]
    public async Task<IActionResult> EditCar(int carId, int customerId)
    {
        var model = await _customerService.GetCustomerCar(carId, customerId);
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        ViewBag.FuelTypes = await _carService.GetFuelTypes();
        return View(nameof(AddCar), model);
    }

    [HttpPost]
    public async Task<IActionResult> EditCar(CustomerCarInputModel model)
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
            return View(nameof(AddCar), model);
        }

        var isFinished = await _customerService.UpdateCustomerCar(model);
        if (isFinished)
        {
            return RedirectToAction(nameof(Details), new{id = model.CustomerId});
        }
        
        ViewBag.Cars = await _carService.GetAllCarsAsync();
        ViewBag.FuelTypes = await _carService.GetFuelTypes();
        ModelState.AddModelError(nameof(model), "Server error!");
        return View(nameof(AddCar), model);

    }

    [HttpGet]
    public async Task<IActionResult> AddService(int carId, int customerId)
    {
        ViewBag.ServiceTypes = await _carService.GetServiceTypes();
        ViewBag.Employees = await _employeeService.AllForForm();
        var model = new CarServiceInputModel();
        return View(nameof(AddService), model);
    }

    [HttpPost]
    public async Task<IActionResult> AddService(CarServiceInputModel model)
    {
        if (!await _carService.CarExists(model.CarId))
        {
            ModelState.AddModelError(string.Empty, "Car is not valid.");
        }
        
        if (!await _carService.ServiceTypeExists(model.ServiceTypeId))
        {
            ModelState.AddModelError(string.Empty, "Service type is not valid.");
        }

        if (!await _customerService.Exists(model.CustomerId))
        {
            ModelState.AddModelError(string.Empty, "Customer is not valid.");
        }

        foreach (var emp in model.Employees)
        {
            if (!await _employeeService.Exists(emp))
            {
                ModelState.AddModelError(string.Empty, "Employee is not valid.");
            }
        }

        model.DateStarted = DateTime.UtcNow;
        
        if (!ModelState.IsValid)
        {
            ViewBag.ServiceTypes = await _carService.GetServiceTypes();
            ViewBag.Employees = await _employeeService.AllForForm();
            return View(nameof(AddService), model);
        }

        var isFinished = await _customerService.AddCustomerCarService(model);
        if (isFinished)
        {
            return RedirectToAction(nameof(Details), new{id = model.CustomerId});
        }
        
        ViewBag.ServiceTypes = await _carService.GetServiceTypes();
        ViewBag.Employees = await _employeeService.AllForForm();
        ModelState.AddModelError(nameof(model), "Server error!");
        return View(nameof(AddService), model);

    }
}