using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG6212_POE.Models;
using System.Security.Claims;
using Claim = PROG6212_POE.Models.Claim;

namespace PROG6212_POE.Controllers
{
    [Authorize(AuthenticationSchemes = "CookieAuth")] //(wadepickett, 2025)
    public class ClaimsController : Controller
    {
        [Authorize(Roles = nameof(UserRole.Lecturer))] //(wadepickett, 2025)
        public IActionResult Submit()
        {
            return View(new Claim());
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Lecturer))] //(wadepickett, 2025)
        public async Task<IActionResult> Submit(Claim model, IFormFile? supportingDocument) //(tdykstra, 2025)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Validation failed. Please ensure the Hours Worked and Hourly Rate are valid.";
                return View(model);
            }

            if (supportingDocument != null)
            {
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var fileExtension = Path.GetExtension(supportingDocument.FileName).ToLowerInvariant();
                const long maxFileSize = 5 * 1024 * 1024; 

                if (!allowedExtensions.Contains(fileExtension))
                {
                    TempData["ErrorMessage"] = "Invalid file type. Only PDF, DOCX, and XLSX are allowed.";
                    return View(model);
                }

                if (supportingDocument.Length > maxFileSize)
                {
                    TempData["ErrorMessage"] = "The file size exceeds the 5MB limit.";
                    return View(model);
                }

                var uniqueFileName = $"{model.ClaimId}_{Path.GetFileNameWithoutExtension(supportingDocument.FileName)}{fileExtension}";

                model.AttachedFileName = uniqueFileName;
            }


            model.LecturerUsername = User.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
            model.Status = ClaimStatus.Pending; 
            model.SubmittedDate = DateTime.Now;

            StaticDb.AddClaim(model);

            TempData["SuccessMessage"] = $"Claim for R{model.TotalAmount:F2} submitted successfully! Status: Pending Coordinator Review.";

            return RedirectToAction("Dashboard", "Home");
        }
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.ProgrammeCoordinator) + "," + nameof(UserRole.AcademicManager))] //(wadepickett, 2025)
        public IActionResult Approve(string claimId)
        {
            var reviewerUsername = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? "Unknown";
            var roleString = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (!Enum.TryParse(roleString, out UserRole reviewerRole))
            {
                TempData["ErrorMessage"] = "Could not determine reviewer role.";
                return RedirectToAction("Dashboard", "Home");
            }

            bool success = StaticDb.ReviewClaim(claimId, reviewerUsername, reviewerRole, isApproved: true);

            if (success)
            {
                if (reviewerRole == UserRole.ProgrammeCoordinator)
                {
                    TempData["SuccessMessage"] = $"Claim {claimId} approved by Coordinator and sent to Academic Manager for final review.";
                }
                else 
                {
                    TempData["SuccessMessage"] = $"Claim {claimId} FINALLY APPROVED and processed.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to approve claim {claimId}. It may have already been reviewed.";
            }

            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.ProgrammeCoordinator) + "," + nameof(UserRole.AcademicManager))] //(wadepickett, 2025)
        public IActionResult Reject(string claimId, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "A rejection reason is required.";
                return RedirectToAction("Dashboard", "Home");
            }

            var reviewerUsername = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? "Unknown";
            var roleString = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            Enum.TryParse(roleString, out UserRole reviewerRole); //(BillWagner, 2025)

            bool success = StaticDb.ReviewClaim(claimId, reviewerUsername, reviewerRole, isApproved: false, rejectionReason: reason);

            if (success)
            {
                TempData["SuccessMessage"] = $"Claim {claimId} was rejected by the {reviewerRole}. Lecturer notified.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to reject claim {claimId}. It may have already been reviewed.";
            }

            return RedirectToAction("Dashboard", "Home");
        }
    }
}

//Reference List
//wadepickett (2025). Introduction to authorization in ASP.NET Core. [online] Microsoft.com. Available at: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/.
//tdykstra (2025). Model Binding in ASP.NET Core. [online] Microsoft.com. Available at: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding.
//BillWagner (2025). The nameof expression - evaluate the text name of a symbol - C# reference. [online] Microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/nameof.