using System;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    [TestClass]
    public class OrganisationNameTests
        : OrganisationsTests
    {
        private const string UnverifiedName = "Unverified Company";
        private const string VerifiedName = "Verified Company";

        [TestMethod]
        public void TestUnverifiedSameName()
        {
            // Should be possible to create unverified organisations with the same name.

            var unverified1 = new Organisation { Name = UnverifiedName };
            _organisationsCommand.CreateOrganisation(unverified1);
            var unverified2 = new Organisation { Name = UnverifiedName };
            _organisationsCommand.CreateOrganisation(unverified2);

            Assert.AreNotEqual(Guid.Empty, unverified1.Id);
            Assert.AreNotEqual(Guid.Empty, unverified2.Id);
            Assert.AreNotEqual(unverified1.Id, unverified2.Id);
        }

        [TestMethod]
        public void TestUniqueVerifiedNoParent()
        {
            // Should not be possible to create verified organisations with the same name.

            var verified1 = new VerifiedOrganisation { Name = VerifiedName };
            _organisationsCommand.CreateOrganisation(verified1);
            var verified2 = new VerifiedOrganisation { Name = VerifiedName };

            try
            {
                _organisationsCommand.CreateOrganisation(verified2);
                Assert.Fail("Expected an exception");
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.IsInstanceOfType(ex.Errors[0], typeof(DuplicateValidationError));
            }
        }

        [TestMethod]
        public void TestVerifiedSameNameDifferentParents()
        {
            // Should be possible to create verified organisations with the same name but different parents.

            var adminId = Guid.NewGuid();
            var parent1 = new VerifiedOrganisation{Name = "Parent1", AccountManagerId = adminId, VerifiedById = adminId};
            _organisationsCommand.CreateOrganisation(parent1);
            var parent2 = new VerifiedOrganisation { Name = "Parent2", AccountManagerId = adminId, VerifiedById = adminId };
            _organisationsCommand.CreateOrganisation(parent2);

            var verified1 = new VerifiedOrganisation { Name = VerifiedName, AccountManagerId = adminId, VerifiedById = adminId };
            verified1.SetParent(parent1);
            _organisationsCommand.CreateOrganisation(verified1);

            var verified2 = new VerifiedOrganisation { Name = VerifiedName, AccountManagerId = adminId, VerifiedById = adminId };
            verified2.SetParent(parent2);
            _organisationsCommand.CreateOrganisation(verified2);

            Assert.AreNotEqual(Guid.Empty, verified1.Id);
            Assert.AreNotEqual(Guid.Empty, verified2.Id);
            Assert.AreNotEqual(verified1.Id, verified2.Id);
        }

        [TestMethod]
        public void TestUniqueVerifiedParent()
        {
            // Should not be possible to create verified organisations with the same name and same parent.

            var adminId = Guid.NewGuid();
            var parent = new VerifiedOrganisation { Name = "Parent", AccountManagerId = adminId, VerifiedById = adminId };
            _organisationsCommand.CreateOrganisation(parent);

            var verified1 = new VerifiedOrganisation { Name = VerifiedName, AccountManagerId = adminId, VerifiedById = adminId };
            verified1.SetParent(parent);
            _organisationsCommand.CreateOrganisation(verified1);
            var verified2 = new VerifiedOrganisation { Name = VerifiedName, AccountManagerId = adminId, VerifiedById = adminId };
            verified2.SetParent(parent);

            try
            {
                _organisationsCommand.CreateOrganisation(verified2);
                Assert.Fail("Expected an exception");
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.IsInstanceOfType(ex.Errors[0], typeof(DuplicateValidationError));
            }
        }

        [TestMethod]
        public void TestUnverifiedFullNames()
        {
            var unverified = new Organisation {Name = UnverifiedName};
            _organisationsCommand.CreateOrganisation(unverified);

            Assert.AreEqual(UnverifiedName, unverified.Name);
            Assert.AreEqual(UnverifiedName, unverified.FullName);
        }

        [TestMethod]
        public void TestVerifiedFullNames()
        {
            var verifiedName = VerifiedName;

            var adminId = Guid.NewGuid();
            var verified = new VerifiedOrganisation { Name = verifiedName, VerifiedById = adminId, AccountManagerId = adminId };
            _organisationsCommand.CreateOrganisation(verified);

            Assert.AreEqual(verifiedName, verified.Name);
            Assert.AreEqual(verifiedName, verified.FullName);

            // Set up parent and child organisations.

            var parentName = "Parent Company";
            var childName = "Child Company";

            var parent = new VerifiedOrganisation { Name = parentName, VerifiedById = adminId, AccountManagerId = adminId };
            _organisationsCommand.CreateOrganisation(parent);
            var child = new VerifiedOrganisation { Name = childName, VerifiedById = adminId, AccountManagerId = adminId };
            _organisationsCommand.CreateOrganisation(child);

            // Parent => Verified => Child.

            verified.SetParent(parent);
            _organisationsCommand.UpdateOrganisation(verified);

            child.SetParent(verified);
            _organisationsCommand.UpdateOrganisation(child);

            // Test the full names.

            Assert.AreEqual(parentName, parent.Name);
            Assert.AreEqual(parentName, parent.FullName);

            Assert.AreEqual(verifiedName, verified.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName, verified.FullName);

            Assert.AreEqual(childName, child.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName + Organisation.FullNameSeparator + childName, child.FullName);
            
            // Re-read and make sure they still work.

            parent = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(parent.Id);
            verified = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(verified.Id);
            child = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(child.Id);

            Assert.AreEqual(parentName, parent.Name);
            Assert.AreEqual(parentName, parent.FullName);

            Assert.AreEqual(verifiedName, verified.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName, verified.FullName);

            Assert.AreEqual(childName, child.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName + Organisation.FullNameSeparator + childName, child.FullName);

            // Change some names

            childName = "Child Changed";
            child.Name = childName;
            Assert.AreEqual(childName, child.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName + Organisation.FullNameSeparator + childName, child.FullName);

            verifiedName = "Verified Changed";
            verified.Name = verifiedName;
            Assert.AreEqual(verifiedName, verified.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName, verified.FullName);

            parentName = "Parent Changed";
            parent.Name = parentName;
            Assert.AreEqual(parentName, parent.Name);
            Assert.AreEqual(parentName, parent.FullName);

            // Update.

            _organisationsCommand.UpdateOrganisation(parent);
            _organisationsCommand.UpdateOrganisation(verified);
            _organisationsCommand.UpdateOrganisation(child);
            parent = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(parent.Id);
            verified = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(verified.Id);
            child = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(child.Id);

            Assert.AreEqual(parentName, parent.Name);
            Assert.AreEqual(parentName, parent.FullName);

            Assert.AreEqual(verifiedName, verified.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName, verified.FullName);

            Assert.AreEqual(childName, child.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName + Organisation.FullNameSeparator + childName, child.FullName);

            // Change the parent to another company

            child.SetParent(parent);
            Assert.AreEqual(childName, child.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + childName, child.FullName);

            _organisationsCommand.UpdateOrganisation(child);
            parent = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(parent.Id);
            verified = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(verified.Id);
            child = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(child.Id);

            Assert.AreEqual(parentName, parent.Name);
            Assert.AreEqual(parentName, parent.FullName);

            Assert.AreEqual(verifiedName, verified.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + verifiedName, verified.FullName);

            Assert.AreEqual(childName, child.Name);
            Assert.AreEqual(parentName + Organisation.FullNameSeparator + childName, child.FullName);
        }

        [TestMethod]
        public void TestBracketsName()
        {
            const string name = "Database Consultants Australia (VIC)";
            var organisation = new Organisation { Name = name };
            _organisationsCommand.CreateOrganisation(organisation);

            organisation = _organisationsQuery.GetOrganisation(organisation.Id);
            Assert.AreEqual(name, organisation.Name);
        }
    }
}