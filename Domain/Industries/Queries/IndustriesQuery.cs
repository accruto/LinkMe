using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Industries.Queries
{
    public class IndustriesQuery
        : IIndustriesQuery
    {
        private readonly IIndustriesRepository _repository;
        private readonly IDictionary<Guid, Industry> _industriesById;
        private readonly IDictionary<string, Industry> _industriesByName;
        private readonly IDictionary<string, Industry> _industriesByShortName;
        private readonly IDictionary<string, Industry> _industriesByUrlName;
        private readonly IList<Industry> _industries;

        public IndustriesQuery(IIndustriesRepository repository)
        {
            // Cache the collections but always return clones.

            _repository = repository;
            _industries = _repository.GetIndustries();
            _industriesById = _industries.ToDictionary(i => i.Id);
            _industriesByName = _industries.SelectMany(i => (from n in i.AllNames select n.ToLower()), (i, n) => new { i, n }).ToDictionary(p => p.n, p => p.i);
            _industriesByShortName = _industries.ToDictionary(i => i.ShortName.ToLower());
            _industriesByUrlName = _industries.SelectMany(i => (from n in i.AllUrlNames select n.ToLower()), (i, n) => new { i, n }).ToDictionary(p => p.n, p => p.i);
        }

        IList<Industry> IIndustriesQuery.GetIndustries()
        {
            return (from i in _industries select Clone(i)).ToList();
        }

        IList<Industry> IIndustriesQuery.GetIndustries(IEnumerable<Guid> ids)
        {
            return ids == null
                ? new List<Industry>()
                : (from id in ids.Distinct()
                   let i = GetIndustry(id)
                   where i != null
                   select i).ToList();
        }

        Industry IIndustriesQuery.GetIndustry(Guid id)
        {
            return GetIndustry(id);
        }

        Industry IIndustriesQuery.GetIndustry(string name)
        {
            return GetIndustry(name);
        }

        Industry IIndustriesQuery.GetIndustryByUrlName(string urlName)
        {
            return GetIndustryByUrlName(urlName);
        }

        Industry IIndustriesQuery.GetIndustryByAnyName(string name)
        {
            return (GetIndustry(name) ?? GetIndustryByShortName(name)) ?? GetIndustryByUrlName(name);
        }

        private static Industry Clone(Industry industry)
        {
            return new Industry
            {
                Id = industry.Id,
                Name = industry.Name,
                ShortName = industry.ShortName,
                UrlName = industry.UrlName,
                KeywordExpression = industry.KeywordExpression,
                Aliases = (from a in industry.Aliases select a).ToList(),
                UrlAliases = (from a in industry.UrlAliases select a).ToList(),
            };
        }

        private Industry GetIndustry(Guid id)
        {
            Industry industry;
            _industriesById.TryGetValue(id, out industry);
            return industry != null ? Clone(industry) : null;
        }

        private Industry GetIndustry(string name)
        {
            Industry industry;
            _industriesByName.TryGetValue(name.ToLower(), out industry);
            return industry != null ? Clone(industry) : null;
        }

        private Industry GetIndustryByShortName(string name)
        {
            Industry industry;
            _industriesByShortName.TryGetValue(name.ToLower(), out industry);
            return industry != null ? Clone(industry) : null;
        }

        private Industry GetIndustryByUrlName(string urlName)
        {
            Industry industry;
            _industriesByUrlName.TryGetValue(urlName.ToLower(), out industry);
            return industry != null ? Clone(industry) : null;
        }
    }
}