using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

[Authorize(Roles = "Owner, OfficeEmployee")]
public class CarController : Controller
{
    private ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IActionResult> All([FromQuery] AllCarsQueryModel query)
    {
        var cars = await _carService.GetAll(query.SearchTerm,
            query.CurrentPage, AllCarsQueryModel.CarsPerPage);

        query.Total = cars.Total;
        query.Cars = cars.Items;
        return View(query);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var model = new CarInputModel();
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(CarInputModel input)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        var carId = await _carService.AddCar(input);

        if (carId != -1)
        {
            return RedirectToAction(nameof(All));
        }
        
        ModelState.AddModelError(nameof(input), "Error adding car!");
        return View(input);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (await _carService.CarExists(id))
        {
            await _carService.DeleteCar(id);
        }
        return RedirectToAction(nameof(All));
    }
}