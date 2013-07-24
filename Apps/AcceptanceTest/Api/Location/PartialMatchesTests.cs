using System.Linq;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Location
{
    [TestClass]
    public class PartialMatchesTests
        : WebTestClass
    {
        private ReadOnlyUrl _partialMatchesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _partialMatchesUrl = new ReadOnlyApplicationUrl("~/api/location/partialmatches");
        }

        [TestMethod]
        public void TestPartialMatches()
        {
            GetPartialMatches(1, "Armadale VIC 3143", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale VIC 314", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale VIC 31", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale VIC 3", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale VIC ", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale VIC", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale VI", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale V", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "Armadale ", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "Armadale", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "Armadal", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "Armada", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "Armad", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "Arma", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340", "Armatree NSW 2831");
            GetPartialMatches(1, "Arm", 10, "Arm Cove North NSW 2324", "Arm East VIC 3235", "Arm North NSW 2323", "Arm North QLD 4871", "Arm South NSW 2460", "Arm South TAS 7022", "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340");
            GetPartialMatches(1, "Ar", 10, "Arable QLD 4728", "Arakoon NSW 2850", "Araluen NSW 2621", "Araluen NSW 2622", "Araluen QLD 4570", "Aramac QLD 4726", "Aramara VIC 3375","Aramara North QLD 4620", "Arana Hills QLD 4054", "Aranbanga QLD 4625");

            // Should get ACT and Australian Capital Territory now at the start of the list, then the Region of Adelaide.

            GetPartialMatches(1, "A", 10, "ACT", "Australian Capital Territory", "Adelaide", "Aarons Pass NSW 2346", "Abba River WA 6280", "Abbey WA 6280", "Abbeyard QLD 4350", "Abbeywood QLD 4613", "Abbotsbury NSW 2176", "Abbotsford NSW 2046");
            GetPartialMatches(1, "", 10);
        }

        [TestMethod]
        public void TestPartialMatchesCase()
        {
            GetPartialMatches(1, "armadale vic 3143", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale vic 314", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale vic 31", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale vic 3", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale vic ", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale vic", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale vi", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale v", 10, "Armadale VIC 3143");
            GetPartialMatches(1, "armadale ", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "armadale", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "armadal", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "armada", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "armad", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(1, "arma", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340", "Armatree NSW 2831");
            GetPartialMatches(1, "Arm", 10, "Arm Cove North NSW 2324", "Arm East VIC 3235", "Arm North NSW 2323", "Arm North QLD 4871", "Arm South NSW 2460", "Arm South TAS 7022", "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340");
            GetPartialMatches(1, "ar", 10, "Arable QLD 4728", "Arakoon NSW 2850", "Araluen NSW 2621", "Araluen NSW 2622", "Araluen QLD 4570", "Aramac QLD 4726", "Aramara VIC 3375", "Aramara North QLD 4620", "Arana Hills QLD 4054", "Aranbanga QLD 4625");

            // Should get ACT and Australian Capital Territory now at the start of the list, then the Region of Adelaide.

            GetPartialMatches(1, "a", 10, "ACT", "Australian Capital Territory", "Adelaide", "Aarons Pass NSW 2346", "Abba River WA 6280", "Abbey WA 6280", "Abbeyard QLD 4350", "Abbeywood QLD 4613", "Abbotsbury NSW 2176", "Abbotsford NSW 2046");
            GetPartialMatches(1, "", 10);
        }

        [TestMethod]
        public void TestPartialMatchesPostcode()
        {
            // Should never get any states here.

            GetPartialMatches(1, "3143 Armadale VIC", 10, "3143 Armadale VIC");
            GetPartialMatches(1, "3143 Armadale VI", 10, "3143 Armadale VIC");
            GetPartialMatches(1, "3143 Armadale V", 10, "3143 Armadale VIC");
            GetPartialMatches(1, "3143 Armadale ", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 Armadale", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 Armadal", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 Armada", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 Armad", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 Arma", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 Arm", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 Ar", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 A", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143 ", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "3143", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            GetPartialMatches(1, "314", 10, "3140 Lilydale VIC", "3141 South Yarra VIC", "3142 Hawksburn VIC", "3142 Toorak VIC", "3143 Armadale VIC", "3143 Armadale North VIC", "3144 Kooyong VIC", "3144 Malvern VIC", "3144 Sandpalms Roadhouse VIC", "3145 Caulfield East VIC");
            GetPartialMatches(1, "31", 10, "3101 Hackham West VIC", "3101 Kew VIC", "3102 Kew East VIC", "3103 Balwyn VIC", "3103 Cowa VIC", "3103 Spicketts Creek VIC", "3103 Zumsteins VIC", "3104 Balwyn North VIC", "3104 Willow Banks VIC", "3105 Bulleen VIC");
            GetPartialMatches(1, "3", 10, "3000 Melbourne VIC", "3002 East Melbourne VIC", "3003 West Melbourne VIC", "3004 Middle Falbrook VIC", "3004 St Kilda Rd Central VIC", "3005 World Trade Centre VIC", "3006 Innes View VIC", "3006 Southbank VIC", "3008 Docklands VIC", "3010 University Of Melbourne VIC");
            GetPartialMatches(1, "", 10);
        }

        [TestMethod]
        public void TestPartialMatchesState()
        {
            // Make sure no states are in the list. (See GetPartialMatchedNamedLocationsTest).

            GetPartialMatches(1, "Victoria Hill QLD 4361", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill QLD 436", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill QLD 43", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill QLD 4", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill QLD ", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill QLD", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill QL", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill Q", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill ", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hill", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hil", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria Hi", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria H", 10, "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria ", 10, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165");

            // Should now get Victoria at the start of the list.

            GetPartialMatches(1, "Victoria", 10, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            GetPartialMatches(1, "Victori", 10, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            GetPartialMatches(1, "Victor", 10, "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");
            GetPartialMatches(1, "Victo", 10, "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");
            GetPartialMatches(1, "Vict", 10, "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");

            // Should now get VIC at the start of the list.

            GetPartialMatches(1, "Vic", 10, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            GetPartialMatches(1, "Vi", 10, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            GetPartialMatches(1, "V", 10, "VIC", "Victoria", "V Gate SA 5276", "Vacy NSW 2421", "Valdora QLD 4650", "Vale Of Clwydd VIC 3858", "Vale Park SA 5081", "Vale View QLD 4358", "Valencia Creek VIC 3860", "Valentine NSW 2280");

            GetPartialMatches(1, "", 10);
        }

        [TestMethod]
        public void TestNoAddressLocations()
        {
            // Should only get QLD here.

            GetPartialMatches(1, "QLD", 10, "QLD");
            GetPartialMatches(1, "QL", 10, "QLD");
            GetPartialMatches(1, "Q", 10, "QLD", "Queensland", "Q Supercentre QLD 4218", "Quaama NSW 2550", "Quairading WA 6383", "Quairading South WA 6383", "Quakers Hill NSW 2763", "Qualco VIC 3579", "Qualeup WA 6394", "Quambatook VIC 3540");
            GetPartialMatches(1, "", 10);
        }

        [TestMethod]
        public void TestMaximum()
        {
            GetPartialMatches(1, "Victoria", 10, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            GetPartialMatches(1, "Victoria", 9, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");
            GetPartialMatches(1, "Victoria", 8, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            GetPartialMatches(1, "Victoria", 7, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981");
            GetPartialMatches(1, "Victoria", 6, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101");
            GetPartialMatches(1, "Victoria", 5, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979");
            GetPartialMatches(1, "Victoria", 4, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100");
            GetPartialMatches(1, "Victoria", 3, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Victoria", 2, "Victoria", "Victoria Estate NSW 2460");
            GetPartialMatches(1, "Victoria", 1, "Victoria");

            // Anything <= 0 should mean ignore the maximum.  In this case 16 will be returned rather than 10.

            GetPartialMatches(1, "Victoria", 0, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294");
            GetPartialMatches(1, "Victoria", -1, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294");

            // Use a location that will slice across the two state / aliases.

            GetPartialMatches(1, "Vic", 10, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            GetPartialMatches(1, "Vic", 9, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981");
            GetPartialMatches(1, "Vic", 8, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101");
            GetPartialMatches(1, "Vic", 7, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979");
            GetPartialMatches(1, "Vic", 6, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100");
            GetPartialMatches(1, "Vic", 5, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361");
            GetPartialMatches(1, "Vic", 4, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460");
            GetPartialMatches(1, "Vic", 3, "VIC", "Victoria", "Victor Harbor SA 5211");
            GetPartialMatches(1, "Vic", 2, "VIC", "Victoria");
            GetPartialMatches(1, "Vic", 1, "VIC");

            // Anything <= 0 should mean ignore the maximum.  In this case 20 will be returned rather than 10.

            GetPartialMatches(1, "Vic", 0, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294", "Victory Heights VIC 3673", "Victory Heights WA 6432");
            GetPartialMatches(1, "Vic", -1, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294", "Victory Heights VIC 3673", "Victory Heights WA 6432");
        }

        [TestMethod]
        public void TestAbbreviationsBeforeNames()
        {
            GetPartialMatches(1, "N", 4, "NSW", "NT", "New South Wales", "Northern Territory");
            GetPartialMatches(1, "N", 3, "NSW", "NT", "New South Wales");
            GetPartialMatches(1, "N", 2, "NSW", "NT");
            GetPartialMatches(1, "N", 1, "NSW");
        }

        [TestMethod]
        public void TestNoCountry()
        {
            GetPartialMatches(null, "Armadale VIC 3143", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale VIC 314", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale VIC 31", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale VIC 3", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale VIC ", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale VIC", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale VI", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale V", 10, "Armadale VIC 3143");
            GetPartialMatches(null, "Armadale ", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(null, "Armadale", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(null, "Armadal", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(null, "Armada", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(null, "Armad", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            GetPartialMatches(null, "Arma", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340", "Armatree NSW 2831");
            GetPartialMatches(null, "Arm", 10, "Arm Cove North NSW 2324", "Arm East VIC 3235", "Arm North NSW 2323", "Arm North QLD 4871", "Arm South NSW 2460", "Arm South TAS 7022", "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340");
            GetPartialMatches(null, "Ar", 10, "Arable QLD 4728", "Arakoon NSW 2850", "Araluen NSW 2621", "Araluen NSW 2622", "Araluen QLD 4570", "Aramac QLD 4726", "Aramara VIC 3375", "Aramara North QLD 4620", "Arana Hills QLD 4054", "Aranbanga QLD 4625");

            // Should get ACT and Australian Capital Territory now at the start of the list, then the Region of Adelaide.

            GetPartialMatches(null, "A", 10, "ACT", "Australian Capital Territory", "Adelaide", "Aarons Pass NSW 2346", "Abba River WA 6280", "Abbey WA 6280", "Abbeyard QLD 4350", "Abbeywood QLD 4613", "Abbotsbury NSW 2176", "Abbotsford NSW 2046");
            GetPartialMatches(null, "", 10);
        }

        [TestMethod]
        public void TestNoCountryNoLocation()
        {
            GetPartialMatches(null, null, 10);
            GetPartialMatches(null, "", 10);
        }

        private void GetPartialMatches(int? countryId, string location, int maximum, params string[] expected)
        {
            var url = _partialMatchesUrl.AsNonReadOnly();
            if (countryId != null)
                url.QueryString.Add("countryId", countryId.Value.ToString());
            url.QueryString.Add("location", location);
            url.QueryString.Add("maxResults", maximum.ToString());
            var response = Post(url);
            Assert.AreEqual("[" + string.Join(",", (from e in expected select "\"" + e + "\"").ToArray()) + "]", response);
        }
    }
}