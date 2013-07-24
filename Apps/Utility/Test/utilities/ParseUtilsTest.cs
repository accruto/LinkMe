using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
	[TestClass]
	public class ParseUtilsTest
        : TestClass
	{
		[TestMethod]
		public void TestCurrentDateFormat()
		{
			DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
			Assert.AreEqual("d/MM/yyyy", dateTimeFormat.ShortDatePattern,
				"The short date format on the machine is not set to the expected format, 'd/MM/yyyy' format,"
				+ " which is assumed throughout the application. E.g. DateTime.Parse() fails with MM/dd/yyyy.");
		}
	}
}
