using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    [TestClass]
    public class MockModelValuesQueryStringTests
        : QueryStringTests<MockModelValues>
    {
        [TestMethod]
        public void Test()
        {
            var model = MockModelValues.CreateMockModelValues();
            Test(model, GetExpectedSerialization(model), () => new MockModelValuesConverter(), () => new MockModelValuesConverter());
        }

        [TestMethod]
        public void TestStringArrayValue()
        {
            TestStringArrayValue(null, new QueryString());
            TestStringArrayValue(new[] { "value1" }, new QueryString { { "NotNullStringArrayValue", "value1" } });
            TestStringArrayValue(new[] { "value1", "value2" }, new QueryString { { "NotNullStringArrayValue", "value1" }, { "NotNullStringArrayValue", "value2" } });
            TestStringArrayValue(new[] { "value1" }, new QueryString { { "NotNullStringArrayValue[]", "value1" } });
            TestStringArrayValue(new[] { "value1", "value2" }, new QueryString { { "NotNullStringArrayValue[]", "value1" }, { "NotNullStringArrayValue[]", "value2" } });
        }

        [TestMethod]
        public void TestGuidArrayValue()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            TestGuidArrayValue(null, new QueryString());
            TestGuidArrayValue(new[] { id1 }, new QueryString { { "NotNullGuidArrayValue", id1.ToString() } });
            TestGuidArrayValue(new[] { id1, id2 }, new QueryString { { "NotNullGuidArrayValue", id1.ToString() }, { "NotNullGuidArrayValue", id2.ToString() } });
            TestGuidArrayValue(new[] { id1 }, new QueryString { { "NotNullGuidArrayValue[]", id1.ToString() } });
            TestGuidArrayValue(new[] { id1, id2 }, new QueryString { { "NotNullGuidArrayValue[]", id1.ToString() }, { "NotNullGuidArrayValue[]", id2.ToString() } });
        }

        [TestMethod]
        public void TestBooleanValue()
        {
            TestBooleanValue(null, new QueryString());
            TestBooleanValue(true, new QueryString { { "NotNullBoolValue", true.ToString().ToLower() } });
            TestBooleanValue(false, new QueryString { { "NotNullBoolValue", false.ToString().ToLower() } });
            TestBooleanValue(true, new QueryString { { "NotNullBoolValue", 1.ToString() } });
            TestBooleanValue(false, new QueryString { { "NotNullBoolValue", 0.ToString() } });
            TestBooleanValue(true, new QueryString { { "NotNullBoolValue", true.ToString().ToLower() }, { "NotNullBoolValue", false.ToString().ToLower() } });
            TestBooleanValue(false, new QueryString { { "NotNullBoolValue", false.ToString().ToLower() }, { "NotNullBoolValue", false.ToString().ToLower() } });
            TestBooleanValue(true, new QueryString { { "NotNullBoolValue[]", true.ToString().ToLower() }, { "NotNullBoolValue[]", false.ToString().ToLower() } });
            TestBooleanValue(false, new QueryString { { "NotNullBoolValue[]", false.ToString().ToLower() } });
        }

        private static void TestStringArrayValue(IEnumerable<string> expectedValue, ReadOnlyQueryString queryString)
        {
            var model = (MockModelValues)new ModelBinder(new MockModelValuesConverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new ReadOnlyQueryStringValueProvider(queryString) });
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullStringArrayValue));
            model = (MockModelValues)new QueryStringBinder(new MockModelValuesConverter()).BindQueryString(queryString);
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullStringArrayValue));
        }

        private static void TestGuidArrayValue(IEnumerable<Guid> expectedValue, ReadOnlyQueryString queryString)
        {
            var model = (MockModelValues)new ModelBinder(new MockModelValuesConverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new ReadOnlyQueryStringValueProvider(queryString) });
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullGuidArrayValue));
            model = (MockModelValues)new QueryStringBinder(new MockModelValuesConverter()).BindQueryString(queryString);
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullGuidArrayValue));
        }

        private static void TestBooleanValue(bool? expectedValue, ReadOnlyQueryString queryString)
        {
            var model = (MockModelValues)new ModelBinder(new MockModelValuesConverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new ReadOnlyQueryStringValueProvider(queryString) });
            Assert.AreEqual(expectedValue, model.NotNullBoolValue);
            model = (MockModelValues)new QueryStringBinder(new MockModelValuesConverter()).BindQueryString(queryString);
            Assert.AreEqual(expectedValue, model.NotNullBoolValue);
        }

        protected override void AssertAreEqual(MockModelValues model, MockModelValues deserializedModel)
        {
            Assert.AreEqual(model, deserializedModel);
        }

        private static string GetExpectedSerialization(MockModelValues model)
        {
            var sb = new StringBuilder();
            sb.Append("NotNullStringValue=").Append(model.NotNullStringValue);

            foreach (var value in model.NotNullStringArrayValue)
                sb.Append("&NotNullStringArrayValue=").Append(value);

            sb.Append("&NotNullBoolValue=").Append(model.NotNullBoolValue.Value.ToString().ToLower())
                .Append("&BoolValue=").Append(model.BoolValue.ToString().ToLower())
                .Append("&NotNullIntValue=").Append(model.NotNullIntValue)
                .Append("&IntValue=").Append(model.IntValue)
                .Append("&NotNullDecimalValue=").Append(model.NotNullDecimalValue)
                .Append("&DecimalValue=").Append(model.DecimalValue)
                .Append("&NotNullGuidValue=").Append(model.NotNullGuidValue)
                .Append("&GuidValue=").Append(model.GuidValue);

            foreach (var value in model.NotNullGuidArrayValue)
                sb.Append("&NotNullGuidArrayValue=").Append(value);

            sb.Append("&NotNullDateTimeValue=").Append(HttpUtility.UrlEncode(model.NotNullDateTimeValue.Value.ToString("yyyy-MM-ddThh:mm:ss.fffffffZ")))
                .Append("&NotNullPartialDateValue=").Append(string.Format("{0:D4}-{1:D2}", model.NotNullPartialDateValue.Value.Year, model.NotNullPartialDateValue.Value.Month))
                .Append("&EnumValue=").Append(model.EnumValue)
                .Append("&NotNullEnumValue=").Append(model.NotNullEnumValue.Value);

            if (model.EnumFlagsValue.IsFlagSet(EnumFlagsValue1.Banana))
                sb.Append("&Banana=true");
            if (model.EnumFlagsValue.IsFlagSet(EnumFlagsValue1.Orange))
                sb.Append("&Orange=true");
            if (model.NotNullEnumFlagsValue.Value.IsFlagSet(EnumFlagsValue3.Apple))
                sb.Append("&Apple=true");
            if (model.NotNullEnumFlagsValue.Value.IsFlagSet(EnumFlagsValue3.Grape))
                sb.Append("&Grape=true");

            return sb.ToString();
        }
    }
}