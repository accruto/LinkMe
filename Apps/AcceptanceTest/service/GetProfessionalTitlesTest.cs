using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Web.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.service
{
    [TestClass]
    public class GetProfessionalTitlesTest : WebFormDataTestCase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var connectionFactory = Resolve<IDbConnectionFactory>();
            connectionFactory.DeleteAllTestData();

            DatabaseHelper.ExecuteNonQuery(connectionFactory, "INSERT dbo.EquivalentTerms VALUES ( 'Account Assistant', 'F91481C7-F1FA-4253-BF3B-174E66282046' )");
            DatabaseHelper.ExecuteNonQuery(connectionFactory, "INSERT dbo.EquivalentTerms VALUES ( 'Account Manager', '994F0B3A-5117-4C8C-9BC2-C7976BCA4F88' )");
            DatabaseHelper.ExecuteNonQuery(connectionFactory, "INSERT dbo.EquivalentTerms VALUES ( 'Admin Officer', '222FA125-E583-4509-89D3-DBB4A360DCCB' )");
            DatabaseHelper.ExecuteNonQuery(connectionFactory, "INSERT dbo.EquivalentTerms VALUES ( 'Administration Officer', '222FA125-E583-4509-89D3-DBB4A360DCCB' )");
        }

        [TestMethod]
        public void TestGetProfessionalTitlesNoResults()
        {
            var parameters = new string[2];
            parameters[0] = GetProfessionalTitles.ProfessionalTitleParameter;
            parameters[1] = "whatever";

            string response = CallGetService<GetProfessionalTitles>(parameters);
            IList<string> titles = GetTitlesFromResponse(response);
            Assert.AreEqual(0, titles.Count);
        }

        [TestMethod]
        public void TestExact()
        {
            var parameters = new string[2];
            parameters[0] = GetProfessionalTitles.ProfessionalTitleParameter;
            parameters[1] = "Account Assistant";

            string response = CallGetService<GetProfessionalTitles>(parameters);
            IList<string> titles = GetTitlesFromResponse(response);
            AssertTitles(titles, "Account Assistant");

            parameters[1] = "Account Manager";
            response = CallGetService<GetProfessionalTitles>(parameters);
            titles = GetTitlesFromResponse(response);
            AssertTitles(titles, "Account Manager");

            parameters[1] = "Admin Officer";
            response = CallGetService<GetProfessionalTitles>(parameters);
            titles = GetTitlesFromResponse(response);
            AssertTitles(titles, "Admin Officer");

            parameters[1] = "Administration Officer";
            response = CallGetService<GetProfessionalTitles>(parameters);
            titles = GetTitlesFromResponse(response);
            AssertTitles(titles, "Administration Officer");
        }

        [TestMethod]
        public void TestStart()
        {
            var parameters = new string[2];
            parameters[0] = GetProfessionalTitles.ProfessionalTitleParameter;
            parameters[1] = "Account";

            string response = CallGetService<GetProfessionalTitles>(parameters);
            IList<string> titles = GetTitlesFromResponse(response);

            // The order returned is reversed alphabetical.
            AssertTitles(titles, "Account Manager", "Account Assistant");

            parameters[1] = "Admin";
            response = CallGetService<GetProfessionalTitles>(parameters);
            titles = GetTitlesFromResponse(response);
            AssertTitles(titles, "Administration Officer", "Admin Officer");
        }

        [TestMethod]
        public void TestMiddle()
        {
            var parameters = new string[2];
            parameters[0] = GetProfessionalTitles.ProfessionalTitleParameter;
            parameters[1] = "Offic";

            string response = CallGetService<GetProfessionalTitles>(parameters);
            IList<string> titles = GetTitlesFromResponse(response);
            AssertTitles(titles, "Administration Officer", "Admin Officer");
        }

        [TestMethod]
        public void TestSqlInjection()
        {
            _administratorAccountsCommand.CreateTestAdministrator(TestAdministratorUserId);

            var parameters = new string[2];
            parameters[0] = GetProfessionalTitles.ProfessionalTitleParameter;
            parameters[1] = "whatever' UNION SELECT loginId + ':' + passwordHash FROM RegisteredUser ru INNER JOIN Administrator a ON ru.id = a.id--";

            string response = CallGetService<GetProfessionalTitles>(parameters);
            IList<string> titles = GetTitlesFromResponse(response);
            Assert.AreEqual(0, titles.Count);
        }

        private static IList<string> GetTitlesFromResponse(string responseString)
        {
            var titles = new List<string>();

            if (!responseString.StartsWith("<ul>") || !responseString.EndsWith("</ul>"))
                throw new ApplicationException ("The response does not contain the start and end tags");

            responseString = responseString.Substring("<ul>".Length);
            responseString = responseString.Substring(0, responseString.Length - "</ul>".Length);
            while (responseString.StartsWith("<li>"))
            {
                responseString = responseString.Substring("<li>".Length);
                int pos = responseString.IndexOf("</li>");
                if (pos == -1)
                    throw new ApplicationException("Could not find a </li> end tag.");

                string title = responseString.Substring(0, pos);
                responseString = responseString.Substring(pos);
                titles.Add(title);

                responseString = responseString.Substring("</li>".Length);
            }

            if (responseString.Length != 0)
                throw new ApplicationException("The response does not have the correct format.");

            return titles;
        }

        private static void AssertTitles(IList<string> titles, params string[] expected)
        {
            Assert.AreEqual(expected.Length, titles.Count);
            for (int index = 0; index < expected.Length; ++index)
                Assert.AreEqual(expected[index], titles[index]);
        }
    }
}

