using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Affiliations.Verticals
{
    public interface IVerticalsRepository
    {
        void CreateVertical(Vertical vertical);
        void UpdateVertical(Vertical vertical);

        Vertical GetVertical(Guid id, bool includeDisabled);
        Vertical GetVertical(string name);
        Vertical GetVerticalByHost(string host, bool includeDisabled);
        Vertical GetVerticalByUrl(string url, bool includeDisabled);
        IList<Vertical> GetVerticals(bool includeDisabled);
    }
}
