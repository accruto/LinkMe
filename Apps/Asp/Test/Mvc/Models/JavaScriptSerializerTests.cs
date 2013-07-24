using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Presentation.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    public abstract class JavaScriptSerializerTests<TModel>
        : TestClass
        where TModel : class
    {
        protected void Test(TModel criteria, Func<JavaScriptConverter> createConverter, Func<IDeconverter> createDeconverter)
        {
            // Serialize.

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { createConverter() });
            var serialization = serializer.Serialize(criteria);
            Assert.AreEqual(GetExpectedSerialization(criteria), serialization);

            // Deserialize.

            AssertAreEqual(criteria, serializer.Deserialize<TModel>(serialization));
            AssertAreEqual(criteria, new ModelBinder(createDeconverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new JavaScriptValueProvider(serialization) }) as TModel);
        }

        protected abstract void AssertAreEqual(TModel model, TModel deserializedModel);
        protected abstract string GetExpectedSerialization(TModel model);
    }
}