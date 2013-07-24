using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Resumes
{
    [TestClass]
    public class OrderedSchoolsTests
    {
        [TestMethod]
        public void TestOrderedSchools()
        {
            // Current schools come first, then more recent schools.

            TestTimeOrderedSchools(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1), true);
            TestTimeOrderedSchools(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-3), false);
            TestTimeOrderedSchools(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1), false);

            TestTimeOrderedSchools(DateTime.Now.AddDays(-3), null, true);
            TestTimeOrderedSchools(null, DateTime.Now.AddDays(-3), false);

            TestTimeOrderedSchools(null, null, false);
        }

        private static void TestTimeOrderedSchools(DateTime? end1, DateTime? end2, bool switched)
        {
            var schools = new List<School>
            {
                new School {Id = Guid.NewGuid(), CompletionDate = end1 == null ? new PartialCompletionDate() : new PartialCompletionDate(new PartialDate(end1.Value))},
                new School {Id = Guid.NewGuid(), CompletionDate = end2 == null ? new PartialCompletionDate() : new PartialCompletionDate(new PartialDate(end2.Value))},
            };

            var orderedSchools = new Resume { Schools = schools }.Schools;
            if (switched)
            {
                Assert.AreEqual(schools[0].Id, orderedSchools[1].Id);
                Assert.AreEqual(schools[1].Id, orderedSchools[0].Id);
            }
            else
            {
                Assert.AreEqual(schools[0].Id, orderedSchools[0].Id);
                Assert.AreEqual(schools[1].Id, orderedSchools[1].Id);
            }
        }
    }
}