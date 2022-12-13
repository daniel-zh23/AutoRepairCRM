using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Database.Data.Models.Account;
using AutoRepairCRM.Extensions;
using AutoRepairCRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairCRM.Controllers;

public class AccountController : Controller
{
    private SignInManager<ApplicationUser> _signInManager; 
    private UserManager<ApplicationUser> _userManager;
    private IAccountService _accountService;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IAccountService accountService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is not { IsActive: true })
        {
            ModelState.AddModelError(String.Empty, "Invalid user.");
            return View(model);
        }

        if (user.IsFirstLogin)
        {
            var signResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (signResult.Succeeded)
            {
                return RedirectToAction(nameof(ChangePassword), new {model.Email});
            }
        }
        
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        
        if (!result.Succeeded)
        {
            ModelState.AddModelError(String.Empty, "Invalid user.");
            return View(model);
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
    
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ChangePassword(string email)
    {
        var model = new PasswordChangeModel();
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> ChangePassword(PasswordChangeModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var user = await  _userManager.FindByEmailAsync(model.Email);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

        if (!result.Succeeded)
        {
            return View(model);
        }

        await _accountService.ChangeFirstLoginState(user, false);
        return RedirectToAction(nameof(Index), "Home");
    }
}