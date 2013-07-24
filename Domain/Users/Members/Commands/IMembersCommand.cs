using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.Commands
{
    public interface IMembersCommand
    {
        void CreateMember(Member member);
        void UpdateMember(Member member);
    }
}
