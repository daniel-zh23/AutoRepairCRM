using AutoRepairCRM.Core.Contracts;
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

    public async Task<IActionResult> Index()
    {
        var model = await _revenueService.GetRevenue();
        return View(model);
    }
}