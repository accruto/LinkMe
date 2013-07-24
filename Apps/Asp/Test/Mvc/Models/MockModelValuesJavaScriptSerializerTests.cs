using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    [TestClass]
    public class MockModelValuesJavaScriptSerializerTests
        : JavaScriptSerializerTests<MockModelValues>
    {
        [TestMethod]
        public void Test()
        {
            Test(MockModelValues.CreateMockModelValues(), () => new MockModelValuesJavaScriptConverter(), () => new MockModelValuesConverter());
        }

        [TestMethod]
        public void TestStringArrayValue()
        {
            TestStringArrayValue(null, "{}");
            TestStringArrayValue(null, "{\"NotNullStringArrayValue\":null}");
            TestStringArrayValue(null, "{\"NotNullStringArrayValue\":[]}");
            TestStringArrayValue(new[] { "value1" }, "{\"NotNullStringArrayValue\":\"value1\"}");
            TestStringArrayValue(new[] { "value1" }, "{\"NotNullStringArrayValue\":[\"value1\"]}");
            TestStringArrayValue(new[] { "value1", "value2" }, "{\"NotNullStringArrayValue\":[\"value1\",\"value2\"]}");
        }

        [TestMethod]
        public void TestGuidArrayValue()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            TestGuidArrayValue(null, "{}");
            TestGuidArrayValue(null, "{\"NotNullGuidArrayValue\":null}");
            TestGuidArrayValue(null, "{\"NotNullGuidArrayValue\":[]}");
            TestGuidArrayValue(new[] { id1 }, "{\"NotNullGuidArrayValue\":\"" + id1 + "\"}");
            TestGuidArrayValue(new[] { id1 }, "{\"NotNullGuidArrayValue\":[\"" + id1 + "\"]}");
            TestGuidArrayValue(new[] { id1, id2 }, "{\"NotNullGuidArrayValue\":[\"" + id1 + "\",\"" + id2 + "\"]}");
        }

        [TestMethod]
        public void TestBooleanValue()
        {
            TestBooleanValue(null, "{}");
            TestBooleanValue(null, "{\"NotNullBoolValue\":null}");
            TestBooleanValue(null, "{\"NotNullBoolValue\":[]}");
            TestBooleanValue(true, "{\"NotNullBoolValue\":true}");
            TestBooleanValue(false, "{\"NotNullBoolValue\":false}");
            TestBooleanValue(true, "{\"NotNullBoolValue\":[true,false]}");
            TestBooleanValue(false, "{\"NotNullBoolValue\":[false,false]}");
        }

        private static void TestStringArrayValue(IEnumerable<string> expectedValue, string serialization)
        {
            var model = (MockModelValues)new ModelBinder(new MockModelValuesConverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new JavaScriptValueProvider(serialization) });
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullStringArrayValue));

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new MockModelValuesJavaScriptConverter() });
            model = serializer.Deserialize<MockModelValues>(serialization);
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullStringArrayValue));
        }

        private static void TestGuidArrayValue(IEnumerable<Guid> expectedValue, string serialization)
        {
            var model = (MockModelValues)new ModelBinder(new MockModelValuesConverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new JavaScriptValueProvider(serialization) });
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullGuidArrayValue));

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new MockModelValuesJavaScriptConverter() });
            model = serializer.Deserialize<MockModelValues>(serialization);
            Assert.IsTrue(expectedValue.NullableCollectionEqual(model.NotNullGuidArrayValue));
        }

        private static void TestBooleanValue(bool? expectedValue, string serialization)
        {
            var model = (MockModelValues)new ModelBinder(new MockModelValuesConverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new JavaScriptValueProvider(serialization) });
            Assert.AreEqual(expectedValue, model.NotNullBoolValue);

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new MockModelValuesJavaScriptConverter() });
            model = serializer.Deserialize<MockModelValues>(serialization);
            Assert.AreEqual(expectedValue, model.NotNullBoolValue);
        }

        protected override void AssertAreEqual(MockModelValues model, MockModelValues deserializedModel)
        {
            Assert.AreEqual(model, deserializedModel);
        }

        protected override string GetExpectedSerialization(MockModelValues model)
        {
            var output = "{"
                + "\"NullStringValue\":null,"
                + "\"NotNullStringValue\":\"" + model.NotNullStringValue + "\","
                + "\"NullStringArrayValue\":null,"
                + "\"NotNullStringArrayValue\":[\"" + string.Join("\",\"", model.NotNullStringArrayValue) + "\"],"
                + "\"NullBoolValue\":null,"
                + "\"NotNullBoolValue\":" + model.NotNullBoolValue.Value.ToString().ToLower() + ","
                + "\"BoolValue\":" + model.BoolValue.ToString().ToLower() + ","
                + "\"NullIntValue\":null,"
                + "\"NotNullIntValue\":" + model.NotNullIntValue + ","
                + "\"IntValue\":" + model.IntValue + ","
                + "\"NullDecimalValue\":null,"
                + "\"NotNullDecimalValue\":" + model.NotNullDecimalValue + ","
                + "\"DecimalValue\":" + model.DecimalValue + ","
                + "\"NullGuidValue\":null,"
                + "\"NotNullGuidValue\":\"" + model.NotNullGuidValue + "\","
                + "\"GuidValue\":\"" + model.GuidValue + "\","
                + "\"NullGuidArrayValue\":null,"
                + "\"NotNullGuidArrayValue\":[\"" + string.Join("\",\"", (from g in model.NotNullGuidArrayValue select g.ToString()).ToArray()) + "\"],"
                + "\"NullDateTimeValue\":null,"
                + "\"NotNullDateTimeValue\":\"" + model.NotNullDateTimeValue.Value.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ss.fffffffZ") + "\","
                + "\"NullPartialDateValue\":null,"
                + "\"NotNullPartialDateValue\":\"" + string.Format("{0:D4}-{1:D2}", model.NotNullPartialDateValue.Value.Year, model.NotNullPartialDateValue.Value.Month) + "\","
                + "\"EnumValue\":\"" + model.EnumValue + "\","
                + "\"NullEnumValue\":null,"
                + "\"NotNullEnumValue\":\"" + model.NotNullEnumValue.Value + "\"";

            if (model.EnumFlagsValue.IsFlagSet(EnumFlagsValue1.Banana))
                output += ",\"Banana\":true";
            if (model.EnumFlagsValue.IsFlagSet(EnumFlagsValue1.Orange))
                output += ",\"Orange\":true";
            if (model.NotNullEnumFlagsValue.Value.IsFlagSet(EnumFlagsValue3.Apple))
                output += ",\"Apple\":true";
            if (model.NotNullEnumFlagsValue.Value.IsFlagSet(EnumFlagsValue3.Grape))
                output += ",\"Grape\":true";
            return output + "}";
        }
    }
}