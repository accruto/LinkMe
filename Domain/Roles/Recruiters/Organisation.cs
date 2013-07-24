using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Recruiters
{
    public class Organisation
        : IOrganisation, IHaveAddress, ICreditOwner
    {
        public const char FullNameSeparator = '\u2192';
        private Guid? _parentId;
        private string _parentFullName;

        [DefaultNewGuid]
        public Guid Id { get; set; }

        [Required, OrganisationName]
        public string Name { get; set; }

        [Prepare, Validate]
        public Address Address { get; set; }

        public Guid? AffiliateId { get; set; }

        public Guid? ParentId
        {
            get { return _parentId; }
        }

        public string ParentFullName
        {
            get { return _parentFullName; }
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(ParentFullName))
                    return Name;
                return ParentFullName + FullNameSeparator + Name;
            }
        }

        public virtual bool IsVerified
        {
            get { return false; }
        }

        public void SetParent(Organisation parent)
        {
            if (parent == null)
            {
                _parentId = null;
                _parentFullName = null;
            }
            else
            {
                _parentId = parent.Id;
                _parentFullName = parent.FullName;
            }
        }

        public void SetParent(Guid? parentId, string parentFullName)
        {
            _parentId = parentId;
            _parentFullName = parentFullName;
        }
    }
}