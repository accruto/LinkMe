using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using LinkMe.Apps.Employers.Test;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Common.Integration;
using LinkMe.Common.Managers.Security;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Helpers;
using LinkMe.Web.Jobs;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Common.JobBoard;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Common.Test;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Employers;
using LinkMe.Web.Service;
using LinkMe.Web.UI.Unregistered.Common;
using NUnit.Framework;

namespace LinkMe.AcceptanceTest.service
{
    [TestFixture]
    public class GetJobAdsTest : WebFormDataTestCase
    {
        private readonly IIntegrationCommand _integrationCommand = Container.Current.Resolve<IIntegrationCommand>();
        private JobAd _jobAd;
        private Employer _employer;
        private IntegratorUser _integratorUser;

        protected override void SetUp()
        {
            base.SetUp();

            ClearSearchIndexes();
            _integratorUser = _integrationCommand.CreateTestIntegratorUser();

            const string testUserId = "employer";
            _employer = _employersCommand.CreateTestEmployer(testUserId, _organisationsCommand.CreateTestOrganisation(0));

            _jobAd = _employer.CreateTestJobAd();
            _jobAd.JobDescription.Summary = "Summary";
            _jobAd.Status = JobAdStatus.Open;
            _jobAd.JobDescription.Industries.Clear();
            _jobAd.JobDescription.Industries.Add(Container.Current.Resolve<IIndustriesCommand>().GetIndustries()[0]);
            _jobAdsCommand.PostJobAd(_jobAd);
        }

        [Test]
        public void TestJobAdFeed()
        {
            //Changing to upper case to test that webservice is case-insensetive
            string response = CallService(GetJobAds.IndustriesParameter,
                ((Industry)_jobAd.JobDescription.Industries[0]).Name.ToUpper());

            var netJobAd = GetJobAdFeedFromResponse(response);
            var viewJobAdUrl = NavigationManager.GetUrlForPage<Job>(Job.JobAdIdParam, _jobAd.Id.ToString());
            var applyJobAdUrl = NavigationManager.GetUrlForPage<JobApplicationSignInForm>(Job.JobAdIdParam, _jobAd.Id.ToString());
            var localJobAd = _jobAd.Map(_employer, viewJobAdUrl.AbsoluteUri.ToLower(), applyJobAdUrl.AbsoluteUri.ToLower());
            CompareJobAdFeed(localJobAd, netJobAd);
        }

        [Test]
        public void TestCareerOneJobAdNotReturned()
        {
            // Create a "normal" job ad and make sure it's returned

            var careerOneJobAd = _employer.CreateTestJobAd();
            careerOneJobAd.Status = JobAdStatus.Open;
            careerOneJobAd.JobDescription.Industries.Clear();
            careerOneJobAd.JobDescription.Industries.Add(Container.Current.Resolve<IIndustriesCommand>().GetIndustries()[1]);
            _jobAdsCommand.PostJobAd(careerOneJobAd);

            string response = CallService(GetJobAds.IndustriesParameter,
                ((Industry)careerOneJobAd.JobDescription.Industries[0]).Name);

            var netJobAd = GetJobAdFeedFromResponse(response);
            AssertNotNull(netJobAd);
            AssertEquals(careerOneJobAd.Id, netJobAd.Id);

            // Now make it a CareerOne job ad and make sure it's no longer returned.

            careerOneJobAd.IntegratorUserId = _integrationCommand.CreateTestIntegratorUser(IntegrationHelper.CareerOneIntegratorUsername, "password", IntegratorPermissions.None).Id;
            _jobAdsCommand.UpdateJobAd(careerOneJobAd);

            response = CallService(GetJobAds.IndustriesParameter,
                ((Industry)careerOneJobAd.JobDescription.Industries[0]).Name);

            netJobAd = GetJobAdFeedFromResponse(response);
            AssertNull(netJobAd);
        }

        private void CompareJobAdFeed(JobAdFeedElement localJobAd, JobAdFeedElement netJobAd)
        {
            AssertEquals(netJobAd.Title, localJobAd.Title);
            AssertEquals(netJobAd.PositionTitle, localJobAd.PositionTitle);
            AssertEquals(netJobAd.BulletPoints.Length, localJobAd.BulletPoints.Length);

            int i = 0;
            foreach (string bp in netJobAd.BulletPoints)
            {
                AssertEquals(bp, localJobAd.BulletPoints[i++]);
            }
            AssertEquals(netJobAd.Summary, localJobAd.Summary);
            AssertEquals(netJobAd.Content, localJobAd.Content);
            AssertEquals(netJobAd.EmployerCompanyName, localJobAd.EmployerCompanyName);
            AssertEquals(netJobAd.JobTypes, localJobAd.JobTypes);
            AssertEquals(netJobAd.Location, localJobAd.Location);
            AssertEquals(netJobAd.Postcode, localJobAd.Postcode);
            AssertEquals(netJobAd.Salary.HasLowerBound, localJobAd.Salary.HasLowerBound);
            AssertEquals(netJobAd.Salary.HasUpperBound, localJobAd.Salary.HasUpperBound);
            AssertEquals(netJobAd.Salary.IsEmpty, localJobAd.Salary.IsEmpty);
            AssertEquals(netJobAd.Salary.LowerBound, localJobAd.Salary.LowerBound);
            AssertEquals(netJobAd.Salary.UpperBound, localJobAd.Salary.UpperBound);
            AssertEquals(netJobAd.PackageDetails, localJobAd.PackageDetails);
            AssertEquals(netJobAd.ResidencyRequired, localJobAd.ResidencyRequired);
            AssertEquals(netJobAd.ContactDetails.FirstName, localJobAd.ContactDetails.FirstName);
            AssertEquals(netJobAd.ContactDetails.LastName, localJobAd.ContactDetails.LastName);
            AssertEquals(netJobAd.ContactDetails.EmailAddress, localJobAd.ContactDetails.EmailAddress);
            AssertEquals(netJobAd.ContactDetails.FaxNumber, localJobAd.ContactDetails.FaxNumber);
            AssertEquals(netJobAd.ContactDetails.PhoneNumber, localJobAd.ContactDetails.PhoneNumber);
            AssertEquals(netJobAd.Industries.Count, localJobAd.Industries.Count);
            i = 0;
            foreach (var industry in netJobAd.Industries)
            {
                AssertEquals(localJobAd.Industries[i++], industry);
            }
            AssertEquals(netJobAd.ViewJobAdUrl, localJobAd.ViewJobAdUrl);
            AssertEquals(netJobAd.ViewJobAdUrl, String.Format("{0}jobs/job.aspx?jobadid={1}",
                                                            new ApplicationUrl("~/").AbsoluteUri, netJobAd.Id));

            AssertEquals(netJobAd.ApplyJobAdUrl, localJobAd.ApplyJobAdUrl);
            AssertEquals(netJobAd.ApplyJobAdUrl, String.Format("{0}ui/unregistered/common/jobapplicationsigninform.aspx?jobadid={1}",
                                                              new ApplicationUrl("~/").AbsoluteUri, netJobAd.Id));

            // Not set in the original post.

            AssertEquals(null, netJobAd.RecruiterCompanyName);
        }

        private JobAdFeedElement GetJobAdFeedFromResponse(string response)
        {
            ThrowIfContainsServerError(response);

            XmlSerializer deser = new XmlSerializer(typeof(JobAdFeedElement), null, new Type[]{}, null, LinkMe.Apps.Services.Constants.XmlSerializationNamespace);

            XmlReader reader = XmlReader.Create(new StringReader(response));
            reader.ReadStartElement();
            var netJobAd = (JobAdFeedElement)deser.Deserialize(reader);
            reader.Close();

            return netJobAd;
        }

        [Test]
        public void TestConnectivity()
        {
            const string wrongUsername = "blahuser";

            string[] parameters = new string[] {  GetJobAds.IndustriesParameter, "accounting",
                GetJobAds.ModifiedSinceParameter, "2007-01-01T00:00:00" };

            //No username-password

            string response = CallIntegrationGetService<GetJobAds>(null, null, parameters);
            string[] errors = GetErrorsFromResponse(response);
            CheckErrors(errors, WebServiceSecurityManager.NoUserNameError);

            //Wrong username-password
            response = CallIntegrationGetService<GetJobAds>(wrongUsername, "blahpassword", parameters);
            errors = GetErrorsFromResponse(response);
            CheckErrors(errors, String.Format(WebServiceSecurityManager.UnknownUserError, wrongUsername));

            //Correct username-password

            response = CallService(parameters);
            errors = GetErrorsFromResponse(response);
            Assert(errors.Length == 0);
        }

        [Test]
        public void TestIndustries()
        {
            string[] parameters = new string[4];
            parameters[0] = GetJobAds.IndustriesParameter;
            parameters[1] = "accounting|retards|idiots";
            parameters[2] = GetJobAds.ModifiedSinceParameter;
            parameters[3] = "2007-01-01T00:00:00";

            //Check non-existing industry

            string response = CallService(parameters);
            string[] errors = GetErrorsFromResponse(response);
            CheckErrors(errors, String.Format("Specified industry '{0}' is unknown", "retards"));

            //Check industry with no jobads

            parameters[1] = Container.Current.Resolve<IIndustriesCommand>().GetIndustries()[1].Name.ToLower();

            response = CallService(parameters);

            XPathDocument doc = new XPathDocument(new StringReader(response));
            XPathNavigator nav = doc.CreateNavigator();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nav.NameTable);
            nsmgr.AddNamespace("lm", LinkMe.Apps.Services.Constants.XmlSerializationNamespace);
            XPathExpression expr = nav.Compile("//lm:JobAd");
            expr.SetContext(nsmgr);
            XPathNodeIterator iter = nav.Select(expr);
            AssertEquals(iter.Count, 0);
        }

        [Test]
        public void TestModifiedSince()
        {
            string[] parameters = new string[4];
            parameters[0] = GetJobAds.IndustriesParameter;
            parameters[1] = ((Industry)_jobAd.JobDescription.Industries[0]).Name;
            parameters[2] = GetJobAds.ModifiedSinceParameter;
            parameters[3] = "20070101T000000";

            //Wrong modifiedSince parameter

            string response = CallService(parameters);
            string[] errors = GetErrorsFromResponse(response);
            CheckErrors(errors, GetJobAds.ERROR_MODIFIEDSINCE);

            //Correct modifiedSince, one job ad found
            parameters[3] = "2007-01-01T00:00:00Z";

            response = CallService(parameters);

            var viewJobAdUrl = NavigationManager.GetUrlForPage<Job>(Job.JobAdIdParam, _jobAd.Id.ToString());
            var applyJobAdUrl = NavigationManager.GetUrlForPage<JobApplicationSignInForm>(Job.JobAdIdParam, _jobAd.Id.ToString());
            var localJobAd = _jobAd.Map(_employer, viewJobAdUrl.AbsoluteUri, applyJobAdUrl.AbsoluteUri);
            var netJobAd = GetJobAdFeedFromResponse(response);
            CompareJobAdFeed(localJobAd, netJobAd);

            //Date in future, nothing found
            parameters[3] = String.Format("{0}-01-01T00:00:00Z", DateTime.Now.AddYears(1).Year);

            response = CallService(parameters);

            XmlReader reader = XmlReader.Create(new StringReader(response));
            reader.ReadStartElement("GetJobAdsResponse");
            reader.ReadEndElement();
        }

        //This test can be used used to measure performance of the 
        //webservice with IsReusable = true/false
        [Test, Ignore("Slow test for manual performance measurments")]
        public void TestPerformance()
        {
            //Creating heaps of job ads

            for (int i = 0; i < 1000; i++)
            {
                _jobAd = _employer.CreateTestJobAd();
                _jobAd.ExternalReferenceId = "REF_" + i;
                _jobAd.JobDescription.Summary = "Summary " + i;
                _jobAd.Status = JobAdStatus.Open;
                _jobAd.JobDescription.Industries.Add(Container.Current.Resolve<IIndustriesCommand>().GetIndustries()[0]);
                _jobAdsCommand.PostJobAd(_jobAd);
            }

            string[] parameters = new string[2];
            parameters[0] = GetJobAds.IndustriesParameter;
            parameters[1] = ((Industry)_jobAd.JobDescription.Industries[0]).Name.ToLower();

            for (int i = 0; i < 10; i++)
            {
                CallService(parameters);
            }
        }

        private string[] GetErrorsFromResponse(string response)
        {
            ThrowIfContainsServerError(response);

            XPathDocument doc = new XPathDocument(new StringReader(response));
            XPathNavigator nav = doc.CreateNavigator();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nav.NameTable);
            nsmgr.AddNamespace("lm", LinkMe.Apps.Services.Constants.XmlSerializationNamespace);
            XPathExpression expr = nav.Compile("//lm:Error");
            expr.SetContext(nsmgr);
            XPathNodeIterator iter = nav.Select(expr);
            List<string> res = new List<string>();
            while(iter.MoveNext()) 
            {
                XPathNavigator node = iter.Current;
                res.Add(node.Value);
            } 
            
            return res.ToArray();
        }

        private static void CheckErrors(string[] errors, string exStr)
        {
            Assert(errors.Length != 0);
            foreach (string s in errors)
            {
                Assert(s.Contains(exStr));
            }
        }

        private string CallService(params string[] parameters)
        {
            return CallIntegrationGetService<GetJobAds>(_integratorUser.UserName, "password", parameters);
        }
    }
}

