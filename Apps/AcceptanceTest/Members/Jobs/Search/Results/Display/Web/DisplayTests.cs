using System;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public abstract class DisplayTests
        : ResultsTests
    {
        protected const string Keywords = "Analyst";

        protected HtmlNode GetResult(Guid jobAdId)
        {
            return Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + jobAdId + "']");
        }
    }
}