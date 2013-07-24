using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public abstract class OrganisationsTests
        : WebTestClass
    {
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IOrganisationsQuery _organisationsQuery = Resolve<IOrganisationsQuery>();
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();

        private ReadOnlyUrl _organisationsUrl;
        private ReadOnlyUrl _employersUrl;
        private ReadOnlyUrl _searchOrganisationsUrl;
        private ReadOnlyUrl _newOrganisationUrl;

        [TestInitialize]
        public void OrganisationsTestsInitialize()
        {
            _organisationsUrl = new ReadOnlyApplicationUrl(true, "~/administrators/organisations");
            _employersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/employers");
            _searchOrganisationsUrl = new ReadOnlyApplicationUrl(true, "~/administrators/organisations/search");
            _newOrganisationUrl = new ReadOnlyApplicationUrl(true, "~/administrators/organisations/organisation/new");
        }

        protected ReadOnlyUrl GetOrganisationUrl(IOrganisation organisation)
        {
            return new ReadOnlyApplicationUrl((_organisationsUrl.AbsoluteUri + "/").AddUrlSegments(organisation.Id.ToString().ToLower()));
        }

        protected ReadOnlyUrl GetSearchOrganisationsUrl()
        {
            return _searchOrganisationsUrl;
        }

        protected ReadOnlyUrl GetNewOrganisationUrl()
        {
            return _newOrganisationUrl;
        }

        protected ReadOnlyUrl GetReportUrl(IOrganisation organisation, string type)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/reports/" + type);
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCommunicationsUrl(IOrganisation organisation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/communications");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetEmployersUrl(IOrganisation organisation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/employers");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetNewEmployerUrl(IOrganisation organisation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/employers/new");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCreditsUrl(IOrganisation organisation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/credits");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetDeallocateUrl(IOrganisation organisation, Allocation allocation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/").AddUrlSegments(organisation.Id + "/allocations/deallocate");
            return new ReadOnlyApplicationUrl(url, new ReadOnlyQueryString("allocationId", allocation.Id.ToString()));
        }

        protected ReadOnlyUrl GetEmployerUrl(IEmployer employer)
        {
            var url = (_employersUrl.AbsoluteUri + "/")
                .AddUrlSegments(employer.Id.ToString());
            return new ReadOnlyApplicationUrl(url.ToLower());
        }
    }
}
