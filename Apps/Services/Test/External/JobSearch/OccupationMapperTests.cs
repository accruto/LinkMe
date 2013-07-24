using LinkMe.Apps.Services.External.JobSearch;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.JobSearch
{
    [TestClass]
    public class OccupationMapperTests
        : TestClass
    {
        private const int DefaultCode = 8999;
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly OccupationMapper _mapper;

        public OccupationMapperTests()
        {
            _mapper = new OccupationMapper(_industriesQuery);
        }

        [TestMethod]
        public void CanMapMappedIndistry()
        {
            var industry = _industriesQuery.GetIndustryByUrlName("hr-recruitment");

            var jobAd = new JobAd
                            {
                                Description = {Industries = new[] {industry}},
                            };

            var code = _mapper.Map(jobAd);
            Assert.AreEqual(1323, code);
        }

        [TestMethod]
        public void ReturnsDefaultForUnmappedIndistry()
        {
            var industry = _industriesQuery.GetIndustryByUrlName("other");

            var jobAd = new JobAd
                            {
                                Description = { Industries = new[] { industry } },
                            };

            var code = _mapper.Map(jobAd);
            Assert.AreEqual(DefaultCode, code);
        }
 
        [TestMethod]
        public void ReturnsDefaultForJobWithoutIndistry()
        {
            var jobAd = new JobAd();

            var code = _mapper.Map(jobAd);
            Assert.AreEqual(DefaultCode, code);

            jobAd.Description.Industries = new Industry[0];

            code = _mapper.Map(jobAd);
            Assert.AreEqual(DefaultCode, code);
        }
    }
}