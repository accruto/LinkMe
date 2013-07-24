using WatiN.Core;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.AcceptanceTest.FrontEnd.Extension
{
    public static class BrowserIeExtension
    {
        public static void Setup(this IE browser)
        {
            Settings.WaitForCompleteTimeOut = 300;
            browser.AutoClose = true;
            //browser.Visible = false;
        }

        public static void GoToHomePage(this IE browser)
        {
            var homepageUrl = new ReadOnlyApplicationUrl(false, "~/");
            browser.GoTo(homepageUrl.AbsolutePath);
        }

        public static void GoToEmployerHomePage(this IE browser)
        {
            var employerHomeUrl = new ReadOnlyApplicationUrl(true, "~/employers");
            browser.GoTo(employerHomeUrl.AbsolutePath);
            if (browser.Links.Exists("overridelink")) browser.Link("overridelink").Click();
            browser.WaitForComplete();
        }

        public static void LogIn(this IE browser)
        {
            browser.TextField(Find.ById("LoginId")).TypeText("employer0");
            browser.TextField(Find.ById("Password")).TypeText("password");
            browser.Button(Find.ById("login")).Click();
            browser.WaitForComplete();
        }

        public static void PerformSearch(this IE browser, string keyword)
        {
            browser.TextField(Find.ById("Keywords")).TypeText(keyword);
            browser.Button(Find.ById("search")).Click();
            browser.WaitForComplete();
        }
    }
}
