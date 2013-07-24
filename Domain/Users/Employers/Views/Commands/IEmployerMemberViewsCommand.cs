using System.Collections.Generic;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Domain.Users.Employers.Views.Commands
{
    public interface IEmployerMemberViewsCommand
    {
        void ViewMember(ChannelApp app, IEmployer employer, IMember member);
        void ViewMember(ChannelApp app, IEmployer employer, IMember member, InternalApplication application);

        MemberAccess AccessMember(ChannelApp app, IEmployer employer, ProfessionalView view, MemberAccessReason reason);
        void CheckCanAccessMember(ChannelApp app, IEmployer employer, ProfessionalView view, MemberAccessReason reason);

        IList<MemberAccess> AccessMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, MemberAccessReason reason);
        void CheckCanAccessMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, MemberAccessReason reason);
    }
}