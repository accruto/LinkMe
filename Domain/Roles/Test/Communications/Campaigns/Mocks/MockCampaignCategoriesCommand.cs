using System.Collections.Generic;
using LinkMe.Domain.Criterias;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Mocks
{
    public class MockEmployerCriteria
        : Criteria
    {
        private static readonly IDictionary<string, CriteriaDescription> _descriptions = new Dictionary<string, CriteriaDescription>();

        public MockEmployerCriteria()
            : base(_descriptions)
        {
        }
    }

    public class MockMemberCriteria
        : Criteria
    {
        private static readonly IDictionary<string, CriteriaDescription> _descriptions = new Dictionary<string, CriteriaDescription>();

        public MockMemberCriteria()
            : base(_descriptions)
        {
        }
    }
}