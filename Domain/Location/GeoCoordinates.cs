using System;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public struct GeoCoordinates
    {
        private readonly float _latitude;
        private readonly float _longitude;

        public GeoCoordinates(float latitude, float longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public float Latitude
        {
            get { return _latitude; }
        }

        public float Longitude
        {
            get { return _longitude; }
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Latitude, Longitude);
        }

        public static double Distance(GeoCoordinates c1, GeoCoordinates c2)
        {
            const double avgEarthRadius = 6372.795;

            // Convert to radians.

            var lat1 = c1.Latitude * Math.PI / 180;
            var lat2 = c2.Latitude * Math.PI / 180;
            var dLng = (c2.Longitude - c1.Longitude) * Math.PI / 180;

            var sinLat = Math.Sin((lat1 - lat2) / 2);
            var sinLng = Math.Sin(dLng / 2);
            var cosLat1 = Math.Cos(lat1);
            var cosLat2 = Math.Cos(lat2);

            // Use the haversine formula (see http://en.wikipedia.org/wiki/Great-circle_distance).

            return avgEarthRadius * 2 * Math.Asin(Math.Sqrt(sinLat * sinLat + cosLat1 * cosLat2 * sinLng * sinLng));
        }

        public double Distance(GeoCoordinates c)
        {
            return Distance(this, c);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GeoCoordinates))
                return false;
            var other = (GeoCoordinates)obj;

            // Round to 4 decimal places when matching.

            float thisLatitude = (float)Math.Round(_latitude * 10000) / 10000;
            float otherLatitude = (float)Math.Round(other._latitude * 10000) / 10000;
            if (thisLatitude != otherLatitude)
                return false;

            float thisLongitude = (float)Math.Round(_longitude * 10000) / 10000;
            float otherLongitude = (float)Math.Round(other._longitude * 10000) / 10000;
            if (thisLongitude != otherLongitude)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            float thisLatitude = (float)Math.Round(_latitude * 10000) / 10000;
            float thisLongitude = (float)Math.Round(_longitude * 10000) / 10000;
            return thisLatitude.GetHashCode() ^ thisLongitude.GetHashCode();
        }
    }
}