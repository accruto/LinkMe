namespace LinkMe.Domain.Location
{
    public class LocationAbbreviation
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public bool IsPrefix { get; set; }
        public bool IsSuffix { get; set; }
    }
}
