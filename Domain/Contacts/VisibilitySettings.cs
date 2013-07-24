namespace LinkMe.Domain.Contacts
{
    public class VisibilitySettings
    {
        private readonly PersonalVisibilitySettings _personalVisibilitySettings = new PersonalVisibilitySettings();
        private readonly ProfessionalVisibilitySettings _professionalVisibilitySettings = new ProfessionalVisibilitySettings();

        public PersonalVisibilitySettings Personal
        {
            get { return _personalVisibilitySettings; }
        }

        public ProfessionalVisibilitySettings Professional
        {
            get { return _professionalVisibilitySettings; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (VisibilitySettings))
                return false;
            return Equals((VisibilitySettings) obj);
        }

        public bool Equals(VisibilitySettings other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other._personalVisibilitySettings, _personalVisibilitySettings)
                && Equals(other._professionalVisibilitySettings, _professionalVisibilitySettings);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_personalVisibilitySettings != null ? _personalVisibilitySettings.GetHashCode() : 0)*397)
                    ^ (_professionalVisibilitySettings != null ? _professionalVisibilitySettings.GetHashCode() : 0);
            }
        }
    }
}
