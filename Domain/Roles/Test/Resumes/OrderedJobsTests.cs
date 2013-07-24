using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Resumes
{
    [TestClass]
    public class OrderedJobsTests
    {
        [TestMethod]
        public void TestOrderedJobs()
        {
            TestTimeOrderedJobs(DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), true);
            TestTimeOrderedJobs(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-3), false);
            TestTimeOrderedJobs(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1), true);
            TestTimeOrderedJobs(DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-3), false);

            TestTimeOrderedJobs(DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), true);
            TestTimeOrderedJobs(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1), false);

            // Current jobs come first.

            TestTimeOrderedJobs(DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-2), null, true);
            TestTimeOrderedJobs(DateTime.Now.AddDays(-2), null, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-3), false);

            TestTimeOrderedJobs(DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1), null, DateTime.Now.AddDays(-1), true);
            TestTimeOrderedJobs(null, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1), false);

            TestTimeOrderedJobs(DateTime.Now.AddDays(-4), null, DateTime.Now.AddDays(-2), null, true);
            TestTimeOrderedJobs(DateTime.Now.AddDays(-2), null, DateTime.Now.AddDays(-4), null, false);
        }

        [TestMethod]
        public void TestCurrentJobs()
        {
            TestCurrentJobs(2, null, null);
            TestCurrentJobs(1, null, DateTime.Now);
            TestCurrentJobs(1, DateTime.Now, null);
            TestCurrentJobs(0, DateTime.Now, DateTime.Now);
        }

        private static void TestCurrentJobs(int expectedCount, params DateTime?[] endDates)
        {
            var resume = new Resume
            {
                Jobs = (from e in endDates
                        select new Job
                        {
                            Dates = e == null
                                ? new PartialDateRange()
                                : new PartialDateRange(null, new PartialDate(e.Value))
                        }).ToList()
            };

            Assert.AreEqual(expectedCount, resume.CurrentJobs.Count);
        }

        private static void TestTimeOrderedJobs(DateTime? start1, DateTime? end1, DateTime? start2, DateTime? end2, bool switched)
        {
            var jobs = new List<Job>
            {
                new Job
                {
                    Id = Guid.NewGuid(),
                    Dates = start1 == null
                        ? (end1 == null ? new PartialDateRange() : new PartialDateRange(null, new PartialDate(end1.Value)))
                        : (end1 == null ? new PartialDateRange(new PartialDate(start1.Value)) : new PartialDateRange(new PartialDate(start1.Value), new PartialDate(end1.Value)))
                },
                new Job
                {
                    Id = Guid.NewGuid(),
                    Dates = start2 == null
                        ? (end2 == null ? new PartialDateRange() : new PartialDateRange(null, new PartialDate(end2.Value)))
                        : (end2 == null ? new PartialDateRange(new PartialDate(start2.Value)) : new PartialDateRange(new PartialDate(start2.Value), new PartialDate(end2.Value)))
                },
            };

            var orderedJobs = new Resume { Jobs = jobs }.Jobs;
            if (switched)
            {
                Assert.AreEqual(jobs[0].Id, orderedJobs[1].Id);
                Assert.AreEqual(jobs[1].Id, orderedJobs[0].Id);
            }
            else
            {
                Assert.AreEqual(jobs[0].Id, orderedJobs[0].Id);
                Assert.AreEqual(jobs[1].Id, orderedJobs[1].Id);
            }
        }
    }
}