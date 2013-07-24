using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;

namespace LinkMe.AcceptanceTest.Employers.Candidates
{
    public enum CreditAllocation
    {
        Employer,
        Organisation,
        ParentOrganisation,
    }

    public abstract class CreditInfo
    {
        private readonly bool _verified;
        private readonly IEmployerAccountsCommand _employerAccountsCommand;
        private readonly IOrganisationsCommand _organisationsCommand;

        protected CreditInfo(bool verified, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand)
        {
            _verified = verified;
            _employerAccountsCommand = employerAccountsCommand;
            _organisationsCommand = organisationsCommand;
        }

        public abstract Employer CreateEmployer(Member[] members);
        public abstract bool CanContact { get; }
        public abstract bool HasUsedCredit { get; }
        public abstract bool HasExpired { get; }
        public abstract bool ShouldUseCredit { get; }
        public abstract CreditAllocation? CreditAllocation { get; }

        protected Employer CreateEmployer(bool createParentOrganisation)
        {
            IOrganisation organisation;
            if (_verified)
            {
                Organisation parentOrganisation = null;
                if (createParentOrganisation)
                    parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());

                organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, parentOrganisation, Guid.NewGuid());
            }
            else
            {
                organisation = _organisationsCommand.CreateTestOrganisation(0);
            }

            return _employerAccountsCommand.CreateTestEmployer(1, organisation);
        }
    }

    public class AnonymousCreditInfo
        : CreditInfo
    {
        public AnonymousCreditInfo(IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand)
            : base(false, employerAccountsCommand, organisationsCommand)
        {
        }

        public override Employer CreateEmployer(Member[] members)
        {
            return null;
        }

        public override bool CanContact
        {
            get { return false; }
        }

        public override bool HasUsedCredit
        {
            get { return false; }
        }

        public override bool HasExpired
        {
            get { return true; }
        }

        public override bool ShouldUseCredit
        {
            get { return false; }
        }

        public override CreditAllocation? CreditAllocation
        {
            get { return null; }
        }
    }

    public class NoAllocationsCreditInfo
        : CreditInfo
    {
        public NoAllocationsCreditInfo(bool verified, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand)
            : base(verified, employerAccountsCommand, organisationsCommand)
        {
        }

        public override Employer CreateEmployer(Member[] members)
        {
            return CreateEmployer(false);
        }

        public override bool CanContact
        {
            get { return false; }
        }

        public override bool HasUsedCredit
        {
            get { return false; }
        }

        public override bool HasExpired
        {
            get { return true; }
        }

        public override bool ShouldUseCredit
        {
            get { return false; }
        }

        public override CreditAllocation? CreditAllocation
        {
            get { return null; }
        }
    }

    public abstract class AllocationCreditInfo
        : CreditInfo
    {
        private readonly bool _hasExpired;
        private readonly CreditAllocation _creditAllocation;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IAllocationsCommand _allocationsCommand;
        private Guid? _allocationId;

        protected AllocationCreditInfo(bool verified, bool hasExpired, CreditAllocation creditAllocation, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, ICreditsQuery creditsQuery, IAllocationsCommand allocationsCommand)
            : base(verified, employerAccountsCommand, organisationsCommand)
        {
            _hasExpired = hasExpired;
            _creditAllocation = creditAllocation;
            _creditsQuery = creditsQuery;
            _allocationsCommand = allocationsCommand;
        }

        public Employer CreateEmployer(int? quantity, DateTime? expiryDate)
        {
            var employer = CreateEmployer(_creditAllocation == Candidates.CreditAllocation.ParentOrganisation);

            Guid ownerId;
            switch (_creditAllocation)
            {
                case Candidates.CreditAllocation.ParentOrganisation:
                    ownerId = ((Organisation)employer.Organisation).ParentId.Value;
                    break;

                case Candidates.CreditAllocation.Organisation:
                    ownerId = employer.Organisation.Id;
                    break;

                default:
                    ownerId = employer.Id;
                    break;
            }

            var allocation = new Allocation { OwnerId = ownerId, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = quantity, ExpiryDate = expiryDate };
            _allocationsCommand.CreateAllocation(allocation);
            _allocationId = allocation.Id;

            return employer;
        }

        public override CreditAllocation? CreditAllocation
        {
            get { return _creditAllocation; }
        }

        public override bool HasExpired
        {
            get { return _hasExpired; }
        }

        protected void ExpireCredits()
        {
            if (_allocationId != null)
                _allocationsCommand.Deallocate(_allocationId.Value);
        }
    }

    public class NoCreditsCreditInfo
        : AllocationCreditInfo
    {
        public NoCreditsCreditInfo(bool verified, bool hasExpired, CreditAllocation creditAllocation, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, ICreditsQuery creditsQuery, IAllocationsCommand allocationsCommand)
            : base(verified, hasExpired, creditAllocation, employerAccountsCommand, organisationsCommand, creditsQuery, allocationsCommand)
        {
        }

        public override Employer CreateEmployer(Member[] members)
        {
            return CreateEmployer(0, DateTime.Now.AddMonths(HasExpired ? -6 : 6).Date);
        }

        public override bool CanContact
        {
            get { return false; }
        }

        public override bool HasUsedCredit
        {
            get { return false; }
        }

        public override bool ShouldUseCredit
        {
            get { return false; }
        }
    }

    public class NoCreditsUsedCreditCreditInfo
        : AllocationCreditInfo
    {
        private readonly IEmployerCreditsCommand _employerCreditsCommand;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;

        public NoCreditsUsedCreditCreditInfo(bool verified, bool hasExpired, CreditAllocation creditAllocation, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, ICreditsQuery creditsQuery, IAllocationsCommand allocationsCommand, IEmployerCreditsCommand employerCreditsCommand, IEmployerMemberViewsQuery employerMemberViewsQuery)
            : base(verified, hasExpired, creditAllocation, employerAccountsCommand, organisationsCommand, creditsQuery, allocationsCommand)
        {
            _employerCreditsCommand = employerCreditsCommand;
            _employerMemberViewsQuery = employerMemberViewsQuery;
        }

        public override Employer CreateEmployer(Member[] members)
        {
            var employer = CreateEmployer(members.Length, DateTime.Now.AddMonths(6).Date);
            foreach (var member in members)
                _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));
            if (HasExpired)
                ExpireCredits();
            return employer;
        }

        public override bool CanContact
        {
            get { return false; }
        }

        public override bool HasUsedCredit
        {
            get { return true; }
        }

        public override bool ShouldUseCredit
        {
            get { return false; }
        }
    }

    public class SomeCreditsCreditInfo
        : AllocationCreditInfo
    {
        private readonly int _quantity;

        public SomeCreditsCreditInfo(int quantity, bool verified, bool hasExpired, CreditAllocation creditAllocation, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, ICreditsQuery creditsQuery, IAllocationsCommand allocationsCommand)
            : base(verified, hasExpired, creditAllocation, employerAccountsCommand, organisationsCommand, creditsQuery, allocationsCommand)
        {
            _quantity = quantity;
        }

        public override Employer CreateEmployer(Member[] members)
        {
            return CreateEmployer(_quantity, DateTime.Now.AddMonths(HasExpired ? -6 : 6).Date);
        }

        public override bool CanContact
        {
            get { return true; }
        }

        public override bool HasUsedCredit
        {
            get { return false; }
        }

        public override bool ShouldUseCredit
        {
            get { return true; }
        }
    }

    public class SomeCreditsUsedCreditCreditInfo
        : AllocationCreditInfo
    {
        private readonly IEmployerCreditsCommand _employerCreditsCommand;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;

        public SomeCreditsUsedCreditCreditInfo(bool verified, bool hasExpired, CreditAllocation creditAllocation, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, ICreditsQuery creditsQuery, IAllocationsCommand allocationsCommand, IEmployerCreditsCommand employerCreditsCommand, IEmployerMemberViewsQuery employerMemberViewsQuery)
            : base(verified, hasExpired, creditAllocation, employerAccountsCommand, organisationsCommand, creditsQuery, allocationsCommand)
        {
            _employerCreditsCommand = employerCreditsCommand;
            _employerMemberViewsQuery = employerMemberViewsQuery;
        }

        public override Employer CreateEmployer(Member[] members)
        {
            var employer = CreateEmployer(10, DateTime.Now.AddMonths(6).Date);
            foreach (var member in members)
                _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));
            if (HasExpired)
                ExpireCredits();
            return employer;
        }

        public override bool CanContact
        {
            get { return true; }
        }

        public override bool HasUsedCredit
        {
            get { return true; }
        }

        public override bool ShouldUseCredit
        {
            get { return false; }
        }
    }

    public class UnlimitedCreditsCreditInfo
        : AllocationCreditInfo
    {
        public UnlimitedCreditsCreditInfo(bool verified, bool hasExpired, CreditAllocation creditAllocation, IEmployerAccountsCommand employerAccountsCommand, IOrganisationsCommand organisationsCommand, ICreditsQuery creditsQuery, IAllocationsCommand allocationsCommand)
            : base(verified, hasExpired, creditAllocation, employerAccountsCommand, organisationsCommand, creditsQuery, allocationsCommand)
        {
        }

        public override Employer CreateEmployer(Member[] members)
        {
            return CreateEmployer(null, DateTime.Now.AddMonths(HasExpired ? -6 : 6).Date);
        }

        public override bool CanContact
        {
            get { return true; }
        }

        public override bool HasUsedCredit
        {
            get
            {
                // Unlimited always means you get access if not already expired.

                return true;
            }
        }

        public override bool ShouldUseCredit
        {
            get { return false; }
        }
    }
}
