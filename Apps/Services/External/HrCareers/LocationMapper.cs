using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Services.External.HrCareers.Schema;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Apps.Services.External.HrCareers
{
    public class LocationMapper
    {
        private readonly WorldIndex _world = new WorldIndex();

        // map point sets to HRcareer JobRegion IDs
        private readonly List<KeyValuePair<HashSet<int>, string>> _mappings = new List<KeyValuePair<HashSet<int>, string>>();

        public LocationMapper(ILocationQuery locationQuery)
        {
            _world.BuildUp(locationQuery, false);
            var australia = locationQuery.GetCountry("Australia");

            Add(locationQuery, australia, "Canberra", "ACT", "20-162-3", "20-162-4");
            Add(locationQuery, australia, "Sydney", "NSW", "20-163-5", "20-163-6");
            Add(locationQuery, australia, "Darwin", "NT", "20-164-7", "20-164-8");
            Add(locationQuery, australia, "Brisbane", "QLD", "20-165-9", "20-165-10");
            Add(locationQuery, australia, "Adelaide", "SA", "20-166-11", "20-166-12");
            Add(locationQuery, australia, "Hobart", "TAS", "20-167-13", "20-167-14");
            Add(locationQuery, australia, "Melbourne", "VIC", "20-168-1", "20-168-2");
            Add(locationQuery, australia, "Perth", "WA", "20-169-15", "20-169-16");
        }

        public JobRegion Map(LocationReference location)
        {
            var l = _world.GetPointSet(location);
            var id = _mappings.First(x => l.IsSubsetOf(x.Key)).Value;
            var name = location.ToString();
            return new JobRegion { id = id, name = name };
        }

        private void Add(ILocationQuery locationQuery, Country australia,
            string capitalName, string stateName, string capitalId, string regionalId)
        {
            var capital = _world.GetPointSet(locationQuery.GetRegion(australia, capitalName));
            _mappings.Add(new KeyValuePair<HashSet<int>, string>(capital, capitalId));

            var state = _world.GetPointSet(locationQuery.GetCountrySubdivision(australia, stateName));
            _mappings.Add(new KeyValuePair<HashSet<int>, string>(
                new HashSet<int>(state.Except(capital)), regionalId));
        }
    }
}
