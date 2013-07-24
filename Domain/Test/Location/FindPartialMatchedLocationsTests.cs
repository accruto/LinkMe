using LinkMe.Domain.Data;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class FindPartialMatchedLocationsTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = new LocationQuery(new LocationRepository(Resolve<IDataContextFactory>()));
        private Country _australia;

        [TestInitialize]
        public void FindPartialMatchedLocationsTestsInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
            Assert.IsNotNull(_australia);
        }

        [TestMethod]
        public void PartialMatchesTest()
        {
            Find("Armadale VIC 3143", 10, "Armadale VIC 3143");
            Find("Armadale VIC 314", 10, "Armadale VIC 3143");
            Find("Armadale VIC 31", 10, "Armadale VIC 3143");
            Find("Armadale VIC 3", 10, "Armadale VIC 3143");
            Find("Armadale VIC ", 10, "Armadale VIC 3143");
            Find("Armadale VIC", 10, "Armadale VIC 3143");
            Find("Armadale VI", 10, "Armadale VIC 3143");
            Find("Armadale V", 10, "Armadale VIC 3143");
            Find("Armadale ", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("Armadale", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("Armadal", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("Armada", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("Armad", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("Arma", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340", "Armatree NSW 2831");
            Find("Arm", 10, "Arm Cove North NSW 2324", "Arm East VIC 3235", "Arm North NSW 2323", "Arm North QLD 4871", "Arm South NSW 2460", "Arm South TAS 7022", "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340");
            Find("Ar", 10, "Arable QLD 4728", "Arakoon NSW 2850", "Araluen NSW 2621", "Araluen NSW 2622", "Araluen QLD 4570", "Aramac QLD 4726", "Aramara VIC 3375", "Aramara North QLD 4620", "Arana Hills QLD 4054", "Aranbanga QLD 4625");

            // Should get ACT and Australian Capital Territory now at the start of the list, then the Region of Adelaide.

            Find("A", 10, "ACT", "Australian Capital Territory", "Adelaide", "Aarons Pass NSW 2346", "Abba River WA 6280", "Abbey WA 6280", "Abbeyard QLD 4350", "Abbeywood QLD 4613", "Abbotsbury NSW 2176", "Abbotsford NSW 2046");
            Find("", 10);
        }

        [TestMethod]
        public void PartialMatchesCaseTest()
        {
            Find("armadale vic 3143", 10, "Armadale VIC 3143");
            Find("armadale vic 314", 10, "Armadale VIC 3143");
            Find("armadale vic 31", 10, "Armadale VIC 3143");
            Find("armadale vic 3", 10, "Armadale VIC 3143");
            Find("armadale vic ", 10, "Armadale VIC 3143");
            Find("armadale vic", 10, "Armadale VIC 3143");
            Find("armadale vi", 10, "Armadale VIC 3143");
            Find("armadale v", 10, "Armadale VIC 3143");
            Find("armadale ", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("armadale", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("armadal", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("armada", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("armad", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143");
            Find("arma", 10, "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340", "Armatree NSW 2831");
            Find("arm", 10, "Arm Cove North NSW 2324", "Arm East VIC 3235", "Arm North NSW 2323", "Arm North QLD 4871", "Arm South NSW 2460", "Arm South TAS 7022", "Armadale VIC 3143", "Armadale WA 6112", "Armadale North VIC 3143", "Armagh NSW 2340");
            Find("ar", 10, "Arable QLD 4728", "Arakoon NSW 2850", "Araluen NSW 2621", "Araluen NSW 2622", "Araluen QLD 4570", "Aramac QLD 4726", "Aramara VIC 3375", "Aramara North QLD 4620", "Arana Hills QLD 4054", "Aranbanga QLD 4625");

            // Should get ACT and Australian Capital Territory now at the start of the list, then the Region of Adelaide.

            Find("a", 10, "ACT", "Australian Capital Territory", "Adelaide", "Aarons Pass NSW 2346", "Abba River WA 6280", "Abbey WA 6280", "Abbeyard QLD 4350", "Abbeywood QLD 4613", "Abbotsbury NSW 2176", "Abbotsford NSW 2046");
            Find("", 10);
        }

        [TestMethod]
        public void PartialMatchesPostcodeTest()
        {
            // Should never get any states here.

            Find("3143 Armadale VIC", 10, "3143 Armadale VIC");
            Find("3143 Armadale VI", 10, "3143 Armadale VIC");
            Find("3143 Armadale V", 10, "3143 Armadale VIC");
            Find("3143 Armadale ", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 Armadale", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 Armadal", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 Armada", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 Armad", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 Arma", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 Arm", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 Ar", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 A", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143 ", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("3143", 10, "3143 Armadale VIC", "3143 Armadale North VIC");
            Find("314", 10, "3140 Lilydale VIC", "3141 South Yarra VIC", "3142 Hawksburn VIC", "3142 Toorak VIC", "3143 Armadale VIC", "3143 Armadale North VIC", "3144 Kooyong VIC", "3144 Malvern VIC", "3144 Sandpalms Roadhouse VIC", "3145 Caulfield East VIC");
            Find("31", 10, "3101 Hackham West VIC", "3101 Kew VIC", "3102 Kew East VIC", "3103 Balwyn VIC", "3103 Cowa VIC", "3103 Spicketts Creek VIC", "3103 Zumsteins VIC", "3104 Balwyn North VIC", "3104 Willow Banks VIC", "3105 Bulleen VIC");
            Find("3", 10, "3000 Melbourne VIC", "3002 East Melbourne VIC", "3003 West Melbourne VIC", "3004 Middle Falbrook VIC", "3004 St Kilda Rd Central VIC", "3005 World Trade Centre VIC", "3006 Innes View VIC", "3006 Southbank VIC", "3008 Docklands VIC", "3010 University Of Melbourne VIC");
            Find("", 10);
        }

        [TestMethod]
        public void PartialMatchesStateTest()
        {
            // Make sure no states are in the list. (See GetPartialMatchedNamedLocationsTest).

            Find("Victoria Hill QLD 4361", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill QLD 436", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill QLD 43", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill QLD 4", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill QLD ", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill QLD", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill QL", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill Q", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill ", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hill", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hil", 10, "Victoria Hill QLD 4361");
            Find("Victoria Hi", 10, "Victoria Hill QLD 4361");
            Find("Victoria H", 10, "Victoria Hill QLD 4361");
            Find("Victoria ", 10, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165");

            // Should now get Victoria at the start of the list.

            Find("Victoria", 10, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Victori", 10, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Victor", 10, "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");
            Find("Victo", 10, "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");
            Find("Vict", 10, "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");

            // Should now get VIC at the start of the list.

            Find("Vic", 10, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            Find("Vi", 10, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            Find("V", 10, "VIC", "Victoria", "V Gate SA 5276", "Vacy NSW 2421", "Valdora QLD 4650", "Vale Of Clwydd VIC 3858", "Vale Park SA 5081", "Vale View QLD 4358", "Valencia Creek VIC 3860", "Valentine NSW 2280");

            Find("", 10);
        }

        [TestMethod]
        public void NoAddressLocationsTest()
        {
            // Should only get QLD here.

            Find("QLD", 10, "QLD");
            Find("QL", 10, "QLD");
            Find("Q", 10, "QLD", "Queensland", "Q Supercentre QLD 4218", "Quaama NSW 2550", "Quairading WA 6383", "Quairading South WA 6383", "Quakers Hill NSW 2763", "Qualco VIC 3579", "Qualeup WA 6394", "Quambatook VIC 3540");
            Find("", 10);
        }

        [TestMethod]
        public void MaximumTest()
        {
            Find("Victoria", 10, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Victoria", 9, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");
            Find("Victoria", 8, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            Find("Victoria", 7, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981");
            Find("Victoria", 6, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101");
            Find("Victoria", 5, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979");
            Find("Victoria", 4, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100");
            Find("Victoria", 3, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361");
            Find("Victoria", 2, "Victoria", "Victoria Estate NSW 2460");
            Find("Victoria", 1, "Victoria");

            // Anything <= 0 should mean ignore the maximum.  In this case 16 will be returned rather than 10.

            Find("Victoria", 0, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294");
            Find("Victoria", -1, "Victoria", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294");

            // Use a location that will slice across the two state / aliases.

            Find("Vic", 10, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            Find("Vic", 9, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981");
            Find("Vic", 8, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101");
            Find("Vic", 7, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979");
            Find("Vic", 6, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100");
            Find("Vic", 5, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361");
            Find("Vic", 4, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460");
            Find("Vic", 3, "VIC", "Victoria", "Victor Harbor SA 5211");
            Find("Vic", 2, "VIC", "Victoria");
            Find("Vic", 1, "VIC");

            // Anything <= 0 should mean ignore the maximum.  In this case 20 will be returned rather than 10.

            Find("Vic", 0, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294", "Victory Heights VIC 3673", "Victory Heights WA 6432");
            Find("Vic", -1, "VIC", "Victoria", "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294", "Victory Heights VIC 3673", "Victory Heights WA 6432");
        }

        [TestMethod]
        public void AbbreviationsBeforeNamesTest()
        {
            Find("N", 4, "NSW", "NT", "New South Wales", "Northern Territory");
            Find("N", 3, "NSW", "NT", "New South Wales");
            Find("N", 2, "NSW", "NT");
            Find("N", 1, "NSW");
        }

        private void Find(string location, int maximum, params string[] expected)
        {
            var matches = _locationQuery.FindPartialMatchedLocations(_australia, location, maximum);
            Assert.IsNotNull(matches);
            Assert.AreEqual(expected.Length, matches.Count);
            for (var index = 0; index < matches.Count; ++index)
                Assert.AreEqual(expected[index], matches[index].Key);
        }
    }
}
