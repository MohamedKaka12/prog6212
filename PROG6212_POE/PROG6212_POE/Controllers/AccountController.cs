using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PROG6212_POE.Models; 
using System.Security.Claims; //(dotnet-bot, 2025)
using Claim = System.Security.Claims.Claim; //(dotnet-bot, 2025)

namespace PROG6212_POE.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (StaticDb.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View(model);
                }

                StaticDb.Users.Add(model);
                TempData["SuccessMessage"] = "Registration successful. Please log in.";

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            var user = StaticDb.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal); //(Rick-Anderson, 2024)

                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth"); //(Rick-Anderson, 2024)
            return RedirectToAction("Login", "Account");
        }
    }
}

//Reference List
//Rick-Anderson (2024). Use cookie authentication without ASP.NET Core Identity. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-9.0.
//dotnet-bot (2025). System.Security.Claims Namespace. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.security.claims?view=net-9.0.
//YouTube. (n.d.). ASP.NET Core MVC with Role-Based Authentication and Authorization. [online] Available at: http://www.youtube.com/playlist?list=PLTOaik4iw-qUzTx82QuAso1NktusoyJ9J.