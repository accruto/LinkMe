namespace LinkMe.Domain.Roles.Registration
{
    public struct ReferralContact
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string Website { get; set; }
        public string Employees { get; set; }

        public string FirstName
        {
            get { return Name.Split(new[] { ' ' })[0]; }
        }
    }
}