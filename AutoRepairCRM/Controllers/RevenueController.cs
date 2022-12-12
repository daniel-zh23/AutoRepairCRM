using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

[Authorize(Roles = "Owner")]
public class RevenueController : Controller
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] AllRevenueQueryModel query)
    {
        var model = await _revenueService.GetRevenue(new DateTime(query.Year, query.Month, 1));
        query.Employees = model.Employees;
        query.Revenue = model.RevenueThisMonth;
        query.Profit = model.Profit;
        return View(query);
    }
}