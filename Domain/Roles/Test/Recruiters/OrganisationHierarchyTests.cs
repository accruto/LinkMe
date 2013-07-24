using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    [TestClass]
    public class OrganisationHierarchyTests
        : OrganisationsTests
    {
        [TestMethod]
        public void TestSingleOrganisationHierarchy()
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());

            var expected = new OrganisationHierarchy
            {
                Organisation = organisation,
                ChildOrganisationHierarchies = new List<OrganisationHierarchy>()
            };

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(organisation.Id));
            AssertHierarchy(expected, _organisationsQuery.GetSubOrganisationHierarchy(organisation.Id));
        }

        [TestMethod]
        public void TestLinearOrganisationHierarchy()
        {
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, Guid.NewGuid());
            var grandchild = _organisationsCommand.CreateTestVerifiedOrganisation(3, child, Guid.NewGuid());

            var expected = new OrganisationHierarchy
            {
                Organisation = parent,
                ChildOrganisationHierarchies = new List<OrganisationHierarchy>
                {
                    new OrganisationHierarchy
                    {
                        Organisation = child,
                        ChildOrganisationHierarchies = new List<OrganisationHierarchy>
                        {
                            new OrganisationHierarchy
                            {
                                Organisation = grandchild,
                                ChildOrganisationHierarchies = new List<OrganisationHierarchy>()
                            }
                        }
                    }
                }
            };

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(parent.Id));
            AssertHierarchy(expected, _organisationsQuery.GetSubOrganisationHierarchy(parent.Id));

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(child.Id));
            AssertHierarchy(expected.ChildOrganisationHierarchies[0], _organisationsQuery.GetSubOrganisationHierarchy(child.Id));

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(grandchild.Id));
            AssertHierarchy(expected.ChildOrganisationHierarchies[0].ChildOrganisationHierarchies[0], _organisationsQuery.GetSubOrganisationHierarchy(grandchild.Id));
        }

        [TestMethod]
        public void TestTreeOrganisationHierarchy()
        {
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            var child1 = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, Guid.NewGuid());
            var child2 = _organisationsCommand.CreateTestVerifiedOrganisation(3, parent, Guid.NewGuid());
            var grandchild1 = _organisationsCommand.CreateTestVerifiedOrganisation(4, child1, Guid.NewGuid());
            var grandchild2 = _organisationsCommand.CreateTestVerifiedOrganisation(5, child1, Guid.NewGuid());
            var grandchild3 = _organisationsCommand.CreateTestVerifiedOrganisation(6, child2, Guid.NewGuid());

            var expected = new OrganisationHierarchy
            {
                Organisation = parent,
                ChildOrganisationHierarchies = new List<OrganisationHierarchy>
                {
                    new OrganisationHierarchy
                    {
                        Organisation = child1,
                        ChildOrganisationHierarchies = new List<OrganisationHierarchy>
                        {
                            new OrganisationHierarchy
                            {
                                Organisation = grandchild1,
                                ChildOrganisationHierarchies = new List<OrganisationHierarchy>()
                            },
                            new OrganisationHierarchy
                            {
                                Organisation = grandchild2,
                                ChildOrganisationHierarchies = new List<OrganisationHierarchy>()
                            }
                        }
                    },
                    new OrganisationHierarchy
                    {
                        Organisation = child2,
                        ChildOrganisationHierarchies = new List<OrganisationHierarchy>
                        {
                            new OrganisationHierarchy
                            {
                                Organisation = grandchild3,
                                ChildOrganisationHierarchies = new List<OrganisationHierarchy>()
                            },
                        }
                    }
                }
            };

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(parent.Id));
            AssertHierarchy(expected, _organisationsQuery.GetSubOrganisationHierarchy(parent.Id));

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(child1.Id));
            var child1Hierarchy = (from c in expected.ChildOrganisationHierarchies where c.Organisation.Id == child1.Id select c).Single();
            AssertHierarchy(child1Hierarchy, _organisationsQuery.GetSubOrganisationHierarchy(child1.Id));

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(child2.Id));
            var child2Hierarchy = (from c in expected.ChildOrganisationHierarchies where c.Organisation.Id == child2.Id select c).Single();
            AssertHierarchy(child2Hierarchy, _organisationsQuery.GetSubOrganisationHierarchy(child2.Id));

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(grandchild1.Id));
            var grandchild1Hierarchy = (from c in child1Hierarchy.ChildOrganisationHierarchies where c.Organisation.Id == grandchild1.Id select c).Single();
            AssertHierarchy(grandchild1Hierarchy, _organisationsQuery.GetSubOrganisationHierarchy(grandchild1.Id));

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(grandchild2.Id));
            var grandchild2Hierarchy = (from c in child1Hierarchy.ChildOrganisationHierarchies where c.Organisation.Id == grandchild2.Id select c).Single();
            AssertHierarchy(grandchild2Hierarchy, _organisationsQuery.GetSubOrganisationHierarchy(grandchild2.Id));

            AssertHierarchy(expected, _organisationsQuery.GetOrganisationHierarchy(grandchild3.Id));
            var grandchild3Hierarchy = (from c in child2Hierarchy.ChildOrganisationHierarchies where c.Organisation.Id == grandchild3.Id select c).Single();
            AssertHierarchy(grandchild3Hierarchy, _organisationsQuery.GetSubOrganisationHierarchy(grandchild3.Id));
        }

        private static void AssertHierarchy(OrganisationHierarchy expectedHierarchy, OrganisationHierarchy organisationHierarchy)
        {
            Assert.AreEqual(expectedHierarchy.Organisation.Id, organisationHierarchy.Organisation.Id);
            Assert.AreEqual(expectedHierarchy.ChildOrganisationHierarchies.Count, organisationHierarchy.ChildOrganisationHierarchies.Count);
            for (var index = 0; index < expectedHierarchy.ChildOrganisationHierarchies.Count; ++index)
                AssertHierarchy(expectedHierarchy.ChildOrganisationHierarchies[index], organisationHierarchy.ChildOrganisationHierarchies[index]);
        }
    }
}