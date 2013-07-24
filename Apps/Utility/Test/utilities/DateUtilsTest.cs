using System;
using LinkMe.Utility.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
	[TestClass]
	public class DateUtilsTest
        : TestClass
	{
		[TestMethod]
		public void TestParseMonthAndYear()
		{
            // No input

            Assert.AreEqual(null, DateUtils.ParseMonthAndYear(null));
            Assert.AreEqual(null, DateUtils.ParseMonthAndYear(""));

			// The special cases for which ParseMonthAndYear() is needed.

			Assert.AreEqual(new DateTime(2004, 2, 1), DateUtils.ParseMonthAndYear("Feb 04"));
			Assert.AreEqual(new DateTime(2003, 8, 1), DateUtils.ParseMonthAndYear(" August '03"));
			Assert.AreEqual(new DateTime(2017, 1, 1), DateUtils.ParseMonthAndYear("Jan-17 "));
			Assert.AreEqual(new DateTime(2006, 1, 1), DateUtils.ParseMonthAndYear(" 06"));
			Assert.AreEqual(new DateTime(2006, 1, 1), DateUtils.ParseMonthAndYear("2006 "));
			Assert.AreEqual(new DateTime(1970, 1, 1), DateUtils.ParseMonthAndYear(" 70 "));
			Assert.AreEqual(new DateTime(1970, 1, 1), DateUtils.ParseMonthAndYear("  1970   "));

			// Regular cases, handled by DateTime.Parse().

			Assert.AreEqual(new DateTime(1987, 2, 1), DateUtils.ParseMonthAndYear("Feb 87"));
			Assert.AreEqual(new DateTime(1987, 2, 1), DateUtils.ParseMonthAndYear("Feb 1987"));
			Assert.AreEqual(new DateTime(2004, 2, 5), DateUtils.ParseMonthAndYear("5 Feb 04"));
			Assert.AreEqual(new DateTime(2004, 2, 5), DateUtils.ParseMonthAndYear("5 Feb 2004"));
            Assert.AreEqual(new DateTime(123, 3, 1), DateUtils.ParseMonthAndYear("Mar 123"));
            Assert.AreEqual(new DateTime(9999, 3, 1), DateUtils.ParseMonthAndYear("Mar 9999"));

			// Invalid.

			Assert.AreEqual(new DateTime(2000, 2, 4), DateUtils.ParseMonthAndYear("Feb 0.4"));
            Assert.AreEqual(null, DateUtils.ParseMonthAndYear("Mar 0000"));
            Assert.AreEqual(null, DateUtils.ParseMonthAndYear("Mar 10000"));
            Assert.AreEqual(null, DateUtils.ParseMonthAndYear("123"));
            Assert.AreEqual(null, DateUtils.ParseMonthAndYear("0000")); // Case 3995
            Assert.AreEqual(null, DateUtils.ParseMonthAndYear("9999"));
            Assert.AreEqual(null, DateUtils.ParseMonthAndYear("10000"));
        }
	}
}