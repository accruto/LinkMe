using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    [TestClass]
    public abstract class EmployersTests
        : WebTestClass
    {
        private ReadOnlyUrl _employersUrl;
        private ReadOnlyUrl _searchEmployersUrl;
        private ReadOnlyUrl _organisationsUrl;

        [TestInitialize]
        public void EmployersTestsInitialize()
        {
            _employersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/employers");
            _searchEmployersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/employers/search");
            _organisationsUrl = new ReadOnlyApplicationUrl(true, "~/administrators/organisations");
        }

        protected ReadOnlyUrl GetEmployerUrl(IEmployer employer)
        {
            return new ReadOnlyApplicationUrl((_employersUrl.AbsoluteUri + "/").AddUrlSegments(employer.Id.ToString().ToLower()));
        }

        protected ReadOnlyUrl GetSearchEmployersUrl()
        {
            return _searchEmployersUrl;
        }

        protected ReadOnlyUrl GetCreditsUrl(IEmployer employer)
        {
            var url = (_employersUrl.AbsoluteUri + "/")
                .AddUrlSegments(employer.Id + "/credits");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCreditsUrl(Organisation organisation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/credits");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCreditsUsageUrl(IEmployer employer)
        {
            var url = (_employersUrl.AbsoluteUri + "/")
                .AddUrlSegments(employer.Id + "/credits/usage");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCreditsUsageUrl(Organisation organisation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/credits/usage");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCreditsUsageUrl(IEmployer employer, Allocation allocation)
        {
            var url = (_employersUrl.AbsoluteUri + "/")
                .AddUrlSegments(employer.Id + "/allocations/" + allocation.Id + "/usage");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCreditsUsageUrl(Organisation organisation, Allocation allocation)
        {
            var url = (_organisationsUrl.AbsoluteUri + "/")
                .AddUrlSegments(organisation.Id + "/allocations/" + allocation.Id + "/usage");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetDeallocateUrl(IEmployer employer, Allocation allocation)
        {
            var url = (_employersUrl.AbsoluteUri + "/").AddUrlSegments(employer.Id + "/allocations/deallocate");
            return new ReadOnlyApplicationUrl(url, new ReadOnlyQueryString("allocationId", allocation.Id.ToString()));
        }
    }
}
