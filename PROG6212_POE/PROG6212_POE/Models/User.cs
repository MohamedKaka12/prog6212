using System.ComponentModel.DataAnnotations;

namespace PROG6212_POE.Models
{
    public enum UserRole //(BillWagner, n.d.)
    {
        Lecturer,
        ProgrammeCoordinator,
        AcademicManager
    }

    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role")]
        public UserRole Role { get; set; }
    }

    public enum ClaimStatus //(BillWagner, n.d.)
    {
        Pending,
        ApprovedByCoordinator,
        ApprovedByManager,
        Rejected
    }

    public class Claim
    {
        public string ClaimId { get; set; } = Guid.NewGuid().ToString();

        public string LecturerUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hours worked is required.")]
        [Range(0.1, 1000, ErrorMessage = "Hours must be greater than 0.")] //(dotnet-bot, 2025)
        public double HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate is required.")]
        [Range(1.0, 10000.0, ErrorMessage = "Hourly rate must be a valid amount.")] //(dotnet-bot, 2025)
        public decimal HourlyRate { get; set; }

        public decimal TotalAmount => (decimal)HoursWorked * HourlyRate;

        public string AdditionalNotes { get; set; } = string.Empty;

        public string? AttachedFileName { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        public DateTime? ReviewedDate { get; set; }
        public string? ReviewerUsername { get; set; } 
        public string? RejectionReason { get; set; }
    }

    public static class StaticDb
    {
        public static List<User> Users = new List<User>();
        public static List<Claim> Claims = new List<Claim>();

        public static User AuthenticateUser(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public static void AddClaim(Claim claim)
        {
            Claims.Add(claim);
        }

        public static List<Claim> GetClaimsByLecturer(string lecturerUsername)
        {
            return Claims.Where(c => c.LecturerUsername == lecturerUsername).OrderByDescending(c => c.SubmittedDate).ToList();
        }

        public static List<Claim> GetPendingClaims()
        {
            return Claims.Where(c => c.Status == ClaimStatus.Pending).OrderBy(c => c.SubmittedDate).ToList();
        }

        public static List<Claim> GetClaimsForManagerReview()
        {
            return Claims.Where(c => c.Status == ClaimStatus.ApprovedByCoordinator).OrderBy(c => c.ReviewedDate).ToList();
        }

        public static Claim? GetClaimById(string claimId)
        {
            return Claims.FirstOrDefault(c => c.ClaimId == claimId); 
        }

        public static bool ReviewClaim(string claimId, string reviewerUsername, UserRole reviewerRole, bool isApproved, string? rejectionReason = null)
        {
            var claim = Claims.FirstOrDefault(c => c.ClaimId == claimId);

            if (claim == null || claim.Status == ClaimStatus.Rejected || claim.Status == ClaimStatus.ApprovedByManager)
            {
                return false; 
            }

            claim.ReviewedDate = DateTime.Now;
            claim.ReviewerUsername = reviewerUsername;

            if (isApproved)
            {
                if (reviewerRole == UserRole.ProgrammeCoordinator)
                {
                    claim.Status = ClaimStatus.ApprovedByCoordinator;
                }
                else if (reviewerRole == UserRole.AcademicManager)
                {
                    claim.Status = ClaimStatus.ApprovedByManager;
                }
            }
            else 
            {
                claim.Status = ClaimStatus.Rejected;
                claim.RejectionReason = rejectionReason;
            }

            return true;
        }
    }
}

//Reference List
//BillWagner (n.d.). Enumeration types - C# reference. [online] learn.microsoft.com. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum.
//dotnet-bot (2025). System.componentModel.DataAnnotations Namespace. [online] Microsoft.com. Available at: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.
//YouTube. (n.d.). ASP.NET Core MVC with Role-Based Authentication and Authorization. [online] Available at: http://www.youtube.com/playlist?list=PLTOaik4iw-qUzTx82QuAso1NktusoyJ9J.