using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG6212_POE.Models;
using System.Collections.Generic;
using System.Linq;

namespace PROG6212_POE.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestInitialize]
        public void Setup()
        {
            StaticDb.Users.Clear();
            StaticDb.Claims.Clear();

            StaticDb.Users.Add(new User { Username = "lecturer1", Password = "123", Role = UserRole.Lecturer });
            StaticDb.Users.Add(new User { Username = "pc1", Password = "123", Role = UserRole.ProgrammeCoordinator });
            StaticDb.Users.Add(new User { Username = "am1", Password = "123", Role = UserRole.AcademicManager });
        }


        [TestMethod]
        public void AuthenticateUser_ValidCredentials_ReturnsUser()
        {
            string username = "lecturer1";
            string password = "123";

            var user = StaticDb.AuthenticateUser(username, password);

            Assert.IsNotNull(user, "User should be found with valid credentials.");
            Assert.AreEqual(UserRole.Lecturer, user.Role, "The returned user should have the correct role.");
        }

        [TestMethod]
        public void AuthenticateUser_InvalidPassword_ReturnsNull()
        {
            string username = "lecturer1";
            string password = "wrongpassword";

            var user = StaticDb.AuthenticateUser(username, password);

            Assert.IsNull(user, "User should not be found with invalid password.");
        }


       
        [TestMethod]
        public void AddClaim_ClaimIsSubmitted_StatusIsPending()
        {
            var newClaim = new Claim
            {
                LecturerUsername = "lecturer1",
                HoursWorked = 10,
                HourlyRate = 250.00m
            };

            StaticDb.AddClaim(newClaim);
            var retrievedClaim = StaticDb.Claims.FirstOrDefault();

            Assert.IsNotNull(retrievedClaim, "Claim should be added to the database.");
            Assert.AreEqual(ClaimStatus.Pending, retrievedClaim.Status, "New claim status should be Pending.");
            Assert.AreEqual(2500.00m, retrievedClaim.TotalAmount, "Total amount should be calculated correctly.");
        }

        [TestMethod]
        public void ReviewClaim_CoordinatorApproves_StatusIsApprovedByCoordinator()
        {
            var claim = new Claim { LecturerUsername = "lecturer1", HoursWorked = 10, HourlyRate = 250.00m };
            StaticDb.AddClaim(claim);
            string coordinatorUsername = "pc1";

            bool success = StaticDb.ReviewClaim(claim.ClaimId, coordinatorUsername, UserRole.ProgrammeCoordinator, isApproved: true);

            Assert.IsTrue(success, "Review should be successful.");
            Assert.AreEqual(ClaimStatus.ApprovedByCoordinator, claim.Status, "Status should be ApprovedByCoordinator.");
            Assert.AreEqual(coordinatorUsername, claim.ReviewerUsername, "Reviewer username should be recorded.");
        }

        [TestMethod]
        public void ReviewClaim_ManagerApproves_StatusIsApprovedByManager()
        {
            var claim = new Claim { LecturerUsername = "lecturer1", HoursWorked = 10, HourlyRate = 250.00m };
            StaticDb.AddClaim(claim);

            StaticDb.ReviewClaim(claim.ClaimId, "pc1", UserRole.ProgrammeCoordinator, isApproved: true);

            string managerUsername = "am1";

            bool success = StaticDb.ReviewClaim(claim.ClaimId, managerUsername, UserRole.AcademicManager, isApproved: true);

            Assert.IsTrue(success, "Final review should be successful.");
            Assert.AreEqual(ClaimStatus.ApprovedByManager, claim.Status, "Final status should be ApprovedByManager.");
            Assert.AreEqual(managerUsername, claim.ReviewerUsername, "Manager username should be recorded.");
        }

        [TestMethod]
        public void ReviewClaim_CoordinatorRejects_StatusIsRejected()
        {
            var claim = new Claim { LecturerUsername = "lecturer1", HoursWorked = 10, HourlyRate = 250.00m };
            StaticDb.AddClaim(claim);
            string rejectionReason = "Missing supporting documents.";

            bool success = StaticDb.ReviewClaim(claim.ClaimId, "pc1", UserRole.ProgrammeCoordinator, isApproved: false, rejectionReason);

            Assert.IsTrue(success, "Rejection should be successful.");
            Assert.AreEqual(ClaimStatus.Rejected, claim.Status, "Status should be Rejected.");
            Assert.AreEqual(rejectionReason, claim.RejectionReason, "Rejection reason should be recorded.");
        }
    }
}