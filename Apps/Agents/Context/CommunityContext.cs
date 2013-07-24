using System;
using LinkMe.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Apps.Agents.Context
{
    public interface ICommunityContext
        : ISubActivityContext
    {
        void Set(Community community);
        void Reset();
        bool IsSet { get; }
        Guid? Id { get; }
    }

    internal class DefaultCommunityContext
        : ICommunityContext
    {
        private Community _community;

        void ICommunityContext.Set(Community community)
        {
            _community = community;
        }

        void ICommunityContext.Reset()
        {
            _community = null;
        }

        bool ICommunityContext.IsSet
        {
            get { return _community != null; }
        }

        Guid? ICommunityContext.Id
        {
            get { return _community == null ? (Guid?)null : _community.Id; }
        }
    }
}