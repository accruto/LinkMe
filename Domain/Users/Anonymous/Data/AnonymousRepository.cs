using System;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Users.Anonymous.Data
{
    public class AnonymousRepository
        : Repository, IAnonymousRepository
    {
        private static readonly Func<AnonymousDataContext, Guid, AnonymousUserEntity> GetAnonymousUserEntity
            = CompiledQuery.Compile((AnonymousDataContext dc, Guid id)
                => (from u in dc.AnonymousUserEntities
                    where u.id == id
                    select u).SingleOrDefault());

        private static readonly Func<AnonymousDataContext, Guid, AnonymousContact> GetContact
            = CompiledQuery.Compile((AnonymousDataContext dc, Guid id)
                => (from uc in dc.AnonymousContactEntities
                    join c in dc.ContactDetailEntities on uc.contactDetailsId equals c.id
                    where c.id == id
                    select uc.Map(c)).SingleOrDefault());

        private static readonly Func<AnonymousDataContext, Guid, ContactDetails, AnonymousContact> GetContactByDetails
            = CompiledQuery.Compile((AnonymousDataContext dc, Guid userId, ContactDetails contactDetails)
                => (from u in dc.AnonymousUserEntities
                    join uc in dc.AnonymousContactEntities on u.id equals uc.userId
                    join c in dc.ContactDetailEntities on uc.contactDetailsId equals c.id
                    where u.id == userId
                    && c.email == contactDetails.EmailAddress
                    && c.firstName == contactDetails.FirstName
                    && c.lastName == contactDetails.LastName
                    select uc.Map(c)).SingleOrDefault());

        public AnonymousRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IAnonymousRepository.CreateContact(IAnonymousUser user, AnonymousContact contact)
        {
            using (var dc = CreateContext())
            {
                // Ensure there is a user.

                var userEntity = GetAnonymousUserEntity(dc, user.Id);
                if (userEntity == null)
                    dc.AnonymousUserEntities.InsertOnSubmit(user.Map());

                dc.AnonymousContactEntities.InsertOnSubmit(contact.Map(user.Id));
                dc.SubmitChanges();
            }
        }

        AnonymousContact IAnonymousRepository.GetContact(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetContact(dc, id);
            }
        }

        AnonymousContact IAnonymousRepository.GetContact(IAnonymousUser user, ContactDetails contactDetails)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetContactByDetails(dc, user.Id, contactDetails);
            }
        }

        private AnonymousDataContext CreateContext()
        {
            return CreateContext(c => new AnonymousDataContext(c));
        }
    }
}
