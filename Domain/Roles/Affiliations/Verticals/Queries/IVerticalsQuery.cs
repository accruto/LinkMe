using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Affiliations.Verticals.Queries
{
    public interface IVerticalsQuery
    {
        Vertical GetVertical(Guid id);
        Vertical GetVertical(string name);
        Vertical GetVerticalByHost(string host);
        Vertical GetVerticalByUrl(string url);
        IList<Vertical> GetVerticals();
    }
}