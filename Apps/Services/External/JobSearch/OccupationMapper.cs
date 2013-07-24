using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Services.External.JobSearch
{
    public class OccupationMapper
    {
        private const int DefaultCode = 8999;
        private readonly IDictionary<Industry, int> _industryMap;

        public OccupationMapper(IIndustriesQuery industriesQuery)
        {
            var industryMapTemplate = new[]
            {
                Tuple.Create("accounting", 2211), //Accountants
                Tuple.Create("administration", 5619), //Other Clerical and Office Support Worker
                Tuple.Create("advertising-media-entertainment", 2251), //Advertising and Marketing Professionals
                Tuple.Create("automotive", 3212), //Motor Mechanics
                Tuple.Create("banking-financial-services", 5521), // Bank Workers
                Tuple.Create("call-centre-customer-service", 6393), // Telemarketers
                Tuple.Create("community-sport", 4518), // Other Personal Service Workers
                Tuple.Create("construction", 1331), // Construction Managers
                Tuple.Create("consulting-corporate-strategy", 2247), // Management and Organisation Analysts
                Tuple.Create("education-training", 1344), // Other Education Managers
                Tuple.Create("engineering", 1332), // Engineering Managers
                Tuple.Create("government-defence", 4422), // Security Officers and Guards
                Tuple.Create("healthcare-medical-pharmaceutical", 2513), // Generalist Medical Practitioners
                Tuple.Create("hospitality-tourism", 4516), // Tourism and Travel Advisers
                Tuple.Create("hr-recruitment", 1323), // Human Resource Managers
                Tuple.Create("insurance-superannuation", 6112), // Insurance Agents
                Tuple.Create("it-telecommunications", 3132), // Telecommunications Technical Specialists
                Tuple.Create("legal", 2711), // Barristers
                Tuple.Create("manufacturing-operations", 8999), // Other Miscellaneous Labourers
                Tuple.Create("mining-oil-gas", 2336), // Mining Engineers
                Tuple.Create("primary-industry", 8419), // Other Farm, Forestry and Garden Workers
                Tuple.Create("real-estate-property", 6121), // Real Estate Sales Agents
                Tuple.Create("retail-consumer-products", 6219), // Other Sales Assistants and Salespersons
                Tuple.Create("sales-marketing", 1311), // Advertising and Sales Managers
                Tuple.Create("science-technology", 3114), // Science Technicians
                Tuple.Create("trades-services", 3411), // Electricians
                Tuple.Create("transport-logistics", 7331) // Truck Drivers
            };

            _industryMap =
                industryMapTemplate.ToDictionary(t => industriesQuery.GetIndustryByUrlName(t.Item1), t => t.Item2);
        }

        public int Map(JobAd jobAd)
        {
            var jobAdIndustries = jobAd.Description.Industries;
            if (jobAdIndustries == null || jobAdIndustries.Count == 0)
                return DefaultCode;

            int code;
            return _industryMap.TryGetValue(jobAdIndustries[0], out code) ? code : DefaultCode;
        }
    }
}
