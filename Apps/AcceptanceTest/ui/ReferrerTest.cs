using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.ui
{
	[TestClass]
	public class ReferrerTest : WebTestClass
	{
		[TestMethod]
		public void TestPageRedirectsWithReferrer()
		{
			Get(new ApplicationUrl("~/?ref=linkme"));
			Assert.IsTrue(Browser.CurrentUrl.PathAndQuery.IndexOf("ref=linkme") == -1);
		}
	}
}