using System;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    [TestClass]
    public class CircularHierarchyTests
        : OrganisationsTests
    {
        [TestMethod]
        public void TestSelfAsParent()
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            Test(organisation, organisation);
        }

        [TestMethod]
        public void TestChildAsParent()
        {
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, Guid.NewGuid());
            Test(parent, child);
        }

        [TestMethod]
        public void TestGrandChildAsParent()
        {
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, Guid.NewGuid());
            var grandChild = _organisationsCommand.CreateTestVerifiedOrganisation(3, child, Guid.NewGuid());
            Test(parent, grandChild);
        }

        private void Test(Organisation organisation, Organisation parentOrganisation)
        {
            try
            {
                organisation.SetParent(parentOrganisation);
                _organisationsCommand.UpdateOrganisation(organisation);

                Assert.Fail("Should have been an exception.");
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.IsInstanceOfType(ex.Errors[0], typeof(CircularValidationError));
                Assert.AreEqual("Parent", ex.Errors[0].Name);
            }

        }
    }
}
