namespace LinkMe.Domain.Location
{
    public interface IHaveAddress
    {
        Address Address { get; set; }
    }

    public interface IHaveLocation
    {
        LocationReference Location { get; set; }
    }
}
