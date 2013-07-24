namespace LinkMe.Domain.Location
{
    public class RelativeLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrefix { get; set; }
        public bool IsSuffix { get; set; }
    }
}
