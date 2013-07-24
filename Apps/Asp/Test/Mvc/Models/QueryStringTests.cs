using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Presentation.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    public abstract class QueryStringTests<TModel>
        : TestClass
        where TModel : class
    {
        protected void Test(TModel model, string expectedQueryString, Func<IConverter> createConverter, Func<IDeconverter> createDeconverter)
        {
            // Convert it.

            var queryString = new QueryStringGenerator(createConverter()).GenerateQueryString(model);
            Assert.AreEqual(expectedQueryString, queryString.ToString());

            // Deconvert it.

            AssertAreEqual(model, new QueryStringBinder(createDeconverter()).BindQueryString(queryString) as TModel);
            AssertAreEqual(model, new ModelBinder(createDeconverter(), null).BindModel(null, new ModelBindingContext { FallbackToEmptyPrefix = true, ValueProvider = new ReadOnlyQueryStringValueProvider(queryString) }) as TModel);
        }

        protected abstract void AssertAreEqual(TModel model, TModel deconvertedModel);
    }
}