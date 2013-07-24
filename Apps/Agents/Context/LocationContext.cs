using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Agents.Context
{
    public interface ILocationContext
        : ISubActivityContext
    {
        void Reset();
        Country Country { get; set; }
    }

    internal class DefaultLocationContext
        : ILocationContext
    {
        private Country _country;

        public void Reset()
        {
            _country = Container.Current.Resolve<ILocationQuery>().GetCountry("Australia");
        }

        public Country Country
        {
            get { return _country ?? Container.Current.Resolve<ILocationQuery>().GetCountry("Australia"); }
            set { _country = value; }
        }
    }
}