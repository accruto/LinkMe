namespace LinkMe.Domain.Location.Queries
{
    public class LocationResetQuery
        : ILocationResetQuery
    {
        private readonly ILocationRepository _repository;

        public LocationResetQuery(ILocationRepository repository)
        {
            _repository = repository;
        }

        void ILocationResetQuery.Reset()
        {
            World.Reset(_repository);
        }
    }
}