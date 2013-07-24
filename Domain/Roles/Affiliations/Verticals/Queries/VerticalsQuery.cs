using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Affiliations.Verticals.Queries
{
    public class VerticalsQuery
        : IVerticalsQuery
    {
        private readonly IVerticalsRepository _repository;

        public VerticalsQuery(IVerticalsRepository repository)
        {
            _repository = repository;
        }

        IList<Vertical> IVerticalsQuery.GetVerticals()
        {
            return _repository.GetVerticals(false);
        }

        Vertical IVerticalsQuery.GetVertical(Guid id)
        {
            return _repository.GetVertical(id, false);
        }

        Vertical IVerticalsQuery.GetVertical(string name)
        {
            return _repository.GetVertical(name);
        }

        Vertical IVerticalsQuery.GetVerticalByHost(string host)
        {
            return _repository.GetVerticalByHost(host, false);
        }

        Vertical IVerticalsQuery.GetVerticalByUrl(string url)
        {
            return _repository.GetVerticalByUrl(url, false);
        }
    }
}