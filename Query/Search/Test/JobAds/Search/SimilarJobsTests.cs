using LinkMe.Domain;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Search
{
    [TestClass]
    public class SimilarJobsTests
        : ExecuteJobAdSearchTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        [TestMethod]
        public void TestSimilarJobs()
        {
            var jobPoster = CreateJobPoster();

            // Various industries.

            var jobAd0 = CreateJobAd(jobPoster);
            var jobAd1 = CreateJobAd(jobPoster);
            var jobAd2 = CreateJobAd(jobPoster);

            // Search.

            // How to get some similar jobs.  All combinations at the moment elude me ...

            var results = _executeJobAdSearchCommand.SearchSimilar(null, jobAd2.Id, null);
        }

        private JobAd CreateJobAd(JobPoster jobPoster)
        {
            return CreateJobAd(
                jobPoster,
                0,
                j =>
                {
                    j.Description.Content = "Centrally located in the heart of Melbourne CBD, Advanced Training provides a wide range technical and desktop application training to Government, Corporate and SME clients throughout Australia<br /><br />Reporting directly to the Training Manager, You would ideally have the following:<br /><br />Qualifications &amp; Education:<br />* MCT, Cert IV in Workplace Training and Assessment or TAA, Dip Ed<br />* MCITP, MCSE 2003, MCAD/MCSD, or MCDBA<br />* CCNA, Linux, Citrix, VM Ware looked upon favourably<br /><br />Required Skills:<br />* Either/OR in Microsoft Networking / .Net programming / SQL 2000/2005<br />* Excellent interpersonal communication skills<br />* Technical ability in varied environments<br />* Enthusiasm<br /><br />Duties &amp; Responsibilities::<br />The position involves the following activities:<br />* Facilitation of networking, software development or database courses <br />* Post-course customer support<br />* Assisting sales team with client needs analysis<br /><br />If you are either a talented IT Trainer or an IT Certified Professional with significant industry experience with a desire to teach, then this is an outstanding opportunity for those looking for that next challenge in their careers.<br />If you are looking for a great career opportunity and enjoy working with people please email your CV to kristy.mclean@itfutures.net.au";
                    j.Title = "IT Trainer - MCSE / MCSA";
                    j.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Norlane VIC 3214");
                    j.Description.Salary = new Salary { LowerBound = 80000, UpperBound = 120000, Currency = Currency.AUD, Rate = SalaryRate.Year };
                });
        }
    }
}
