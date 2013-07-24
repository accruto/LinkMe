using System.Collections.Generic;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Location
{
    [TestClass]
    public class ResolvePostalSuburbTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = new LocationQuery(new LocationRepository(Resolve<IDataContextFactory>()));
        private Country _australia;

        [TestInitialize]
        public void ResolvePostalSuburbTestsInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
        }

        [TestMethod]
        public void TestResolveAliasSuburbExactPostcodeState()
        {
            // St -> Saint

            Resolve(_australia, "Saint Helena VIC 3088", "Saint Helena", "3088");
            Resolve(_australia, "St Helena VIC 3088", "Saint Helena", "3088");

            // Saint does not support suffix.

            TryResolve(_australia, "Helena Saint VIC 3088", false, "VIC", "3088");
            TryResolve(_australia, "Helena St VIC 3088", false, "VIC", "3088");

            // Pt - > Port

            Resolve(_australia, "Port Adelaide SA 5015", "Port Adelaide", "5015");
            Resolve(_australia, "Pt Adelaide SA 5015", "Port Adelaide", "5015");
            Resolve(_australia, "Port Melbourne VIC 3207", "Port Melbourne", "3207");
            Resolve(_australia, "Pt Melbourne VIC 3207", "Port Melbourne", "3207");
            Resolve(_australia, "Portarlington VIC 3223", "Portarlington", "3223");
            TryResolve(_australia, "Ptarlington VIC 3223", false, "VIC", "3223");

            // Port does not support suffix (actually matches a different suburb because of SoundEx).

            TryResolve(_australia, "Adelaide Port SA 5015", false, "SA", "5015");
            TryResolve(_australia, "Adelaide Pt SA 5015", false, "SA", "5015");

            // N, Nth - > North

            // Defined as 'North X'

            Resolve(_australia, "North Adelaide SA 5006", "North Adelaide", "5006");
            Resolve(_australia, "Nth Adelaide SA 5006", "North Adelaide", "5006");
            Resolve(_australia, "N Adelaide SA 5006", "North Adelaide", "5006");
            Resolve(_australia, "Adelaide North SA 5006", "North Adelaide", "5006");
            Resolve(_australia, "Adelaide Nth SA 5006", "North Adelaide", "5006");
            Resolve(_australia, "Adelaide N SA 5006", "North Adelaide", "5006");

            Resolve(_australia, "North Shore VIC 3214", "North Shore", "3214");
            Resolve(_australia, "Nth Shore VIC 3214", "North Shore", "3214");
            Resolve(_australia, "N Shore VIC 3214", "North Shore", "3214");
            Resolve(_australia, "Northam WA 6401", "Northam", "6401");
            TryResolve(_australia, "Ntham WA 6401", false, "WA", "6401");
            TryResolve(_australia, "Nam WA 6401", false, "WA", "6401");

            // Defined as 'X North'

            Resolve(_australia, "Croydon North VIC 3136", "Croydon North", "3136");
            Resolve(_australia, "Croydon Nth VIC 3136", "Croydon North", "3136");
            Resolve(_australia, "Croydon N VIC 3136", "Croydon North", "3136");
            Resolve(_australia, "North Croydon VIC 3136", "Croydon North", "3136");
            Resolve(_australia, "Nth Croydon VIC 3136", "Croydon North", "3136");
            Resolve(_australia, "N Croydon VIC 3136", "Croydon North", "3136");

            // S, Sth -> South

            // Defined as 'South X'

            Resolve(_australia, "South Yarra VIC 3141", "South Yarra", "3141");
            Resolve(_australia, "Sth Yarra VIC 3141", "South Yarra", "3141");
            Resolve(_australia, "S Yarra VIC 3141", "South Yarra", "3141");
            Resolve(_australia, "Yarra South VIC 3141", "South Yarra", "3141");
            Resolve(_australia, "Yarra Sth VIC 3141", "South Yarra", "3141");
            Resolve(_australia, "Yarra S VIC 3141", "South Yarra", "3141");

            Resolve(_australia, "South Bingera QLD 4670", "South Bingera", "4670");
            Resolve(_australia, "Sth Bingera QLD 4670", "South Bingera", "4670");
            Resolve(_australia, "S Bingera QLD 4670", "South Bingera", "4670");
            Resolve(_australia, "Southampton WA 6253", "Southampton", "6253");
            TryResolve(_australia, "Sampton WA 6253", false, "WA", "6253");

            // Defined as 'X South'

            Resolve(_australia, "Box Hill South VIC 3128", "Box Hill South", "3128");
            Resolve(_australia, "Box Hill Sth VIC 3128", "Box Hill South", "3128");
            Resolve(_australia, "Box Hill S VIC 3128", "Box Hill South", "3128");
            Resolve(_australia, "South Box Hill VIC 3128", "Box Hill South", "3128");
            Resolve(_australia, "Sth Box Hill VIC 3128", "Box Hill South", "3128");
            Resolve(_australia, "S Box Hill VIC 3128", "Box Hill South", "3128");

            // E -> East

            // Defined as 'East X'

            Resolve(_australia, "East Brisbane QLD 4169", "East Brisbane", "4169");
            Resolve(_australia, "E Brisbane QLD 4169", "East Brisbane", "4169");
            Resolve(_australia, "Brisbane East QLD 4169", "East Brisbane", "4169");
            Resolve(_australia, "Brisbane E QLD 4169", "East Brisbane", "4169");

            Resolve(_australia, "Eastern Creek NSW 2766", "Eastern Creek", "2766");
            TryResolve(_australia, "Eern Creek NSW 2766", false, "NSW", "2766");

            // Defined as 'X East'

            Resolve(_australia, "Camberwell East VIC 3126", "Camberwell East", "3126");
            Resolve(_australia, "Camberwell E VIC 3126", "Camberwell East", "3126");
            Resolve(_australia, "East Camberwell VIC 3126", "Camberwell East", "3126");
            Resolve(_australia, "E Camberwell VIC 3126", "Camberwell East", "3126");

            // W -> West

            // Defined as 'West X'

            Resolve(_australia, "West Ballidu WA 6606", "West Ballidu", "6606");
            Resolve(_australia, "W Ballidu WA 6606", "West Ballidu", "6606");
            Resolve(_australia, "Ballidu West WA 6606", "West Ballidu", "6606");
            Resolve(_australia, "Ballidu W WA 6606", "West Ballidu", "6606");

            Resolve(_australia, "Western Junction TAS 7212", "Western Junction", "7212");
            TryResolve(_australia, "Wern Junction TAS 7212", false, "TAS", "7212");

            // Defined as 'X West'

            Resolve(_australia, "Geelong West VIC 3218", "Geelong West", "3218");
            Resolve(_australia, "Geelong W VIC 3218", "Geelong West", "3218");
            Resolve(_australia, "West Geelong VIC 3218", "Geelong West", "3218");
            Resolve(_australia, "W Geelong VIC 3218", "Geelong West", "3218");
        }

        [TestMethod]
        public void TestResolveExactSuburbExactPostcodeState()
        {
            var malaysia = _locationQuery.GetCountry("Malaysia");
            TryResolve(malaysia, "11700", false, null, null);

            // Unique suburb.

            Resolve(_australia, "Norlane 3214 VIC", "Norlane", "3214");

            // Differentiated by postcode and state.

            Resolve(_australia, "Camberwell 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 2330 NSW", "Camberwell", "2330");

            // Differentiated by postcode.

            Resolve(_australia, "Newtown 4305 QLD", "Newtown", "4305");
            Resolve(_australia, "Newtown 4350 QLD", "Newtown", "4350");

            // Differentiated by suburb.

            Resolve(_australia, "Corio 3214 VIC", "Corio", "3214");
            Resolve(_australia, "Berowra Waters 2082 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra Heights 2082 NSW", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolveSuburbContainingState()
        {
            // This location has a suburb name which contains a state.

            Resolve(_australia, "Victoria Park WA 6979", "Victoria Park", "6979");
            Resolve(_australia, "WA Victoria Park 6979", "Victoria Park", "6979");

            // Without the postcode.

            Resolve(_australia, "Victoria Hill QLD 4361", "Victoria Hill", "4361");
            Resolve(_australia, "QLD Victoria Hill 4361", "Victoria Hill", "4361");
            Resolve(_australia, "Victoria Hill QLD", "Victoria Hill", "4361");
            Resolve(_australia, "QLD Victoria Hill", "Victoria Hill", "4361");

            // Same suburb in different states.

            Resolve(_australia, "Victoria Valley TAS 7140", "Victoria Valley", "7140");
            Resolve(_australia, "TAS Victoria Valley 7140", "Victoria Valley", "7140");

            Resolve(_australia, "Victoria Valley TAS", "Victoria Valley", "7140");
            Resolve(_australia, "TAS Victoria Valley", "Victoria Valley", "7140");

            Resolve(_australia, "Victoria Valley VIC", "Victoria Valley", "3294");
            Resolve(_australia, "VIC Victoria Valley", "Victoria Valley", "3294");

            Resolve(_australia, "Victoria TAS", "Victoria Valley", "7140");
            Resolve(_australia, "TAS Victoria", "Victoria Valley", "7140");

            // Containing.

            Resolve(_australia, "Port Victoria SA", "Port Victoria", "5573");
            Resolve(_australia, "SA Port Victoria", "Port Victoria", "5573");

            Resolve(_australia, "ort Victoria SA", "Port Victoria", "5573");
            Resolve(_australia, "SA ort Victoria", "Port Victoria", "5573");

            Resolve(_australia, "Mount Victoria NSW", "Mount Victoria", "2786");
            Resolve(_australia, "NSW Mount Victoria", "Mount Victoria", "2786");

            Resolve(_australia, "ount Victoria NSW", "Mount Victoria", "2786");
            Resolve(_australia, "NSW ount Victoria", "Mount Victoria", "2786");
            
            // Should not resolve.

            TryResolve(_australia, "Queensland Tasmania", false, null, null);
            TryResolve(_australia, "Tasmania Queensland", false, null, null);
        }

        [TestMethod]
        public void TestResolveExactSuburbPartialPostcodeState()
        {
            // Unique suburb.

            Resolve(_australia, "Norlane 321 VIC", "Norlane", "3214");
            Resolve(_australia, "Norlane 32 VIC", "Norlane", "3214");
            Resolve(_australia, "Norlane 3 VIC", "Norlane", "3214");

            // Differentiated by postcode and state.

            Resolve(_australia, "Camberwell 312 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 31 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 3 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 233 NSW", "Camberwell", "2330");
            Resolve(_australia, "Camberwell 23 NSW", "Camberwell", "2330");
            Resolve(_australia, "Camberwell 2 NSW", "Camberwell", "2330");

            // Differentiated by postcode.

            Resolve(_australia, "Newtown 430 QLD", "Newtown", "4305");
            Resolve(_australia, "Newtown 435 QLD", "Newtown", "4350");
            TryResolve(_australia, "Newtown 43 QLD", true, "QLD", null);
            TryResolve(_australia, "Newtown 4 QLD", true, "QLD", null);

            // Differentiated by suburb.

            Resolve(_australia, "Corio 321 VIC", "Corio", "3214");
            Resolve(_australia, "Corio 32 VIC", "Corio", "3214");
            Resolve(_australia, "Corio 3 VIC", "Corio", "3214");
            Resolve(_australia, "Berowra Waters 208 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra Heights 208 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Berowra Waters 20 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra Heights 20 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Berowra Waters 2 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra Heights 2 NSW", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolvePartialSuburbExactPostcodeState()
        {
            // Unique suburb.

            Resolve(_australia, "Norlan 3214 VIC", "Norlane", "3214");
            Resolve(_australia, "Norla 3214 VIC", "Norlane", "3214");
            Resolve(_australia, "Norl 3214 VIC", "Norlane", "3214");

            // 2 possibilities - other is North Shore

            Resolve(_australia, "Nor 3214 VIC", "Norlane", "3214");

            // 4 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West

            Resolve(_australia, "Camberwel 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camberwe 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camberw 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camber 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Cambe 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Camb 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Cam 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "Ca 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "C 3124 VIC", "Camberwell", "3124");

            Resolve(_australia, "Camberwell 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "Camberwel 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "Camberwe 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "Camberw 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "Camber 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "Cambe 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "Camb 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "Cam 2330 NSW", "Camberwell", "2330");

            // 2 possibilities - other is Carrowbrook

            Resolve(_australia, "Ca 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "C 2330 NSW", "Camberwell", "2330");

            // Differentiated by postcode.

            Resolve(_australia, "Newtow 4305 QLD", "Newtown", "4305");
            Resolve(_australia, "Newtow 4350 QLD", "Newtown", "4350");

            // Differentiated by suburb.

            Resolve(_australia, "Berowra W 2082 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra H 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Berowra 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Berowr 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Berow 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Bero 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Ber 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "Be 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "B 2082 NSW", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolvePartialSuburbPartialPostcodeState()
        {
            // Unique suburb.

            Resolve(_australia, "Norlan 321 VIC", "Norlane", "3214");
            Resolve(_australia, "Norla 321 VIC", "Norlane", "3214");
            Resolve(_australia, "Norl 321 VIC", "Norlane", "3214");

            // 3 possibilities - Norlane, North Shore 3214 VIC, North Geelong 3215 VIC

            TryResolve(_australia, "Nor 321 VIC", true, "VIC", null);

            // 5 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West 3124 VIC, Camberwell East 3126

            TryResolve(_australia, "Camberwel 312 VIC", true, "VIC", null);
            TryResolve(_australia, "Camberwe 312 VIC", true, "VIC", null);
            TryResolve(_australia, "Camberw 312 VIC", true, "VIC", null);
            TryResolve(_australia, "Camber 312 VIC", true, "VIC", null);
            TryResolve(_australia, "Cambe 312 VIC", true, "VIC", null);
            TryResolve(_australia, "Camb 312 VIC", true, "VIC", null);
            TryResolve(_australia, "Cam 312 VIC", true, "VIC", null);

            // 6 possibilities - Canterbury 3126 VIC

            TryResolve(_australia, "Ca 312 VIC", true, "VIC", null);

            // 7 possibilities - Cremorne 3121 VIC

            TryResolve(_australia, "C 312 VIC", true, "VIC", null);

            // 1 possibility

            Resolve(_australia, "Stirlin 291", "Stirling", "2914");
            Resolve(_australia, "Stirli 291", "Stirling", "2914");
            Resolve(_australia, "Stirl 291", "Stirling", "2914");
            Resolve(_australia, "Stir 291", "Stirling", "2914");
            Resolve(_australia, "Sti 291", "Stirling", "2914");

            // 2 possibilities - other is Strathmore

            Resolve(_australia, "St 291", "Stirling", "2914");
            Resolve(_australia, "S 291", "Stirling", "2914");

            // Differentiated by postcode.

            Resolve(_australia, "Newtow 430 QLD", "Newtown", "4305");
            Resolve(_australia, "Newtow 435 QLD", "Newtown", "4350");
            TryResolve(_australia, "Newtow 43 QLD", true, "QLD", null);

            // Differentiated by suburb.

            Resolve(_australia, "Berowra W 208 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra H 208 NSW", "Berowra Heights", "2082");

            // 3 possibilities, Berowra 2081 NSW, Berowra Heights 2082 NSW, Berowra Waters 2082 NSW

            TryResolve(_australia, "Berowr 208 NSW", true, "NSW", null);
            TryResolve(_australia, "Berow 208 NSW", true, "NSW", null);
            TryResolve(_australia, "Bero 208 NSW", true, "NSW", null);
            TryResolve(_australia, "Ber 208 NSW", true, "NSW", null);

            // 5 possibilities - Belrose, Belrose West

            TryResolve(_australia, "Be 208 NSW", true, "NSW", null);

            // 6 possibilities - Brooklyn

            TryResolve(_australia, "B 208 NSW", true, "NSW", null);
        }

        [TestMethod]
        public void TestResolveContainingSuburbExactPostcodeState()
        {
            // Unique suburb.

            Resolve(_australia, "orlan 3214 VIC", "Norlane", "3214");
            Resolve(_australia, "orla 3214 VIC", "Norlane", "3214");
            Resolve(_australia, "orl 3214 VIC", "Norlane", "3214");
            Resolve(_australia, "rl 3214 VIC", "Norlane", "3214");

            // 5 possibilities - Camberwell, Camberwell North, Camberwell South, Camberwell West, Middle Camberwell

            Resolve(_australia, "amberwel 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "berw 3124 VIC", "Camberwell", "3124");
            Resolve(_australia, "er 3124 VIC", "Camberwell", "3124");

            // 1 possibility - Camberwell

            Resolve(_australia, "amberwel 2330 NSW", "Camberwell", "2330");
            Resolve(_australia, "berw 2330 NSW", "Camberwell", "2330");

            // 1 possibility - Jerrys Plains

            Resolve(_australia, "err 2330 NSW", "Jerrys Plains", "2330");

            // 2 possibilities - Camberwell, Jerrys Plains

            Resolve(_australia, "er 2330 NSW", "Camberwell", "2330");

            // 1 possibility - Newtown

            Resolve(_australia, "ewtow 4305 QLD", "Newtown", "4305");
            Resolve(_australia, "wto 4305 QLD", "Newtown", "4305");
            Resolve(_australia, "ewtow 4350 QLD", "Newtown", "4350");
            Resolve(_australia, "wto 4350 QLD", "Newtown", "4350");

            // 1 possibility - Berowra Waters

            Resolve(_australia, "erowra Water 2082 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "owra Wat 2082 NSW", "Berowra Waters", "2082");
            Resolve(_australia, "ra W 2082 NSW", "Berowra Waters", "2082");

            // 1 possibility - Berowra Heights

            Resolve(_australia, "erowra Height 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "owra Heig 2082 NSW", "Berowra Heights", "2082");
            Resolve(_australia, "ra Hei 2082 NSW", "Berowra Heights", "2082");

            // 2 possibilities - Berowra Waters, Berowra Heights

            Resolve(_australia, "ra 2082 NSW", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolveContainingSuburbPartialPostcodeState()
        {
            // Unique suburb.

            Resolve(_australia, "orlan 321 VIC", "Norlane", "3214");
            Resolve(_australia, "rl 321 VIC", "Norlane", "3214");

            // 4 possibilities - Corio, Norlane, North Shore 3214 VIC, North Geelong 3215 VIC

            TryResolve(_australia, "or 321 VIC", true, "VIC", null);

            // 6 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West, Middle Camberwell 3124 VIC, Camberwell East 3126

            TryResolve(_australia, "amberwel 312 VIC", true, "VIC", null);
            TryResolve(_australia, "mberw 312 VIC", true, "VIC", null);

            // 1 possibility

            Resolve(_australia, "amberwel 233 NSW", "Camberwell", "2330");

            // 2 possibilities - Aberdeen 2336

            TryResolve(_australia, "ber 233 NSW", true, "NSW", null);

            // Differentiated by postcode.

            Resolve(_australia, "ewtow 430 QLD", "Newtown", "4305");
            Resolve(_australia, "ewtow 435 QLD", "Newtown", "4350");
            TryResolve(_australia, "ewtow 43 QLD", true, "QLD", null);

            // 3 possibilities, Berowra 2081 NSW, Berowra Heights 2082 NSW, Berowra Waters 2082 NSW

            TryResolve(_australia, "erowr 208 NSW", true, "NSW", null);
            TryResolve(_australia, "row 208 NSW", true, "NSW", null);

            // 5 possibilities - Belrose, Belrose West

            TryResolve(_australia, "ro 208 NSW", true, "NSW", null);

            // 2 possibilities - Belrose, Belrose West

            Resolve(_australia, "elros 208 NSW", "Belrose", "2085");
        }

        [TestMethod]
        public void TestResolveSoundExSuburbExactPostcodeState()
        {
            // 1 possibility

            Resolve(_australia, "Nrlan 3214 VIC", "Norlane", "3214");

            // 4 possibilities - Camberwell, Camberwell North, Camberwell South, Camberwell West

            Resolve(_australia, "Cmberwel 3124 VIC", "Camberwell", "3124");

            // 1 possibility

            Resolve(_australia, "Cmberwel 2330 NSW", "Camberwell", "2330");

            // 1 possibility

            Resolve(_australia, "Nwtown 4305 QLD", "Newtown", "4305");
            Resolve(_australia, "Nwtown 4350 QLD", "Newtown", "4350");

            // 2 possibilities - Berowra Heights, Berowra Waters

            Resolve(_australia, "Browra 2082 NSW", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolveSoundExSuburbPartialPostcodeState()
        {
            // Unique suburb.

            Resolve(_australia, "Nrlan 321 VIC", "Norlane", "3214");

            // 5 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West 3124 VIC, Camberwell East 3126

            TryResolve(_australia, "Cmberwell 312 VIC", true, "VIC", null);

            // 1 possibility

            Resolve(_australia, "Cmberwel 233 NSW", "Camberwell", "2330");

            // Differentiated by postcode.

            Resolve(_australia, "Nwtown 430 QLD", "Newtown", "4305");
            Resolve(_australia, "Nwtown 435 QLD", "Newtown", "4350");
            TryResolve(_australia, "Nwtown 43 QLD", true, "QLD", null);

            // 2 possibilities - Belrose, Belrose West

            Resolve(_australia, "Blros 208 NSW", "Belrose", "2085");
        }

        [TestMethod]
        public void TestResolveExactPostcodeState()
        {
            // 3 possibilities: Corio, Norlane, North Shore 3214 VIC

            Resolve(_australia, "3214 VIC", "Corio", "3214");

            // 3001 is a postcode without any suburbs.

            TryResolve(_australia, "3001 VIC", false, "VIC", "3001");
        }

        [TestMethod]
        public void TestResolveExactSuburbState()
        {
            // 1 possibility: Norlane 3214 VIC

            Resolve(_australia, "Norlane VIC", "Norlane", "3214");

            // 2 possibilities: Newtown 4305 QLD, Newtown 4350 QLD

            TryResolve(_australia, "Newtown QLD", true, "QLD", null);
        }

        [TestMethod]
        public void TestResolvePartialSuburbState()
        {
            // Unique suburb.

            Resolve(_australia, "Norlan VIC", "Norlane", "3214");
            Resolve(_australia, "Norla VIC", "Norlane", "3214");
            
            // 2 possibilities
            
            TryResolve(_australia, "Norl VIC", true, "VIC", null);

            // 11 possibilities

            TryResolve(_australia, "Nor VIC", true, "VIC", null);

            // 5 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West 3124 VIC, Camberwell East 3126

            TryResolve(_australia, "Camberwel VIC", true, "VIC", null);
            TryResolve(_australia, "Camberwe VIC", true, "VIC", null);
            TryResolve(_australia, "Camberw VIC", true, "VIC", null);
            TryResolve(_australia, "Camber VIC", true, "VIC", null);
            TryResolve(_australia, "Cambe VIC", true, "VIC", null);
            TryResolve(_australia, "Camb VIC", true, "VIC", null);

            // 12 possibilities

            TryResolve(_australia, "Cam VIC", true, "VIC", null);

            // 60 possibilities

            TryResolve(_australia, "Ca VIC", true, "VIC", null);

            // 166 possibilities

            TryResolve(_australia, "C VIC", true, "VIC", null);

            // 1 possibility

            Resolve(_australia, "Camberwel NSW", "Camberwell", "2330");
            Resolve(_australia, "Camberwe NSW", "Camberwell", "2330");
            Resolve(_australia, "Camberw NSW", "Camberwell", "2330");
            Resolve(_australia, "Camber NSW", "Camberwell", "2330");

            // Multiple possibilities

            TryResolve(_australia, "Cambe NSW", true, "NSW", null);
            TryResolve(_australia, "Camb NSW", true, "NSW", null);
            TryResolve(_australia, "Cam NSW", true, "NSW", null);
            TryResolve(_australia, "Ca NSW", true, "NSW", null);
            TryResolve(_australia, "C NSW", true, "NSW", null);

            // Differentiated by postcode.

            TryResolve(_australia, "Newtow QLD", true, "QLD", null);

            // 2 possibilities - Belrose, Belrose West

            Resolve(_australia, "Belros NSW", "Belrose", "2085");
        }

        [TestMethod]
        public void TestResolveContainingSuburbState()
        {
            // Unique suburb.

            Resolve(_australia, "orlan VIC", "Norlane", "3214");
            Resolve(_australia, "orla VIC", "Norlane", "3214");

            // 4 possibilities.

            TryResolve(_australia, "orl VIC", true, "VIC", null);

            // 6 possibilities

            TryResolve(_australia, "amberwel VIC", true, "VIC", null);

            // 1 possibility - Camberwell

            Resolve(_australia, "amberwel NSW", "Camberwell", "2330");
            Resolve(_australia, "berw NSW", "Camberwell", "2330");

            // 1 possibility - Newtown

            TryResolve(_australia, "ewtow QLD", true, "QLD", null);

            // 1 possibility - Berowra Waters

            Resolve(_australia, "erowra Water NSW", "Berowra Waters", "2082");
            Resolve(_australia, "owra Wat NSW", "Berowra Waters", "2082");

            // 1 possibility - Berowra Heights

            Resolve(_australia, "erowra Height NSW", "Berowra Heights", "2082");
            Resolve(_australia, "owra Heig NSW", "Berowra Heights", "2082");

            // 4 possibilities.

            TryResolve(_australia, "ra Hei NSW", true, "NSW", null);

            // 3 possibilities - Blackbutt, Blackbutt North, and Blackbutt South - all 4306

            Resolve(_australia, "lackbut QLD", "Blackbutt", "4306");
        }

        [TestMethod]
        public void TestResolveSoundExSuburbState()
        {
            // Unique suburb.

            Resolve(_australia, "Nrlan VIC", "Norlane", "3214");

            // 7 possibilities

            TryResolve(_australia, "Cmberwell VIC", true, "VIC", null);

            // 8 possibilities

            TryResolve(_australia, "Cmberwel NSW", true, "NSW", null);

            // Differentiated by postcode.

            TryResolve(_australia, "Nwtown QLD", true, "QLD", null);

            // 2 possibilities - Caboolture, Caboolture South

            Resolve(_australia, "Cbooltr QLD", "Caboolture", "4510");
        }

        [TestMethod]
        public void TestResolveExactSuburbExactPostcode()
        {
            // Unique suburb.

            Resolve(_australia, "Norlane 3214", "Norlane", "3214");

            // Differentiated by postcode, different states.

            Resolve(_australia, "Camberwell 3124", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 2330", "Camberwell", "2330");

            // Differentiated by postcode, same state.

            Resolve(_australia, "Newtown 4305", "Newtown", "4305");
            Resolve(_australia, "Newtown 4350", "Newtown", "4350");

            // Same postcode.

            Resolve(_australia, "Berowra Waters 2082", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra Heights 2082", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolveExactSuburbPartialPostcode()
        {
            // Unique suburb.

            Resolve(_australia, "Norlane 321", "Norlane", "3214");
            Resolve(_australia, "Norlane 32", "Norlane", "3214");
            Resolve(_australia, "Norlane 3", "Norlane", "3214");

            // Differentiated by postcode, different states.

            Resolve(_australia, "Camberwell 312", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 31", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 3", "Camberwell", "3124");
            Resolve(_australia, "Camberwell 233", "Camberwell", "2330");
            Resolve(_australia, "Camberwell 23", "Camberwell", "2330");
            Resolve(_australia, "Camberwell 2", "Camberwell", "2330");

            // Differentiated by postcode, same state.

            Resolve(_australia, "Newtown 430", "Newtown", "4305");
            Resolve(_australia, "Newtown 435", "Newtown", "4350");
            Resolve(_australia, "Newtown 43", "Newtown", "4305");
            Resolve(_australia, "Newtown 4", "Newtown", "4305");

            // Same postcode.

            Resolve(_australia, "Berowra Waters 208", "Berowra Waters", "2082");
        }

        [TestMethod]
        public void TestResolvePartialSuburbExactPostcode()
        {
            // Unique suburb.

            Resolve(_australia, "Norlan 3214", "Norlane", "3214");
            Resolve(_australia, "Norla 3214", "Norlane", "3214");
            Resolve(_australia, "Norl 3214", "Norlane", "3214");

            // 2 possibilities - other is North Shore

            Resolve(_australia, "Nor 3214", "Norlane", "3214");

            // 4 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West

            Resolve(_australia, "Camberwel 3124", "Camberwell", "3124");
            Resolve(_australia, "Camberwe 3124", "Camberwell", "3124");
            Resolve(_australia, "Camberw 3124", "Camberwell", "3124");
            Resolve(_australia, "Camber 3124", "Camberwell", "3124");
            Resolve(_australia, "Cambe 3124", "Camberwell", "3124");
            Resolve(_australia, "Camb 3124", "Camberwell", "3124");
            Resolve(_australia, "Cam 3124", "Camberwell", "3124");
            Resolve(_australia, "Ca 3124", "Camberwell", "3124");
            Resolve(_australia, "C 3124", "Camberwell", "3124");

            Resolve(_australia, "Camberwell 2330", "Camberwell", "2330");
            Resolve(_australia, "Camberwel 2330", "Camberwell", "2330");
            Resolve(_australia, "Camberwe 2330", "Camberwell", "2330");
            Resolve(_australia, "Camberw 2330", "Camberwell", "2330");
            Resolve(_australia, "Camber 2330", "Camberwell", "2330");
            Resolve(_australia, "Cambe 2330", "Camberwell", "2330");
            Resolve(_australia, "Camb 2330", "Camberwell", "2330");
            Resolve(_australia, "Cam 2330", "Camberwell", "2330");

            // 2 possibilities - other is Carrowbrook

            Resolve(_australia, "Ca 2330", "Camberwell", "2330");
            Resolve(_australia, "C 2330", "Camberwell", "2330");

            // Differentiated by postcode.

            Resolve(_australia, "Newtow 4305", "Newtown", "4305");
            Resolve(_australia, "Newtow 4350", "Newtown", "4350");

            // Differentiated by suburb.

            Resolve(_australia, "Berowra W 2082", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra H 2082", "Berowra Heights", "2082");
            Resolve(_australia, "Berowra 2082", "Berowra Heights", "2082");
            Resolve(_australia, "Berowr 2082", "Berowra Heights", "2082");
            Resolve(_australia, "Berow 2082", "Berowra Heights", "2082");
            Resolve(_australia, "Bero 2082", "Berowra Heights", "2082");
            Resolve(_australia, "Ber 2082", "Berowra Heights", "2082");
            Resolve(_australia, "Be 2082", "Berowra Heights", "2082");
            Resolve(_australia, "B 2082", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolvePartialSuburbPartialPostcode()
        {
            // Unique suburb.

            Resolve(_australia, "Norlan 321", "Norlane", "3214");
            Resolve(_australia, "Norla 321", "Norlane", "3214");
            Resolve(_australia, "Norl 321", "Norlane", "3214");

            // 3 possibilities - Norlane, North Shore 3214 VIC, North Geelong 3215 VIC

            TryResolve(_australia, "Nor 321", true, null, null);

            // 5 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West 3124 VIC, Camberwell East 3126

            TryResolve(_australia, "Camberwel 312", true, null, null);
            TryResolve(_australia, "Camberwe 312", true, null, null);
            TryResolve(_australia, "Camberw 312", true, null, null);
            TryResolve(_australia, "Camber 312", true, null, null);
            TryResolve(_australia, "Cambe 312", true, null, null);
            TryResolve(_australia, "Camb 312", true, null, null);
            TryResolve(_australia, "Cam 312", true, null, null);

            // 6 possibilities - Canterbury 3126 VIC

            TryResolve(_australia, "Ca 312", true, null, null);

            // 7 possibilities - Cremorne 3121 VIC

            TryResolve(_australia, "C 312", true, null, null);

            // 1 possibility

            Resolve(_australia, "Stirlin 291", "Stirling", "2914");
            Resolve(_australia, "Stirli 291", "Stirling", "2914");
            Resolve(_australia, "Stirl 291", "Stirling", "2914");
            Resolve(_australia, "Stir 291", "Stirling", "2914");
            Resolve(_australia, "Sti 291", "Stirling", "2914");

            // 2 possibilities - other is Strathmore

            Resolve(_australia, "St 291", "Stirling", "2914");
            Resolve(_australia, "S 291", "Stirling", "2914");

            // Differentiated by postcode.

            Resolve(_australia, "Newtow 430", "Newtown", "4305");
            Resolve(_australia, "Newtow 435", "Newtown", "4350");
            Resolve(_australia, "Newtow 43", "Newtown", "4305");

            // Differentiated by suburb.

            Resolve(_australia, "Berowra W 208", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra H 208", "Berowra Heights", "2082");

            // 3 possibilities, Berowra 2081 NSW, Berowra Heights 2082 NSW, Berowra Waters 2082 NSW

            TryResolve(_australia, "Berowr 208", true, null, null);
            TryResolve(_australia, "Berow 208", true, null, null);
            TryResolve(_australia, "Bero 208", true, null, null);
            TryResolve(_australia, "Ber 208", true, null, null);

            // 5 possibilities - Belrose, Belrose West

            TryResolve(_australia, "Be 208", true, null, null);

            // 6 possibilities - Brooklyn

            TryResolve(_australia, "B 208", true, null, null);
        }

        [TestMethod]
        public void TestResolveContainingSuburbExactPostcode()
        {
            // Unique suburb.

            Resolve(_australia, "orlan 3214", "Norlane", "3214");
            Resolve(_australia, "orla 3214", "Norlane", "3214");
            Resolve(_australia, "orl 3214", "Norlane", "3214");
            Resolve(_australia, "rl 3214", "Norlane", "3214");

            // 5 possibilities - Camberwell, Camberwell North, Camberwell South, Camberwell West, Middle Camberwell

            Resolve(_australia, "amberwel 3124", "Camberwell", "3124");
            Resolve(_australia, "berw 3124", "Camberwell", "3124");
            Resolve(_australia, "er 3124", "Camberwell", "3124");

            // 1 possibility - Camberwell

            Resolve(_australia, "amberwel 2330", "Camberwell", "2330");
            Resolve(_australia, "berw 2330", "Camberwell", "2330");

            // 1 possibility - Jerrys Plains

            Resolve(_australia, "err 2330", "Jerrys Plains", "2330");

            // 2 possibilities - Camberwell, Jerrys Plains

            Resolve(_australia, "er 2330", "Camberwell", "2330");

            // 1 possibility - Newtown

            Resolve(_australia, "ewtow 4305", "Newtown", "4305");
            Resolve(_australia, "wto 4305", "Newtown", "4305");
            Resolve(_australia, "ewtow 4350", "Newtown", "4350");
            Resolve(_australia, "wto 4350", "Newtown", "4350");

            // 1 possibility - Berowra Waters

            Resolve(_australia, "erowra Water 2082", "Berowra Waters", "2082");
            Resolve(_australia, "owra Wat 2082", "Berowra Waters", "2082");
            Resolve(_australia, "ra W 2082", "Berowra Waters", "2082");

            // 1 possibility - Berowra Heights

            Resolve(_australia, "erowra Height 2082", "Berowra Heights", "2082");
            Resolve(_australia, "owra Heig 2082", "Berowra Heights", "2082");
            Resolve(_australia, "ra Hei 2082", "Berowra Heights", "2082");

            // 2 possibilities - Berowra Waters, Berowra Heights

            Resolve(_australia, "ra 2082", "Berowra Heights", "2082");

            // 2 possibilities in different subdivisions.

            TryResolve(_australia, "ar 2620", true, null, "2620");
        }

        [TestMethod]
        public void TestResolveContainingSuburbPartialPostcode()
        {
            // Unique suburb.

            Resolve(_australia, "orlan 321", "Norlane", "3214");
            Resolve(_australia, "rl 321", "Norlane", "3214");

            // 4 possibilities - Corio, Norlane, North Shore 3214 VIC, North Geelong 3215 VIC

            TryResolve(_australia, "or 321", true, null, null);

            // 6 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West, Middle Camberwell 3124 VIC, Camberwell East 3126

            TryResolve(_australia, "amberwel 312", true, null, null);
            TryResolve(_australia, "mberw 312", true, null, null);

            // 1 possibility

            Resolve(_australia, "amberwel 233", "Camberwell", "2330");

            // 2 possibilities - Aberdeen 2336

            TryResolve(_australia, "ber 233", true, null, null);

            // Differentiated by postcode.

            Resolve(_australia, "ewtow 430", "Newtown", "4305");
            Resolve(_australia, "ewtow 435", "Newtown", "4350");
            Resolve(_australia, "ewtow 43", "Newtown", "4305");

            // 3 possibilities, Berowra 2081 NSW, Berowra Heights 2082 NSW, Berowra Waters 2082 NSW

            TryResolve(_australia, "erowr 208", true, null, null);
            TryResolve(_australia, "row 208", true, null, null);

            // 5 possibilities - Belrose, Belrose West

            TryResolve(_australia, "ro 208", true, null, null);

            // 2 possibilities - Belrose, Belrose West

            Resolve(_australia, "elros 208", "Belrose", "2085");
        }

        [TestMethod]
        public void TestResolveSoundExSuburbExactPostcode()
        {
            // 1 possibility

            Resolve(_australia, "Nrlan 3214", "Norlane", "3214");

            // 4 possibilities - Camberwell, Camberwell North, Camberwell South, Camberwell West

            Resolve(_australia, "Cmberwel 3124", "Camberwell", "3124");

            // 1 possibility

            Resolve(_australia, "Cmberwel 2330", "Camberwell", "2330");

            // 1 possibility

            Resolve(_australia, "Nwtown 4305", "Newtown", "4305");
            Resolve(_australia, "Nwtown 4350", "Newtown", "4350");

            // 2 possibilities - Berowra Heights, Berowra Waters

            Resolve(_australia, "Browra 2082", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolveSoundExSuburbPartialPostcode()
        {
            // Unique suburb.

            Resolve(_australia, "Nrlan 321", "Norlane", "3214");

            // 5 possibilities, Camberwell, Camberwell North, Camberwell South, Camberwell West 3124 VIC, Camberwell East 3126

            TryResolve(_australia, "Cmberwell 312", true, null, null);

            // 1 possibility

            Resolve(_australia, "Cmberwel 233", "Camberwell", "2330");

            // Differentiated by postcode.

            Resolve(_australia, "Nwtown 430", "Newtown", "4305");
            Resolve(_australia, "Nwtown 435", "Newtown", "4350");
            Resolve(_australia, "Nwtown 43", "Newtown", "4305");

            // 2 possibilities - Belrose, Belrose West

            Resolve(_australia, "Blros 208", "Belrose", "2085");
        }

        [TestMethod]
        public void TestResolveExactSuburb()
        {
            // 1 possibility.

            Resolve(_australia, "Norlane", "Norlane", "3214");

            // 2 possibilities - 3214 VIC, 2330 NSW.

            TryResolve(_australia, "Camberwell", true, null, null);

            // 4 possibilities.

            TryResolve(_australia, "Newtown", true, null, null);

            // 1 possibility.

            Resolve(_australia, "Berowra Waters", "Berowra Waters", "2082");
            Resolve(_australia, "Berowra Heights", "Berowra Heights", "2082");
        }

        [TestMethod]
        public void TestResolvePartialSuburb()
        {
            // Unique suburb.

            Resolve(_australia, "Norlan", "Norlane", "3214");
            Resolve(_australia, "Norla", "Norlane", "3214");

            // Multiple possibilities

            TryResolve(_australia, "Norl", true, null, null);
            TryResolve(_australia, "Nor", true, null, null);

            // Multiple possibilities

            TryResolve(_australia, "Camberwel", true, null, null);
            TryResolve(_australia, "Camberwe", true, null, null);
            TryResolve(_australia, "Camberw", true, null, null);
            TryResolve(_australia, "Camber", true, null, null);
            TryResolve(_australia, "Cambe", true, null, null);
            TryResolve(_australia, "Camb", true, null, null);
            TryResolve(_australia, "Cam", true, null, null);
            TryResolve(_australia, "Ca", true, null, null);
            TryResolve(_australia, "C", true, null, null);

            // Multiple possibilities

            TryResolve(_australia, "Newtow", true, null, null);

            // 2 possibilities

            Resolve(_australia, "Belros", "Belrose", "2085");
        }

        [TestMethod]
        public void TestResolveTrimPostalSuburb()
        {
            Resolve(_australia, "Norlane VIC 3214", "Norlane", "3214");
            Resolve(_australia, "  Norlane VIC 3214", "Norlane", "3214");
            Resolve(_australia, "Norlane VIC 3214  ", "Norlane", "3214");
            Resolve(_australia, " Norlane VIC 3214 ", "Norlane", "3214");
        }

        [TestMethod]
        public void TestResolveContainingSuburb()
        {
            // Unique suburb.

            Resolve(_australia, "orlane", "Norlane", "3214");
            TryResolve(_australia, "orlan", true, null, null);

            // Multiple possibilities

            TryResolve(_australia, "amberwel", true, null, null);

            // Multiple possibility - Newtown

            TryResolve(_australia, "ewtow", true, null, null);
        }

        [TestMethod]
        public void TestResolveMultiplePostalCodes()
        {
            // North Sydney has two postcodes, 2059 and 2060.  Shouldr esolve to the first.

            Resolve(_australia, "North Sydney", "North Sydney", "2059");
        }

        [TestMethod]
        public void TestResolveExactPostcode()
        {
            // 3 possibilities.

            Resolve(_australia, "3214", "Corio", "3214");

            // 2 possibilities: 2620 ACT, 2620 NSW

            TryResolve(_australia, "2620", true, null, "2620");

            // This postcode has not suburbs.

            TryResolve(_australia, "6830", false, null, "6830");
        }

        [TestMethod]
        public void TestResolveState()
        {
            // No possibilities.

            TryResolve(_australia, "VIC", false, "VIC", null);
        }

        [TestMethod]
        public void TestResolveTrimState()
        {
            TryResolve(_australia, "VIC", false, "VIC", null);
            TryResolve(_australia, "  VIC", false, "VIC", null);
            TryResolve(_australia, "VIC  ", false, "VIC", null);
            TryResolve(_australia, " VIC ", false, "VIC", null);
        }

        [TestMethod]
        public void TestUnresolved()
        {
            // Null.

            TryResolve(_australia, null, false, null, null);

            // Empty string

            TryResolve(_australia, "", false, null, null);

            // Bad suburb

            TryResolve(_australia, "xyz", false, null, null);
            TryResolve(_australia, "xyz 3124", false, null, "3124");
            TryResolve(_australia, "xyz VIC", false, "VIC", null);
            TryResolve(_australia, "xyz 3124 VIC", false, "VIC", "3124");

            // Bad postcode

            TryResolve(_australia, "123456", false, null, null);
            TryResolve(_australia, "Norlane 123456", false, null, null);
            TryResolve(_australia, "123456 VIC", false, "VIC", null);
            TryResolve(_australia, "Norlane 123456 VIC", false, "VIC", null);

            // Partial postcode

            TryResolve(_australia, "xyz 312", false, null, null);
            TryResolve(_australia, "xyz 312 VIC", false, "VIC", null);

            // Postcode extremes

            TryResolve(_australia, "Armadale 0000", false, null, null);
            TryResolve(_australia, "Armadale 9999", false, null, null);
        }

        [TestMethod]
        public void TestMaximumSuggestions()
        {
            // This has 2 suggestions.

            TryResolve(_australia, "Newtown 43 QLD", true, -1, "QLD", null);
            TryResolve(_australia, "Newtown 43 QLD", true, 1, "QLD", null);

            // This has 6 suggestions.

            TryResolve(_australia, "Camberwel 312 VIC", true, -1, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 1, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 2, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 3, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 4, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 5, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 6, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 7, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 8, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 9, "VIC", null);
            TryResolve(_australia, "Camberwel 312 VIC", true, 10, "VIC", null);
        }

        [TestMethod]
        public void TestExtraCharacters()
        {
            // White space.

            TryResolve(_australia, " ", false, "", null, null);
            TryResolve(_australia, "  ", false, "", null, null);
            TryResolve(_australia, "\n", false, "", null, null);

            // Extra whitespace.

            Resolve(_australia, "Norlane   3214   VIC", "Norlane", "3214");
            Resolve(_australia, "Norlane  \n3214 \r  VIC", "Norlane", "3214");

            // Bad characters.

            TryResolve(_australia, "@", false, null, null);
            TryResolve(_australia, "@#&", false, null, null);
            TryResolve(_australia, "@\n", false, "@", null, null);
            TryResolve(_australia, "\r@#& ", false, "@#&", null, null);

            // Resolve with extra characters.

            Resolve(_australia, "Norlane @ 3214 VIC", "Norlane", "3214");
            Resolve(_australia, "Norlane@ 3214 VIC", "Norlane", "3214");
            TryResolve(_australia, "Nor @ 321 VIC", true, "VIC", null);

            // Shouldn't resolve.

            TryResolve(_australia, "Nor@lane 3214 VIC", false, "VIC", "3214");

            // Should resolve.

            Resolve(_australia, "North/Geelong VIC", "North Geelong", "3215");
        }

        [TestMethod]
        public void TestResolveCountry()
        {
            TryResolve(_australia, "Australia", false, null, null);
        }

        private void Resolve(Country country, string location, string expectedSuburb, string expectedPostcode)
        {
            var expectedPostalCode = _locationQuery.GetPostalCode(country, expectedPostcode);
            var expectedPostalSuburb = _locationQuery.GetPostalSuburb(expectedPostalCode, expectedSuburb);

            // Without suggestions.

            var locationReference = _locationQuery.ResolvePostalSuburb(country, location);
            AssertResolvedPostalSuburb(locationReference, expectedPostalSuburb);

            // With suggestions.

            IList<NamedLocation> suggestions = new List<NamedLocation>();
            locationReference = _locationQuery.ResolvePostalSuburb(country, location, suggestions, -1);
            AssertResolvedPostalSuburb(locationReference, expectedPostalSuburb);
            Assert.AreEqual(0, suggestions.Count);
        }

        private void TryResolve(Country country, string location, bool expectingSuggestions, string expectedSubdivision, string expectedPostcode)
        {
            TryResolve(country, location, expectingSuggestions, -1, location, expectedSubdivision, expectedPostcode);
        }

        private void TryResolve(Country country, string location, bool expectingSuggestions, string expectedUnstructuredLocation, string expectedSubdivision, string expectedPostcode)
        {
            TryResolve(country, location, expectingSuggestions, -1, expectedUnstructuredLocation, expectedSubdivision, expectedPostcode);
        }

        private void TryResolve(Country country, string location, bool expectingSuggestions, int maximumSuggestions, string subdivision, string postcode)
        {
            TryResolve(country, location, expectingSuggestions, maximumSuggestions, location, subdivision, postcode);
        }

        private void TryResolve(Country country, string location, bool expectingSuggestions, int maximumSuggestions, string unstructuredLocation, string subdivision, string postcode)
        {
            var expectedUnstructuredLocation = string.IsNullOrEmpty(unstructuredLocation) ? null : unstructuredLocation.Trim();
            var expectedSubdivision = _locationQuery.GetCountrySubdivision(country, subdivision);
            var expectedPostalCode = postcode == null ? null : _locationQuery.GetPostalCode(country, postcode);

            // Without suggestions.

            var locationReference = _locationQuery.ResolvePostalSuburb(country, location);
            AssertNotResolvedLocation(locationReference, expectedUnstructuredLocation, expectedSubdivision, expectedPostalCode);

            // With suggestions.

            IList<NamedLocation> suggestions = new List<NamedLocation>();
            locationReference = _locationQuery.ResolvePostalSuburb(country, location, suggestions, maximumSuggestions);
            AssertNotResolvedLocation(locationReference, expectedUnstructuredLocation, expectedSubdivision, expectedPostalCode);

            // Assert suggestions.

            Assert.AreEqual(expectingSuggestions, suggestions.Count != 0);
            if (maximumSuggestions > 0)
                Assert.IsTrue(suggestions.Count <= maximumSuggestions);
        }

        private static void AssertResolvedPostalSuburb(LocationReference locationReference, PostalSuburb expectedPostalSuburb)
        {
            // The unstructured location is not set.

            Assert.IsNull(locationReference.UnstructuredLocation);

            // The named location is the postal suburb.

            Assert.AreEqual(expectedPostalSuburb, locationReference.NamedLocation);

            // The postal code and its locality are determined by the postal suburb.

            Assert.AreEqual(expectedPostalSuburb, locationReference.PostalSuburb);
            Assert.AreEqual(expectedPostalSuburb.PostalCode, locationReference.PostalCode);
            Assert.AreEqual(expectedPostalSuburb.PostalCode.Locality, locationReference.Locality);

            // The region is not set.

            Assert.IsNull(locationReference.Region);

            // The subdivision and country are determined by the postal suburb.

            Assert.AreEqual(expectedPostalSuburb.CountrySubdivision, locationReference.CountrySubdivision);
            Assert.AreEqual(expectedPostalSuburb.CountrySubdivision.Country, locationReference.Country);
        }

        private static void AssertNotResolvedLocation(LocationReference locationReference, string expectedUnstructuredLocation, CountrySubdivision expectedSubdivision, PostalCode expectedPostalCode)
        {
            // If the unstructured location is not set then it is a country.

            if (expectedUnstructuredLocation == null)
            {
                Assert.IsNull(locationReference.UnstructuredLocation);

                Assert.AreEqual(expectedSubdivision, locationReference.CountrySubdivision);
                Assert.IsTrue(locationReference.CountrySubdivision.IsCountry);
                Assert.AreEqual(expectedSubdivision.Country, locationReference.Country);
                Assert.AreEqual(expectedSubdivision, locationReference.NamedLocation);

                // Everything else is not set.

                Assert.IsNull(locationReference.PostalSuburb);
                Assert.IsNull(locationReference.PostalCode);
                Assert.IsNull(locationReference.Locality);
                Assert.IsNull(locationReference.Region);
            }
            else
            {
                // The unstructured location is set.

                Assert.IsNotNull(locationReference.UnstructuredLocation);
                Assert.AreEqual(expectedUnstructuredLocation, locationReference.UnstructuredLocation);

                // The postal suburb is not set.

                Assert.IsNull(locationReference.PostalSuburb);

                // If the postal code was resolved then the named location and locality come from it.

                Assert.AreEqual(expectedPostalCode, locationReference.PostalCode);
                if (expectedPostalCode != null)
                {
                    Assert.AreEqual(expectedPostalCode, locationReference.NamedLocation);
                    Assert.AreEqual(expectedPostalCode.Locality, locationReference.Locality);
                }

                // The region is not set.

                Assert.IsNull(locationReference.Region);

                // Check whether the subdivision has been resolved.  If the postal code was not resolved then the named location is the subdivision.

                Assert.AreEqual(expectedSubdivision, locationReference.CountrySubdivision);
                Assert.AreEqual(expectedSubdivision.Country, locationReference.Country);

                if (expectedPostalCode == null)
                    Assert.AreEqual(expectedSubdivision, locationReference.NamedLocation);
            }
        }
    }
}
