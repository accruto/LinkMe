using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Environment;

namespace LinkMe.Apps.Agents.Test.Applications
{
    public class TestApplicationContext
    {
        public static void SetupApplications(WebSite currentWebSite)
        {
            ApplicationSetUp.SetSourceRootPath(RuntimeEnvironment.GetSourceFolder());

            var details = new Dictionary<WebSite, WebSiteDetails>();
            foreach (WebSite webSite in Enum.GetValues(typeof(WebSite)))
                details.Add(webSite, new WebSiteDetails("localhost", -1, null));

            ApplicationSetUp.SetWebSites(details);
            ApplicationSetUp.SetCurrentApplication(currentWebSite);
        }
    }
}
