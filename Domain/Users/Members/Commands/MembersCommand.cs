using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Members.Commands
{
    public class MembersCommand
        : IMembersCommand
    {
        private readonly IMembersRepository _repository;

        public MembersCommand(IMembersRepository repository)
        {
            _repository = repository;
        }

        void IMembersCommand.CreateMember(Member member)
        {
            if (member.VisibilitySettings == null)
                member.VisibilitySettings = new VisibilitySettings();
            if (member.Address == null)
                member.Address = new Address();

            PrepareCreate(member);
            member.Validate();
            _repository.CreateMember(member);
        }

        void IMembersCommand.UpdateMember(Member member)
        {
            if (member.VisibilitySettings == null)
                member.VisibilitySettings = new VisibilitySettings();
            if (member.Address == null)
                member.Address = new Address();

            PrepareUpdate(member);
            member.Validate();
            _repository.UpdateMember(member);
        }

        private static void PrepareCreate(Member member)
        {
            member.Prepare();
            member.LastUpdatedTime = member.CreatedTime;
            Prepare(member);
        }

        private static void PrepareUpdate(Member member)
        {
            member.LastUpdatedTime = DateTime.Now;
            Prepare(member);
        }

        private static void Prepare(IHaveEmailAddresses member)
        {
            // Remove null and duplicate email addresses.

            if (member.EmailAddresses != null)
            {
                var distinctAddresses = (from a in member.EmailAddresses
                                         where !string.IsNullOrEmpty(a.Address.Trim())
                                         select a.Address.ToLower()).Distinct();
                member.EmailAddresses = (from a in distinctAddresses
                                         select new EmailAddress
                                         {
                                             Address = a,
                                             IsVerified = (from e in member.EmailAddresses where e.Address == a select e).Any(e => e.IsVerified)
                                         }).ToList();
            }
        }
    }
}
