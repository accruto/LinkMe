using System;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool CanResolveLocations { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Country;
            if (other == null)
                return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
