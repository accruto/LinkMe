namespace LinkMe.Domain.Contacts
{
    public class ProfessionalVisibilitySettings
    {
        /// <summary>
        /// The default employer access for a new Member.
        /// </summary>
        public const ProfessionalVisibility DefaultEmployment
            = ProfessionalVisibility.Resume | ProfessionalVisibility.Name | ProfessionalVisibility.PhoneNumbers
            | ProfessionalVisibility.RecentEmployers | ProfessionalVisibility.Communities | ProfessionalVisibility.Salary;

        public const ProfessionalVisibility DefaultPublic
            = 0;

        public const ProfessionalVisibility None = 0;

        public const ProfessionalVisibility InvisibleEmployment = ProfessionalVisibility.None;
        public const ProfessionalVisibility InvisiblePublic = ProfessionalVisibility.None;

        private ProfessionalVisibility _employmentVisibility = DefaultEmployment;
        private ProfessionalVisibility _publicVisibility = DefaultPublic;

        public ProfessionalVisibility EmploymentVisibility
        {
            get { return _employmentVisibility; }
            set { _employmentVisibility = value; }
        }

        public ProfessionalVisibility PublicVisibility
        {
            get { return _publicVisibility; }
            set { _publicVisibility = value; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (ProfessionalVisibilitySettings))
                return false;
            return Equals((ProfessionalVisibilitySettings) obj);
        }

        public bool Equals(ProfessionalVisibilitySettings other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other._employmentVisibility, _employmentVisibility)
                && Equals(other._publicVisibility, _publicVisibility);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_employmentVisibility.GetHashCode()*397) ^ _publicVisibility.GetHashCode();
            }
        }
    }
}
