using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Affiliations.Verticals.Commands
{
    public interface IVerticalsCommand
    {
        void CreateVertical(Vertical vertical);
        void UpdateVertical(Vertical vertical);

        Vertical GetVertical(Guid id);
        Vertical GetVerticalByHost(string host);
        Vertical GetVerticalByUrl(string url);
        IList<Vertical> GetVerticals();
    }
}