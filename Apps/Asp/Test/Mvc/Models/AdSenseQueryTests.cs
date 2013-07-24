using System;
using LinkMe.Apps.Presentation.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    public abstract class AdSenseQueryTests<TModel>
        : TestClass
        where TModel : class
    {
        protected void Test(TModel model, string expectedAdSenseQuery, Func<IConverter> createConverter)
        {
            // Convert it.

            var adSenseQuery = new AdSenseQueryGenerator(createConverter()).GenerateAdSenseQuery(model);
            Assert.AreEqual(expectedAdSenseQuery, adSenseQuery);
        }
    }
}