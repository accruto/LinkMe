using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
{
    [TestClass]
    public class FacetTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void IndustriesFacetTest()
        {
            var industry1 = new Industry { Id = Guid.NewGuid(), Name = "Industry1" };
            var industry2 = new Industry { Id = Guid.NewGuid(), Name = "Industry2" };
            var industry3 = new Industry { Id = Guid.NewGuid(), Name = "Industry3" };

            const int jobCount = 50;

            for (var i = 1; i <= jobCount; i++)
            {
                var jobAd = new JobAd
                                 {
                                     Id = Guid.NewGuid(),
                                     Status = JobAdStatus.Open,
                                     Title = string.Format("Job Ad {0}",i),
                                     Description =
                                                          {
                                                              BulletPoints =
                                                                  new[]
                                                                      {
                                                                          "good verbal communication",
                                                                          "self management and independency",
                                                                      },
                                                              Content =
                                                                  "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                                                              JobTypes = JobTypes.FullTime,
                                                              Salary =
                                                                  new Salary
                                                                      {
                                                                          LowerBound = 20000,
                                                                          UpperBound = 40000,
                                                                          Rate = SalaryRate.Year,
                                                                          Currency = Currency.AUD
                                                                      },
                                                              Industries = new List<Industry> { industry1 }  //everyone gets industry1

                                                          }
                                 };

                //every second one gets industry2
                if (i % 2 == 0)
                    jobAd.Description.Industries.Add(industry2);

                //every third one gets industry3
                if (i % 3 == 0)
                    jobAd.Description.Industries.Add(industry3);

                //override the content on the last half of the ads
                if (i <= jobCount / 2)
                    jobAd.Description.Content = "Those medals you wear on your moth-eaten chest should be there for bungling at which you are best.";

                IndexJobAd(jobAd, "LinkMe");
            }

            // Search without filter.

            var jobQuery = new JobAdSearchQuery();
            var results = Search(jobQuery);
            Assert.AreEqual(jobCount, results.JobAdIds.Count);
            Assert.AreEqual(jobCount, results.IndustryHits.FirstOrDefault(h => h.Key == industry1.Id).Value);
            Assert.AreEqual(jobCount / 2, results.IndustryHits.FirstOrDefault(h => h.Key == industry2.Id).Value);
            Assert.AreEqual(jobCount / 3, results.IndustryHits.FirstOrDefault(h => h.Key == industry3.Id).Value);

            // search on specific term

            jobQuery = new JobAdSearchQuery { Keywords = Expression.ParseExactPhrase("medals you wear") };
            results = Search(jobQuery);
            Assert.AreEqual(jobCount / 2, results.JobAdIds.Count);
            Assert.AreEqual(jobCount / 2, results.IndustryHits.FirstOrDefault(h => h.Key == industry1.Id).Value);
            Assert.AreEqual(jobCount / 4, results.IndustryHits.FirstOrDefault(h => h.Key == industry2.Id).Value);
            Assert.AreEqual(jobCount / 6, results.IndustryHits.FirstOrDefault(h => h.Key == industry3.Id).Value);


        }

        [TestMethod]
        public void JobTypesFacetTest()
        {
            const int jobCount = 50;

            for (var i = 1; i <= jobCount; i++)
            {
                var jobAd = new JobAd
                {
                    Id = Guid.NewGuid(),
                    Status = JobAdStatus.Open,
                    Title = string.Format("Job Ad {0}", i),
                    Description =
                    {
                        BulletPoints =
                            new[]
                                  {
                                      "good verbal communication",
                                      "self management and independency",
                                  },
                        Content =
                            "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                        JobTypes = JobTypes.FullTime,   //everyone wants full time work
                        Salary =
                            new Salary
                            {
                                LowerBound = 20000,
                                UpperBound = 40000,
                                Rate = SalaryRate.Year,
                                Currency = Currency.AUD
                            },
                        Industries = new List<Industry> { _industriesQuery.GetIndustry("Engineering") },
                    }
                };

                //every second wants contract
                if (i % 2 == 0)
                    jobAd.Description.JobTypes |= JobTypes.Contract;

                //every third wants jobShare
                if (i % 3 == 0)
                    jobAd.Description.JobTypes |= JobTypes.JobShare;

                //override the first 10 with 'all'
                if (i <= 10)
                    jobAd.Description.JobTypes = JobTypes.All;

                //override the content on the last half of the ads
                if (i <= jobCount / 2)
                    jobAd.Description.Content = "Those medals you wear on your moth-eaten chest should be there for bungling at which you are best.";

                IndexJobAd(jobAd, "LinkMe");
            }

            // Search without filter.

            var jobQuery = new JobAdSearchQuery();
            var results = Search(jobQuery);
            Assert.AreEqual(jobCount, results.JobAdIds.Count);

            
            Assert.AreEqual(jobCount, results.JobTypeHits.FirstOrDefault(h => h.Key == JobTypes.FullTime).Value);
            Assert.AreEqual((jobCount - 10)/ 2 + 10, results.JobTypeHits.FirstOrDefault(h => h.Key == JobTypes.Contract).Value);
            Assert.AreEqual((jobCount - 10)/ 3 + 10 , results.JobTypeHits.FirstOrDefault(h => h.Key == JobTypes.JobShare).Value);

            // search on specific term

            jobQuery = new JobAdSearchQuery { Keywords = Expression.ParseExactPhrase("medals you wear") };
            results = Search(jobQuery);
            Assert.AreEqual(jobCount / 2, results.JobAdIds.Count);
            Assert.AreEqual(jobCount / 2, results.JobTypeHits.FirstOrDefault(h => h.Key == JobTypes.FullTime).Value);
            Assert.AreEqual(((jobCount / 2) - 10) / 2 + 10, results.JobTypeHits.FirstOrDefault(h => h.Key == JobTypes.Contract).Value);
            Assert.AreEqual(((jobCount / 2) - 10) / 3 + 10, results.JobTypeHits.FirstOrDefault(h => h.Key == JobTypes.JobShare).Value);
        }
    }
}
