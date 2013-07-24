using System.Text;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.service
{
    [TestClass]
    public class GetStructuredLocationsTest : WebTestClass
    {
        private static ReadOnlyUrl GetServiceUrl()
        {
            return GetServiceUrl(new string[]{});
        }

        private static ReadOnlyUrl GetServiceUrl(int countryId)
        {
            return GetServiceUrl(GetStructuredLocations.CountryParameter, countryId.ToString());
        }

        private static ReadOnlyUrl GetServiceUrl(int countryId, int maximum)
        {
            return GetServiceUrl(
                GetStructuredLocations.CountryParameter, countryId.ToString(),
                GetStructuredLocations.MaximumParameter, maximum.ToString()
                );
        }

        private static ReadOnlyUrl GetServiceUrl(int countryId, int maximum, string location)
        {
            return GetServiceUrl(
                GetStructuredLocations.CountryParameter, countryId.ToString(),
                GetStructuredLocations.MaximumParameter, maximum.ToString(),
                GetStructuredLocations.LocationParameter, location);
        }

        private static ReadOnlyUrl GetServiceUrl(params string[] parameters)
        {
            return NavigationManager.GetUrlForPage<GetStructuredLocations>(parameters);
        }

        [TestMethod]
        public void TestAustraliaCountry()
        {
            // No parameters should return all subdivisions and regions.

            var url = GetServiceUrl();
            Get(url);

            var sb = new StringBuilder();
            foreach (CountrySubdivision subdivision in _locationQuery.GetCountrySubdivisions(Australia))
                AppendCountrySubdivisionRecord(sb, subdivision);
            foreach (Region region in _locationQuery.GetRegions(Australia))
                AppendRegionRecord(sb, region);

            Assert.AreEqual(sb.ToString(), Browser.CurrentPageText);

            // Specifying australia should return the same.

            url = GetServiceUrl(Australia.Id);
            Get(url);
            Assert.AreEqual(sb.ToString(), Browser.CurrentPageText);
        }

        [TestMethod]
        public void TestAustraliaCountryWithLimit()
        {
            // If it is just a country specified then all subdivisions and all regions should be displayed.

            var sb = new StringBuilder();
            foreach (CountrySubdivision subdivision in _locationQuery.GetCountrySubdivisions(Australia))
                AppendCountrySubdivisionRecord(sb, subdivision);
            foreach (Region region in _locationQuery.GetRegions(Australia))
                AppendRegionRecord(sb, region);

            var url = GetServiceUrl(Australia.Id, 1);
            Get(url);
            Assert.AreEqual(sb.ToString(), Browser.CurrentPageText);

            // Get 5.

            url = GetServiceUrl(Australia.Id, 5);
            Get(url);
            Assert.AreEqual(sb.ToString(), Browser.CurrentPageText);

            // Get 12.

            url = GetServiceUrl(Australia.Id, 12);
            Get(url);
            Assert.AreEqual(sb.ToString(), Browser.CurrentPageText);
        }

        [TestMethod]
        public void TestAutoSuggestion()
        {
            // Get suggestions for 'Me'.

            var url = GetServiceUrl(Australia.Id, 10, "Me");
            Get(url);

            // These should be returned.

            var sb = new StringBuilder();
            AppendRegionRecord(sb, _locationQuery.GetRegion(Australia, "Melbourne"));
            AppendRegionRecord(sb, _locationQuery.GetRegion(Australia, "Melbourne (Bayside & South Eastern Suburbs)"));
            AppendRegionRecord(sb, _locationQuery.GetRegion(Australia, "Melbourne (CBD & Inner Suburbs)"));
            AppendRegionRecord(sb, _locationQuery.GetRegion(Australia, "Melbourne (Eastern Suburbs)"));
            AppendRegionRecord(sb, _locationQuery.GetRegion(Australia, "Melbourne (Northern Suburbs)"));
            AppendRegionRecord(sb, _locationQuery.GetRegion(Australia, "Melbourne (Western Suburbs)"));

            var suburbs = new[]
            {
                "Mead NSW", "Meadow QLD", "Meadow Creek NSW", "Meadow Flat NSW"
            };

            foreach (var suburb in suburbs)
                AppendSuburbRecord(sb, suburb);
            Assert.AreEqual(sb.ToString(), Browser.CurrentPageText);

            // Only one suggestion should be returned.

            url = GetServiceUrl(Australia.Id, 1, "Me");
            Get(url);
            
            // This should be returned.

            sb = new StringBuilder();
            AppendRegionRecord(sb, _locationQuery.GetRegion(Australia, "Melbourne"));
            Assert.AreEqual(sb.ToString(), Browser.CurrentPageText);
        }

        private static void AppendRegionRecord(StringBuilder sb, Region region)
        {
            if (sb.Length != 0)
                sb.Append(';');
            sb.Append(region.Id).Append(":r=").Append(region.Name);
        }

        private static void AppendCountrySubdivisionRecord(StringBuilder sb, CountrySubdivision countrySubdivision)
        {
            if (sb.Length != 0)
                sb.Append(';');

            sb.Append(countrySubdivision.Id).Append(':');

            string displayName = countrySubdivision.Name;
            if (countrySubdivision.IsCountry)
            {
                sb.Append("c");
                displayName = "Anywhere in " + countrySubdivision.Country.Name;
            }
            else
            {
                sb.Append("s");
            }

            sb.Append('=').Append(displayName);
        }

        private void AppendSuburbRecord(StringBuilder sb, string suburb)
        {
            if (sb.Length != 0)
                sb.Append(';');

            // Resolve the suburb.

            var locationReference = new LocationReference();
            _locationQuery.ResolveLocation(locationReference, Australia, suburb);
            PostalSuburb postalSuburb = locationReference.PostalSuburb;

            // Append.

            sb.Append(postalSuburb.Id).Append(":p=").Append(postalSuburb.ToString());
        }
    }
}
