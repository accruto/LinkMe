using System;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Seo
{
    [TestClass]
    public class CareerOneJobTests
        : JobTests
    {
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();

        protected override Guid? GetIntegratorUserId()
        {
            return _careerOneQuery.GetIntegratorUser().Id;
        }
    }
}
