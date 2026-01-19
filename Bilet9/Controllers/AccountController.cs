using Bilet9.Models;
using Bilet9.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bilet9.Controllers;

public class AccountController(UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager,RoleManager<IdentityRole> _roleManager) : Controller
{
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        AppUser user = new() 
        {
            Email = vm.Email,
            UserName = vm.UserName,
            FullName = vm.FullName
        };

        var result = await _userManager.CreateAsync(user,vm.Password);
        if(!result.Succeeded)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(vm);
        }

        await _userManager.AddToRoleAsync(user, "Member");
        await _signInManager.SignInAsync(user, false);


        return RedirectToAction("Index" , "Home");
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var user  = await _userManager.FindByEmailAsync(vm.Email);

        if(user is null)
        {
            ModelState.AddModelError("", "Email or password wrong");
            return View(vm);
        }

        var sigInResult = await _signInManager.PasswordSignInAsync(user,vm.Password,false,true);
        if(sigInResult.Succeeded)
        {
            ModelState.AddModelError("", "Email or password wrong");
            return View(vm);
        }


        return RedirectToAction("Index" , " Home");
    }
    public async Task<IActionResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> CreateRoles()
    {
        await _roleManager.CreateAsync(new() { Name = "Admin" });
        await _roleManager.CreateAsync(new() { Name = "Member" });
        return Ok("Roles created!");
       
    }
}
