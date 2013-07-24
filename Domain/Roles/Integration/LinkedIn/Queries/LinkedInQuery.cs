using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Domain.Roles.Integration.LinkedIn.Queries
{
    public class LinkedInQuery
        : ILinkedInQuery
    {
        private readonly ILinkedInRepository _repository;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ILocationQuery _locationQuery;

        private readonly IDictionary<string, string> _groups = new Dictionary<string, string>
        {
            { "agr", "Primary Industry & Agriculture" },
            { "art", "Advertising, Media & Entertainment" },
            { "cons", "Construction" },
            { "corp", "Consulting & Corporate Strategy" },
            { "edu", "Education & Training" },
            { "fin", "Banking & Financial Services" },
            { "good", "Retail & Consumer Products" },
            { "gov", "Government & Defence" },
            { "hlth", "Healthcare, Medical & Pharmaceutical" },
            { "leg", "Legal" },
            { "man", "Manufacturing & Operations" },
            { "med", "Advertising, Media & Entertainment" },
            { "org", "Consulting & Corporate Strategy" },
            { "rec", "Community & Sport" },
            { "serv", "Trades & Services" },
            { "tech", "IT & Telecommunications" },
            { "tran", "Transport & Logistics" },
        };

        private readonly IDictionary<string, IList<string>> _industryGroups = new Dictionary<string, IList<string>>
        {
            { "Accounting", new[] { "corp", "fin" } },
            { "Airlines/Aviation", new[] { "man", "tech", "tran" } },
            { "Alternative Dispute Resolution", new[] { "leg", "org" } },
            { "Alternative Medicine", new[] { "hlth" } },
            { "Animation", new[] { "art", "med" } },
            { "Apparel & Fashion", new[] { "good" } },
            { "Architecture & Planning", new[] { "cons" } },
            { "Arts and Crafts", new[] { "art", "med", "rec" } },
            { "Automotive", new[] { "man" } },
            { "Aviation & Aerospace", new[] { "gov", "man" } },
            { "Banking", new[] { "fin" } },
            { "Biotechnology", new[] { "gov", "hlth", "tech" } },
            { "Broadcast Media", new[] { "med", "rec" } },
            { "Building Materials", new[] { "cons" } },
            { "Business Supplies and Equipment", new[] { "corp", "man" } },
            { "Capital Markets", new[] { "fin" } },
            { "Chemicals", new[] { "man" } },
            { "Civic & Social Organization", new[] { "org", "serv" } },
            { "Civil Engineering", new[] { "cons", "gov" } },
            { "Commercial Real Estate", new[] { "cons", "corp", "fin" } },
            { "Computer & Network Security", new[] { "tech" } },
            { "Computer Games", new[] { "med", "rec" } },
            { "Computer Hardware", new[] { "tech" } },
            { "Computer Networking", new[] { "tech" } },
            { "Computer Software", new[] { "tech" } },
            { "Construction", new[] { "cons" } },
            { "Consumer Electronics", new[] { "good", "man" } },
            { "Consumer Goods", new[] { "good", "man" } },
            { "Consumer Services", new[] { "org", "serv" } },
            { "Cosmetics", new[] { "good" } },
            { "Dairy", new[] { "agr" } },
            { "Defense & Space", new[] { "gov", "tech" } },
            { "Design", new[] { "art", "med" } },
            { "Education Management", new[] { "edu" } },
            { "E-Learning", new[] { "edu", "org" } },
            { "Electrical/Electronic Manufacturing", new[] { "good", "man" } },
            { "Entertainment", new[] { "med", "rec" } },
            { "Environmental Services", new[] { "org", "serv" } },
            { "Events Services", new[] { "corp", "rec", "serv" } },
            { "Executive Office", new[] { "gov" } },
            { "Facilities Services", new[] { "corp", "serv" } },
            { "Farming", new[] { "agr" } },
            { "Financial Services", new[] { "fin" } },
            { "Fine Art", new[] { "art", "med", "rec" } },
            { "Fishery", new[] { "agr" } },
            { "Food & Beverages", new[] { "rec", "serv" } },
            { "Food Production", new[] { "good", "man", "serv" } },
            { "Fund-Raising", new[] { "org" } },
            { "Furniture", new[] { "good", "man" } },
            { "Gambling & Casinos", new[] { "rec" } },
            { "Glass, Ceramics & Concrete", new[] { "cons", "man" } },
            { "Government Administration", new[] { "gov" } },
            { "Government Relations", new[] { "gov" } },
            { "Graphic Design", new[] { "art", "med" } },
            { "Health, Wellness and Fitness", new[] { "hlth", "rec" } },
            { "Higher Education", new[] { "edu" } },
            { "Hospital & Health Care", new[] { "hlth" } },
            { "Hospitality", new[] { "rec", "serv", "tran" } },
            { "Human Resources", new[] { "corp" } },
            { "Import and Export", new[] { "corp", "good", "tran" } },
            { "Individual & Family Services", new[] { "org", "serv" } },
            { "Industrial Automation", new[] { "cons", "man" } },
            { "Information Services", new[] { "med", "serv" } },
            { "Information Technology and Services", new[] { "tech" } },
            { "Insurance", new[] { "fin" } },
            { "International Affairs", new[] { "gov" } },
            { "International Trade and Development", new[] { "gov", "org", "tran" } },
            { "Internet", new[] { "tech" } },
            { "Investment Banking", new[] { "fin" } },
            { "Investment Management", new[] { "fin" } },
            { "Judiciary", new[] { "gov", "leg" } },
            { "Law Enforcement", new[] { "gov", "leg" } },
            { "Law Practice", new[] { "leg" } },
            { "Legal Services", new[] { "leg" } },
            { "Legislative Office", new[] { "gov", "leg" } },
            { "Leisure, Travel & Tourism", new[] { "rec", "serv", "tran" } },
            { "Libraries", new[] { "med", "rec", "serv" } },
            { "Logistics and Supply Chain", new[] { "corp", "tran" } },
            { "Luxury Goods & Jewelry", new[] { "good" } },
            { "Machinery", new[] { "man" } },
            { "Management Consulting", new[] { "corp" } },
            { "Maritime", new[] { "tran" } },
            { "Market Research", new[] { "corp" } },
            { "Marketing and Advertising", new[] { "corp", "med" } },
            { "Mechanical or Industrial Engineering", new[] { "cons", "gov", "man" } },
            { "Media Production", new[] { "med", "rec" } },
            { "Medical Devices", new[] { "hlth" } },
            { "Medical Practice", new[] { "hlth" } },
            { "Mental Health Care", new[] { "hlth" } },
            { "Military", new[] { "gov" } },
            { "Mining & Metals", new[] { "man" } },
            { "Motion Pictures and Film", new[] { "art", "med", "rec" } },
            { "Museums and Institutions", new[] { "art", "med", "rec" } },
            { "Music", new[] { "art", "rec" } },
            { "Nanotechnology", new[] { "gov", "man", "tech" } },
            { "Newspapers", new[] { "med", "rec" } },
            { "Non-Profit Organization Management", new[] { "org" } },
            { "Oil & Energy", new[] { "man" } },
            { "Online Media", new[] { "med" } },
            { "Outsourcing/Offshoring", new[] { "corp" } },
            { "Package/Freight Delivery", new[] { "serv", "tran" } },
            { "Packaging and Containers", new[] { "good", "man" } },
            { "Paper & Forest Products", new[] { "man" } },
            { "Performing Arts", new[] { "art", "med", "rec" } },
            { "Pharmaceuticals", new[] { "hlth", "tech" } },
            { "Philanthropy", new[] { "org" } },
            { "Photography", new[] { "art", "med", "rec" } },
            { "Plastics", new[] { "man" } },
            { "Political Organization", new[] { "gov", "org" } },
            { "Primary/Secondary Education", new[] { "edu" } },
            { "Printing", new[] { "med", "rec" } },
            { "Professional Training & Coaching", new[] { "corp" } },
            { "Program Development", new[] { "corp", "org" } },
            { "Public Policy", new[] { "gov" } },
            { "Public Relations and Communications", new[] { "corp" } },
            { "Public Safety", new[] { "gov" } },
            { "Publishing", new[] { "med", "rec" } },
            { "Railroad Manufacture", new[] { "man" } },
            { "Ranching", new[] { "agr" } },
            { "Real Estate", new[] { "cons", "fin", "good" } },
            { "Recreational Facilities and Services", new[] { "rec", "serv" } },
            { "Religious Institutions", new[] { "org", "serv" } },
            { "Renewables & Environment", new[] { "gov", "man", "org" } },
            { "Research", new[] { "edu", "gov" } },
            { "Restaurants", new[] { "rec", "serv" } },
            { "Retail", new[] { "good", "man" } },
            { "Security and Investigations", new[] { "corp", "org", "serv" } },
            { "Semiconductors", new[] { "tech" } },
            { "Shipbuilding", new[] { "man" } },
            { "Sporting Goods", new[] { "good", "rec" } },
            { "Sports", new[] { "rec" } },
            { "Staffing and Recruiting", new[] { "corp" } },
            { "Supermarkets", new[] { "good" } },
            { "Telecommunications", new[] { "gov", "tech" } },
            { "Textiles", new[] { "man" } },
            { "Think Tanks", new[] { "gov", "org" } },
            { "Tobacco", new[] { "good" } },
            { "Translation and Localization", new[] { "corp", "gov", "serv" } },
            { "Transportation/Trucking/Railroad", new[] { "tran" } },
            { "Utilities", new[] { "man" } },
            { "Venture Capital & Private Equity", new[] { "fin", "tech" } },
            { "Veterinary", new[] { "hlth" } },
            { "Warehousing", new[] { "tran" } },
            { "Wholesale", new[] { "good" } },
            { "Wine and Spirits", new[] { "good", "man", "rec" } },
            { "Wireless", new[] { "tech" } },
            { "Writing and Editing", new[] { "art", "med", "rec" } },
        };

        public LinkedInQuery(ILinkedInRepository repository, IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
        {
            _repository = repository;
            _industriesQuery = industriesQuery;
            _locationQuery = locationQuery;
        }

        LinkedInProfile ILinkedInQuery.GetProfile(string linkedInId)
        {
            return _repository.GetProfile(linkedInId);
        }

        LinkedInProfile ILinkedInQuery.GetProfile(Guid userId)
        {
            return _repository.GetProfile(userId);
        }

        IList<Industry> ILinkedInQuery.GetIndustries(string industry)
        {
            if (string.IsNullOrEmpty(industry))
                return new List<Industry>();

            IList<string> groups;
            if (!_industryGroups.TryGetValue(industry, out groups))
                return new List<Industry>();

            var names = (from g in groups
                         let n = _groups.ContainsKey(g) ? _groups[g] : null
                         where n != null
                         select n).Distinct();
            return (from n in names select _industriesQuery.GetIndustry(n)).ToList();
        }

        LocationReference ILinkedInQuery.GetLocation(string countryIsoCode, string location)
        {
            if (string.IsNullOrEmpty(countryIsoCode) || string.IsNullOrEmpty(location))
                return null;

            var country = _locationQuery.GetCountryByIsoCode(countryIsoCode);
            if (country == null)
                return null;

            return _locationQuery.ResolveLocation(country, location);
        }
    }
}
