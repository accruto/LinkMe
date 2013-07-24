namespace LinkMe.Domain.Contacts
{
    public enum PersonalContactDegree
        : byte
    {
        Self = 0,
        FirstDegree = 1,
        SecondDegree = 2,
        Public = 3,
        Anonymous = 4,
        Representative = 5,
        Representee = 6,
    }
}
