using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Custodians;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Registration;

namespace LinkMe.Custodians
{
/*    public class Custodian
        : RegisteredUser, ICustodian
    {
        private Guid? _affiliateId;
        private bool _affiliateIdSet;

        public override UserType UserType
        {
            get { return UserType.Custodian; }
        }

        public override Guid? AffiliateId
        {
            get
            {
                if (_affiliateIdSet)
                    return _affiliateId;

                _affiliateIdSet = true;
                _affiliateId = Container.Current.Resolve<ICustodianAffiliationsCommand>().GetAffiliationId(Id);
                return _affiliateId;
            }
        }
    }
*/
}