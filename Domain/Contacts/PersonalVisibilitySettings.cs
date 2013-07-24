using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Contacts
{
    public class PersonalVisibilitySettings
    {
        #region High

        public const PersonalVisibility HighPublic
            = PersonalVisibility.Name | PersonalVisibility.Photo | PersonalVisibility.Gender | PersonalVisibility.Age
            | PersonalVisibility.CandidateStatus | PersonalVisibility.DesiredJob | PersonalVisibility.CountrySubdivision
            | PersonalVisibility.Country | PersonalVisibility.SendInvites | PersonalVisibility.SendMessages
            | PersonalVisibility.FriendsList | PersonalVisibility.CurrentJobs | PersonalVisibility.Education
            | PersonalVisibility.Interests | PersonalVisibility.Affiliations | PersonalVisibility.PreviousJob;

        public const PersonalVisibility HighSecondDegree
            = HighPublic | PersonalVisibility.PhoneNumbers
            | PersonalVisibility.EmailAddress | PersonalVisibility.Suburb | PersonalVisibility.PreviousJob;

        public const PersonalVisibility HighFirstDegree
            = HighSecondDegree | PersonalVisibility.Resume;

        #endregion

        #region Moderate

        public const PersonalVisibility ModeratePublic
            = PersonalVisibility.Name | PersonalVisibility.Photo | PersonalVisibility.Country
            | PersonalVisibility.SendInvites | PersonalVisibility.SendMessages | PersonalVisibility.FriendsList;

        public const PersonalVisibility ModerateSecondDegree
            = ModeratePublic | PersonalVisibility.CandidateStatus
            | PersonalVisibility.DesiredJob | PersonalVisibility.CountrySubdivision | PersonalVisibility.CurrentJobs
            | PersonalVisibility.Interests | PersonalVisibility.Affiliations | PersonalVisibility.PreviousJob;

        public const PersonalVisibility ModerateFirstDegree
            = ModerateSecondDegree | PersonalVisibility.Gender | PersonalVisibility.Age
            | PersonalVisibility.PhoneNumbers | PersonalVisibility.EmailAddress | PersonalVisibility.Suburb
            | PersonalVisibility.Education | PersonalVisibility.PreviousJob;

        #endregion

        #region Less

        public const PersonalVisibility LessPublic
            = PersonalVisibility.None;

        public const PersonalVisibility LessSecondDegree
            = LessPublic | PersonalVisibility.Name;

        public const PersonalVisibility LessFirstDegree
            = LessSecondDegree | PersonalVisibility.Photo | PersonalVisibility.Gender
            | PersonalVisibility.Age | PersonalVisibility.CandidateStatus | PersonalVisibility.DesiredJob
            | PersonalVisibility.CountrySubdivision | PersonalVisibility.Country | PersonalVisibility.SendMessages
            | PersonalVisibility.FriendsList | PersonalVisibility.CurrentJobs | PersonalVisibility.Education
            | PersonalVisibility.Interests | PersonalVisibility.Affiliations;

        #endregion

        #region Invisible

        public const PersonalVisibility InvisiblePublic = PersonalVisibility.None;
        public const PersonalVisibility InvisibleSecondDegree = InvisiblePublic;
        public const PersonalVisibility InvisibleFirstDegree = InvisibleSecondDegree | PersonalVisibility.Name;

        #endregion

        #region Default

        public const PersonalVisibility DefaultPublic = ModeratePublic;
        public const PersonalVisibility DefaultSecondDegree = ModerateSecondDegree;
        public const PersonalVisibility DefaultFirstDegree = ModerateFirstDegree;

        #endregion

        #region Anonymous

        public const PersonalVisibility Anonymous
            = PersonalVisibility.Name | PersonalVisibility.Photo
            | PersonalVisibility.Gender | PersonalVisibility.Age;

        #endregion

        private PersonalVisibility _firstDegreeVisibility = DefaultFirstDegree;
        private PersonalVisibility _secondDegreeVisibility = DefaultSecondDegree;
        private PersonalVisibility _publicVisibility = DefaultPublic;

        public PersonalVisibility FirstDegreeVisibility
        {
            get { return _firstDegreeVisibility; }
            set { _firstDegreeVisibility = value; }
        }

        public PersonalVisibility SecondDegreeVisibility
        {
            get { return _secondDegreeVisibility; }
            set { _secondDegreeVisibility = value; }
        }

        public PersonalVisibility PublicVisibility
        {
            get { return _publicVisibility; }
            set { _publicVisibility = value; }
        }

        public void Set(PersonalContactDegree degree, PersonalVisibility flag)
        {
            // Make sure the flag is set for all lower degrees.

            SetFlag(degree, flag);

            // Make sure it is not set for all higher degrees.

            if (degree < PersonalContactDegree.Public)
                ResetFlag(degree + 1, flag);
        }

        private void SetFlag(PersonalContactDegree degree, PersonalVisibility flag)
        {
            switch (degree)
            {
                case PersonalContactDegree.Self:
                    return;

                case PersonalContactDegree.FirstDegree:
                    _firstDegreeVisibility = _firstDegreeVisibility.SetFlag(flag);
                    break;

                case PersonalContactDegree.SecondDegree:
                    _secondDegreeVisibility = _secondDegreeVisibility.SetFlag(flag);
                    break;

                case PersonalContactDegree.Public:
                    _publicVisibility = _publicVisibility.SetFlag(flag);
                    break;

                default:
                    return;
            }

            SetFlag(degree - 1, flag);
        }

        private void ResetFlag(PersonalContactDegree degree, PersonalVisibility flag)
        {
            switch (degree)
            {
                case PersonalContactDegree.Self:
                    break;

                case PersonalContactDegree.FirstDegree:
                    _firstDegreeVisibility = _firstDegreeVisibility.ResetFlag(flag);
                    break;

                case PersonalContactDegree.SecondDegree:
                    _secondDegreeVisibility = _secondDegreeVisibility.ResetFlag(flag);
                    break;

                case PersonalContactDegree.Public:
                    _publicVisibility = _publicVisibility.ResetFlag(flag);
                    return;

                default:
                    return;
            }

            ResetFlag(degree + 1, flag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (PersonalVisibilitySettings))
                return false;
            return Equals((PersonalVisibilitySettings) obj);
        }

        public bool Equals(PersonalVisibilitySettings other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other._firstDegreeVisibility, _firstDegreeVisibility)
                && Equals(other._secondDegreeVisibility, _secondDegreeVisibility)
                && Equals(other._publicVisibility, _publicVisibility);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = _firstDegreeVisibility.GetHashCode();
                result = (result*397) ^ _secondDegreeVisibility.GetHashCode();
                result = (result*397) ^ _publicVisibility.GetHashCode();
                return result;
            }
        }
    }
}
