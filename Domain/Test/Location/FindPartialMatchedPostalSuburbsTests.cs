using System.Collections.Generic;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class FindPartialMatchedPostalSuburbsTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = new LocationQuery(new LocationRepository(Resolve<IDataContextFactory>()));
        private Country _australia;

        [TestInitialize]
        public void FindPartialMatchedPostalSuburbsTestsInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
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
            Find("A", 10, "Aarons Pass NSW 2346", "Abba River WA 6280", "Abbey WA 6280", "Abbeyard QLD 4350", "Abbeywood QLD 4613", "Abbotsbury NSW 2176", "Abbotsford NSW 2046", "Abbotsford QLD 4670", "Abbotsford VIC 3067", "Abbotsham TAS 7315");
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
            Find("a", 10, "Aarons Pass NSW 2346", "Abba River WA 6280", "Abbey WA 6280", "Abbeyard QLD 4350", "Abbeywood QLD 4613", "Abbotsbury NSW 2176", "Abbotsford NSW 2046", "Abbotsford QLD 4670", "Abbotsford VIC 3067", "Abbotsham TAS 7315");
            Find("", 10);
        }

        [TestMethod]
        public void PartialMatchesPostcodeTest()
        {
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
            Find("Victoria", 10, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165");
            Find("Victori", 10, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165");
            Find("Victor", 10, "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Victo", 10, "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Vict", 10, "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Vic", 10, "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Vi", 10, "Victor Harbor SA 5211", "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("V", 10, "V Gate SA 5276", "Vacy NSW 2421", "Valdora QLD 4650", "Vale Of Clwydd VIC 3858", "Vale Park SA 5081", "Vale View QLD 4358", "Valencia Creek VIC 3860", "Valentine NSW 2280", "Valentine WA 6532", "Valentine Plains VIC 3579");
            Find("", 10);
        }

        [TestMethod]
        public void PartialMatchesSynonymTest()
        {
            Find("North Shore VIC 3214", 10, "North Shore VIC 3214");
            Find("North Shore VIC 321", 10, "North Shore VIC 3214");
            Find("North Shore VIC 32", 10, "North Shore VIC 3214");
            Find("North Shore VIC 3", 10, "North Shore VIC 3214");
            Find("North Shore VIC ", 10, "North Shore VIC 3214");
            Find("North Shore VIC", 10, "North Shore VIC 3214");
            Find("North Shore VI", 10, "North Shore VIC 3214");
            Find("North Shore V", 10, "North Shore VIC 3214");
            Find("North Shore ", 10, "North Shore NSW 2795", "North Shore QLD 4565", "North Shore VIC 3214");
            Find("North Shore", 10, "North Shore NSW 2795", "North Shore QLD 4565", "North Shore VIC 3214");
            Find("North Shor", 10, "North Shore NSW 2795", "North Shore QLD 4565", "North Shore VIC 3214");
            Find("North Sho", 10, "North Shore NSW 2795", "North Shore QLD 4565", "North Shore VIC 3214");
            Find("North Sh", 10, "North Shepparton NSW 2360", "North Shields VIC 3898", "North Shore NSW 2795", "North Shore QLD 4565", "North Shore VIC 3214");
            Find("North S", 10, "North Sackville NSW 2722", "North Salisbury SA 5108", "North Scottsdale NSW 2671", "North Shepparton NSW 2360", "North Shields VIC 3898", "North Shore NSW 2795", "North Shore QLD 4565", "North Shore VIC 3214", "North Skenes Creek NSW 2630", "North St Marys SA 5271");
            Find("North ", 10, "North Adelaide SA 5006", "North Adelaide Melbourne St SA 5006", "North Albury NSW 2640", "North Allendale NSW 2627", "North Altona VIC 3025", "North Anabranch SA 5602", "North Applecross WA 6153", "North Aramara QLD 4620", "North Arm NSW 2323", "North Arm QLD 4871");
            Find("North", 10, "North Adelaide SA 5006", "North Adelaide Melbourne St SA 5006", "North Albury NSW 2640", "North Allendale NSW 2627", "North Altona VIC 3025", "North Anabranch SA 5602", "North Applecross WA 6153", "North Aramara QLD 4620", "North Arm NSW 2323", "North Arm QLD 4871");
            Find("Nort", 10, "North Adelaide SA 5006", "North Adelaide Melbourne St SA 5006", "North Albury NSW 2640", "North Allendale NSW 2627", "North Altona VIC 3025", "North Anabranch SA 5602", "North Applecross WA 6153", "North Aramara QLD 4620", "North Arm NSW 2323", "North Arm QLD 4871");
            Find("Nor", 10, "Nora Creina TAS 7270", "Noradjuha VIC 3409", "Norah Head NSW 2263", "Noranda WA 6062", "Noraville NSW 2263", "Nords Wharf NSW 2281", "Norfolk Island NSW 2899", "Norlane VIC 3214", "Norley VIC 3377", "Norman Gardens QLD 4701");
            Find("No", 10, "No. 4 Branch QLD 4856", "No. 5 Branch QLD 4856", "No. 6 Branch QLD 4859", "Noah NSW 2824", "Noarlunga Centre SA 5168", "Noarlunga Downs SA 5168", "Nobby QLD 4360", "Nobby Beach QLD 4218", "Nobbys Creek NSW 2441", "Noble Park VIC 3174");
            Find("N", 10, "Nabageena TAS 7330", "Nabawa WA 6532", "Nabawa East WA 6532", "Nabiac NSW 2312", "Nabowla TAS 7260", "Nackara QLD 4854", "Nadgee NSW 2769", "Nadia NT 0832", "Nagambie VIC 3608", "Nahrunda QLD 4570");

            Find("Nth Shore VIC 3214", 10, "Nth Shore VIC 3214");
            Find("Nth Shore VIC 321", 10, "Nth Shore VIC 3214");
            Find("Nth Shore VIC 32", 10, "Nth Shore VIC 3214");
            Find("Nth Shore VIC 3", 10, "Nth Shore VIC 3214");
            Find("Nth Shore VIC ", 10, "Nth Shore VIC 3214");
            Find("Nth Shore VIC", 10, "Nth Shore VIC 3214");
            Find("Nth Shore VI", 10, "Nth Shore VIC 3214");
            Find("Nth Shore V", 10, "Nth Shore VIC 3214");
            Find("Nth Shore ", 10, "Nth Shore NSW 2795", "Nth Shore QLD 4565", "Nth Shore VIC 3214");
            Find("Nth Shore", 10, "Nth Shore NSW 2795", "Nth Shore QLD 4565", "Nth Shore VIC 3214");
            Find("Nth Shor", 10, "Nth Shore NSW 2795", "Nth Shore QLD 4565", "Nth Shore VIC 3214");
            Find("Nth Sho", 10, "Nth Shore NSW 2795", "Nth Shore QLD 4565", "Nth Shore VIC 3214");
            Find("Nth Sh", 10, "Nth Shepparton NSW 2360", "Nth Shields VIC 3898", "Nth Shore NSW 2795", "Nth Shore QLD 4565", "Nth Shore VIC 3214");
            Find("Nth S", 10, "Nth Sackville NSW 2722", "Nth Salisbury SA 5108", "Nth Scottsdale NSW 2671", "Nth Shepparton NSW 2360", "Nth Shields VIC 3898", "Nth Shore NSW 2795", "Nth Shore QLD 4565", "Nth Shore VIC 3214", "Nth Skenes Creek NSW 2630", "Nth St Marys SA 5271");
            Find("Nth ", 10, "Nth Adelaide SA 5006", "Nth Adelaide Melbourne St SA 5006", "Nth Albury NSW 2640", "Nth Allendale NSW 2627", "Nth Altona VIC 3025", "Nth Anabranch SA 5602", "Nth Applecross WA 6153", "Nth Aramara QLD 4620", "Nth Arm NSW 2323", "Nth Arm QLD 4871");
            Find("Nth", 10, "Nth Adelaide SA 5006", "Nth Adelaide Melbourne St SA 5006", "Nth Albury NSW 2640", "Nth Allendale NSW 2627", "Nth Altona VIC 3025", "Nth Anabranch SA 5602", "Nth Applecross WA 6153", "Nth Aramara QLD 4620", "Nth Arm NSW 2323", "Nth Arm QLD 4871");
            Find("Nt", 10, "Nth Adelaide SA 5006", "Nth Adelaide Melbourne St SA 5006", "Nth Albury NSW 2640", "Nth Allendale NSW 2627", "Nth Altona VIC 3025", "Nth Anabranch SA 5602", "Nth Applecross WA 6153", "Nth Aramara QLD 4620", "Nth Arm NSW 2323", "Nth Arm QLD 4871");
            Find("N", 10, "Nabageena TAS 7330", "Nabawa WA 6532", "Nabawa East WA 6532", "Nabiac NSW 2312", "Nabowla TAS 7260", "Nackara QLD 4854", "Nadgee NSW 2769", "Nadia NT 0832", "Nagambie VIC 3608", "Nahrunda QLD 4570");

            Find("N Shore VIC 3214", 10, "N Shore VIC 3214");
            Find("N Shore VIC 321", 10, "N Shore VIC 3214");
            Find("N Shore VIC 32", 10, "N Shore VIC 3214");
            Find("N Shore VIC 3", 10, "N Shore VIC 3214");
            Find("N Shore VIC ", 10, "N Shore VIC 3214");
            Find("N Shore VIC", 10, "N Shore VIC 3214");
            Find("N Shore VI", 10, "N Shore VIC 3214");
            Find("N Shore V", 10, "N Shore VIC 3214");
            Find("N Shore ", 10, "N Shore NSW 2795", "N Shore QLD 4565", "N Shore VIC 3214");
            Find("N Shore", 10, "N Shore NSW 2795", "N Shore QLD 4565", "N Shore VIC 3214");
            Find("N Shor", 10, "N Shore NSW 2795", "N Shore QLD 4565", "N Shore VIC 3214");
            Find("N Sho", 10, "N Shore NSW 2795", "N Shore QLD 4565", "N Shore VIC 3214");
            Find("N Sh", 10, "N Shepparton NSW 2360", "N Shields VIC 3898", "N Shore NSW 2795", "N Shore QLD 4565", "N Shore VIC 3214");
            Find("N S", 10, "N Sackville NSW 2722", "N Salisbury SA 5108", "N Scottsdale NSW 2671", "N Shepparton NSW 2360", "N Shields VIC 3898", "N Shore NSW 2795", "N Shore QLD 4565", "N Shore VIC 3214", "N Skenes Creek NSW 2630", "N St Marys SA 5271");
            Find("N ", 10, "N Adelaide SA 5006", "N Adelaide Melbourne St SA 5006", "N Albury NSW 2640", "N Allendale NSW 2627", "N Altona VIC 3025", "N Anabranch SA 5602", "N Applecross WA 6153", "N Aramara QLD 4620", "N Arm NSW 2323", "N Arm QLD 4871");
            Find("N", 10, "Nabageena TAS 7330", "Nabawa WA 6532", "Nabawa East WA 6532", "Nabiac NSW 2312", "Nabowla TAS 7260", "Nackara QLD 4854", "Nadgee NSW 2769", "Nadia NT 0832", "Nagambie VIC 3608", "Nahrunda QLD 4570");

            Find("Shore North VIC 3214", 10, "Shore North VIC 3214");
            Find("Shore North VIC 321", 10, "Shore North VIC 3214");
            Find("Shore North VIC 32", 10, "Shore North VIC 3214");
            Find("Shore North VIC 3", 10, "Shore North VIC 3214");
            Find("Shore North VIC ", 10, "Shore North VIC 3214");
            Find("Shore North VIC", 10, "Shore North VIC 3214");
            Find("Shore North VI", 10, "Shore North VIC 3214");
            Find("Shore North V", 10, "Shore North VIC 3214");
            Find("Shore North ", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
            Find("Shore North", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
            Find("Shore Nort", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
            Find("Shore Nor", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
            Find("Shore No", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
            Find("Shore N", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
            Find("Shore ", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
            Find("Shore", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214", "Shoreham VIC 3916", "Shorewell Park VIC 3260");

            Find("Shore Nth VIC 3214", 10, "Shore Nth VIC 3214");
            Find("Shore Nth VIC 321", 10, "Shore Nth VIC 3214");
            Find("Shore Nth VIC 32", 10, "Shore Nth VIC 3214");
            Find("Shore Nth VIC 3", 10, "Shore Nth VIC 3214");
            Find("Shore Nth VIC ", 10, "Shore Nth VIC 3214");
            Find("Shore Nth VIC", 10, "Shore Nth VIC 3214");
            Find("Shore Nth VI", 10, "Shore Nth VIC 3214");
            Find("Shore Nth V", 10, "Shore Nth VIC 3214");
            Find("Shore Nth ", 10, "Shore Nth NSW 2795", "Shore Nth QLD 4565", "Shore Nth VIC 3214");
            Find("Shore Nth", 10, "Shore Nth NSW 2795", "Shore Nth QLD 4565", "Shore Nth VIC 3214");
            Find("Shore Nt", 10, "Shore Nth NSW 2795", "Shore Nth QLD 4565", "Shore Nth VIC 3214");
            Find("Shore N", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");

            Find("Shore N VIC 3214", 10, "Shore N VIC 3214");
            Find("Shore N VIC 321", 10, "Shore N VIC 3214");
            Find("Shore N VIC 32", 10, "Shore N VIC 3214");
            Find("Shore N VIC 3", 10, "Shore N VIC 3214");
            Find("Shore N VIC ", 10, "Shore N VIC 3214");
            Find("Shore N VIC", 10, "Shore N VIC 3214");
            Find("Shore N VI", 10, "Shore N VIC 3214");
            Find("Shore N V", 10, "Shore N VIC 3214");
            Find("Shore N ", 10, "Shore N NSW 2795", "Shore N QLD 4565", "Shore N VIC 3214");
            Find("Shore N", 10, "Shore North NSW 2795", "Shore North QLD 4565", "Shore North VIC 3214");
        }

        [TestMethod]
        public void MaximumTest()
        {
            Find("Victoria", 10, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165");
            Find("Victoria", 9, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480");
            Find("Victoria", 8, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850");
            Find("Victoria", 7, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751");
            Find("Victoria", 6, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981");
            Find("Victoria", 5, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101");
            Find("Victoria", 4, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979");
            Find("Victoria", 3, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100");
            Find("Victoria", 2, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361");
            Find("Victoria", 1, "Victoria Estate NSW 2460");

            // Anything <= 0 should mean ignore the maximum.  In this case 16 will be returned rather than 10.

            Find("Victoria", 0, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294");
            Find("Victoria", -1, "Victoria Estate NSW 2460", "Victoria Hill QLD 4361", "Victoria Park WA 6100", "Victoria Park WA 6979", "Victoria Park East WA 6101", "Victoria Park East WA 6981", "Victoria Plains QLD 4751", "Victoria Plantation QLD 4850", "Victoria Point NSW 2480", "Victoria Point QLD 4165", "Victoria Point West SA 5114", "Victoria River SA 5357", "Victoria River Downs NT 0852", "Victoria Rock WA 6429", "Victoria Vale VIC 3533", "Victoria Valley TAS 7140", "Victoria Valley VIC 3294");
        }

        [TestMethod]
        public void NoPartialMatchesTest()
        {
            Find("QLD", 10);
            Find("QL", 10);
            Find("Q", 10, "Q Supercentre QLD 4218", "Quaama NSW 2550", "Quairading WA 6383", "Quairading South WA 6383", "Quakers Hill NSW 2763", "Qualco VIC 3579", "Qualeup WA 6394", "Quambatook VIC 3540", "Quambone NSW 2831", "Quamby Bend VIC 3530");
            Find("", 10);
        }

        [TestMethod]
        public void TestCompareSuburbFirst()
        {
            // Test suburb first.

            Find("Melbourne", 10, "Melbourne VIC 3000", "Melbourne Airport VIC 3045", "Melbourne DC South VIC 3205", "Melbourne East VIC 3002", "Melbourne East VIC 8002", "Melbourne North VIC 3051", "Melbourne South VIC 3205", "Melbourne University VIC 3052", "Melbourne West VIC 3003");

            // Test postcode first.

            Find("4000 Brisbane", 10, "4000 Brisbane QLD", "4000 Brisbane Adelaide Street QLD");
        }

        private void Find(string location, int maximum, params string[] expected)
        {
            IList<PartialMatch> matches = _locationQuery.FindPartialMatchedPostalSuburbs(_australia, location, maximum);
            Assert.IsNotNull(matches);
            Assert.AreEqual(expected.Length, matches.Count);

            for (var index = 0; index < matches.Count; ++index)
                Assert.AreEqual(expected[index], matches[index].Key);
        }
    }
}
