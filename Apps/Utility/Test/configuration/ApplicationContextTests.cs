using System;
using LinkMe.Utility.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Configuration
{
	[TestClass]
	public class ApplicationContextTests
        : TestClass
	{
        [TestInitialize]
        public void ApplicationContextTestsInitialize()
		{
			ApplicationContext.Instance.Reset();
		}

		[TestMethod]
		public void TestGetProperty()
		{
			ApplicationContext instance = ApplicationContext.Instance;
            Assert.IsNotNull(instance.GetProperty("ignored.job.titles"));
		}

		[TestMethod]
		public void TestGetStringArrayProperty()
		{
			ApplicationContext instance = ApplicationContext.Instance;
			instance.SetProperty("BLAH_BLAH_BLAH", "blah1,blah2,blah3");

			string[] values = instance.GetStringArrayProperty("BLAH_BLAH_BLAH");

			Assert.IsNotNull(values);
			Assert.AreEqual(3,values.Length);
			Assert.AreEqual("blah1", values[0]);
			Assert.AreEqual("blah2", values[1]);
			Assert.AreEqual("blah3", values[2]);

		}

		[TestMethod, ExpectedException(typeof(ApplicationException), "The value of the 'test.bool.property' setting, 'invalidString', is not a valid boolean.")]
		public void TestGetBoolProperty()
		{
			const string testBoolProperty = "test.bool.property";
			ApplicationContext.Instance.SetProperty(testBoolProperty, "true");
			Assert.IsTrue(ApplicationContext.Instance.GetBoolProperty(testBoolProperty));
			Assert.IsTrue(ApplicationContext.Instance.GetBoolProperty(testBoolProperty));
			ApplicationContext.Instance.SetProperty(testBoolProperty, "false");
			Assert.IsFalse(ApplicationContext.Instance.GetBoolProperty(testBoolProperty));
			Assert.IsFalse(ApplicationContext.Instance.GetBoolProperty(testBoolProperty));
			ApplicationContext.Instance.SetProperty(testBoolProperty, "TRUE");
			Assert.IsTrue(ApplicationContext.Instance.GetBoolProperty(testBoolProperty));
			ApplicationContext.Instance.SetProperty(testBoolProperty, "FAlse");
			Assert.IsFalse(ApplicationContext.Instance.GetBoolProperty(testBoolProperty));
			ApplicationContext.Instance.SetProperty(testBoolProperty, "invalidString");
			Assert.IsFalse(ApplicationContext.Instance.GetBoolProperty(testBoolProperty));
		}

		[TestMethod, ExpectedException(typeof(ApplicationException), "The value of the 'test.int.property' setting, 'abc', is not a valid integer.")]
		public void TestGetIntProperty()
		{
			const string testIntProperty = "test.int.property";
			ApplicationContext.Instance.SetProperty(testIntProperty, "1");
			Assert.IsTrue(1 == ApplicationContext.Instance.GetIntProperty(testIntProperty));
			Assert.IsFalse(2 == ApplicationContext.Instance.GetIntProperty(testIntProperty));
			ApplicationContext.Instance.SetProperty(testIntProperty, "abc");
			ApplicationContext.Instance.GetIntProperty(testIntProperty);
		}

		[TestMethod, ExpectedException(typeof(ApplicationException), "There is no value for the 'wrongName' setting.")]
		public void TestGetInvalidProperty()
		{
			ApplicationContext instance = ApplicationContext.Instance;
			Assert.IsNull(instance.GetProperty("wrongName"));
		}

		[TestMethod]
		public void TestGetInvalidOptionalProperty()
		{
			ApplicationContext instance = ApplicationContext.Instance;
			Assert.IsNull(instance.GetProperty("wrongName", true));
		}

		[TestMethod]
		public void TestSetProperty()
		{
			ApplicationContext instance = ApplicationContext.Instance;
			instance.SetProperty("test1", "newTestValue1");
			Assert.AreEqual("newTestValue1", instance.GetProperty("test1"));
		}
	}
}