using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PROG6212_POE.Models;

namespace PROG6212_POE.Controllers;

[Authorize(AuthenticationSchemes = "CookieAuth")] ////(wadepickett, 2025)
public class HomeController : Controller
{
    public IActionResult Dashboard()
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value; //(dotnet-bot, 2025)

        return role switch //(BillWagner, 2022)
        {
            nameof(UserRole.Lecturer) => View("LecturerView"),
            nameof(UserRole.ProgrammeCoordinator) => View("CoordinatorView"),
            nameof(UserRole.AcademicManager) => View("ManagerView"),
            _ => RedirectToAction("Logout", "Account"), 
        };
    }

    public IActionResult LecturerView() => View();
    public IActionResult CoordinatorView() => View();
    public IActionResult ManagerView() => View();

}

//Reference List
//BillWagner (2022). switch expression - Evaluate a pattern match expression using the `switch` expression - C# reference. [online] Microsoft.com. Available at: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression.
//wadepickett (2025). Introduction to authorization in ASP.NET Core. [online] Microsoft.com. Available at: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/.
//dotnet-bot (2025). System.Security.Claims Namespace. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.security.claims?view=net-9.0.
//YouTube. (n.d.). ASP.NET Core MVC with Role-Based Authentication and Authorization. [online] Available at: http://www.youtube.com/playlist?list=PLTOaik4iw-qUzTx82QuAso1NktusoyJ9J.
