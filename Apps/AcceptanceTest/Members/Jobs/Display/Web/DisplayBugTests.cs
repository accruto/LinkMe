using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web
{
    [TestClass]
    public class DisplayBugTests
        : DisplayTests
    {
        [TestMethod, Description("Bug 11726")]
        public void TestCustomizedBulletPoints()
        {
            // The error arises because this user's organisation has a CSS file.

            var organisation = new Organisation { Id = new Guid("D800B959-A0B5-41E0-83E2-A33B4A748EC1"), Name = "Organisation" };
            _organisationsCommand.CreateOrganisation(organisation);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = "Senior Oracle Developer",
                LogoId = null,
                Integration = new JobAdIntegration
                {
                    ExternalReferenceId  = "NB1190",
                    ExternalApplyUrl = "http://google.com",
                },
                Description = new JobAdDescription
                {
                    BulletPoints = null,
                    Content = "Require a dynamic and passionate person with a strong developer background including backend and front end experience.     Your advanced Oracle skills will be tested to the limit as you provide new insights and mentoring to our Oracle Certified Practitioner (OCP) level trained developers.     You'll need comprehensive Oracle experience for this senior position with the following desirable skills:      * practical experience writing complex SQL queries    * practical experience implementing complex PL/SQL programs    * diagnostics and performance tuning skills    * systems analysis skills and knowledge    * programming design and implementation patterns         Experience with UI/presentation layer frameworks and technologies     You will have programming experience from Procedural or Object Orientated programming languages:     Such as,  JAVA, C, C++, C#, BASIC, PASCAL, COBOL, PL/SQL, Assembler,     E-R Data modelling experience     physical data design and implementation experience     XML, xpath     Oracle 10g & ideally 11 experience      All applicants must be Australian Citizens and have the ability to obtain a Government Security Clearance or be currently already cleared.      If you believe you have the experience and the determination to be successful in this position and for more information on this role, please contact Nick Biziak at Balance Recruitment on (02) 6198 3369 or email your CV to nbiziak@balancerecruitment.com.au quoting ref 1190 or apply below",
                    CompanyName = null,
                    JobTypes = (JobTypes) 4,
                    Location = null,
                    Salary = null,
                    Package = null,
                    ResidencyRequired = true,
                    Summary = "Require a dynamic and passionate person with a strong developer background including backend & front end experience. Contract & Permanent Opportunity",
                    PositionTitle = null,
                }
            };

            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);

            Get(GetJobUrl(jobAd.Id));
            AssertPageContains(jobAd.Title);
        }
    }
}
