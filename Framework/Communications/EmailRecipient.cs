using System;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Framework.Communications
{
    [Serializable]
    public class EmailRecipient
        : IInstrumentable
    {
        private readonly string _address;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _displayName;

        public EmailRecipient(string address, string displayName, string firstName, string lastName)
            : this(address, displayName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public EmailRecipient(string address, string displayName)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("The address must be specified.", "address");

            _address = address;
            _displayName = displayName ?? string.Empty;
        }

        public EmailRecipient(string address)
            : this(address, null)
        {
        }

        public string Address
        {
            get { return _address; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EmailRecipient))
                return false;
            var other = (EmailRecipient)obj;
            return _address == other._address && _displayName == other._displayName;
        }

        public override int GetHashCode()
        {
            return _address.GetHashCode() ^ _displayName.GetHashCode();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(DisplayName) ? Address : string.Format("{0} <{1}>", DisplayName, Address);
        }

        void IInstrumentable.Write(IInstrumentationWriter writer)
        {
            writer.Write("Address", _address);
            writer.Write("DisplayName", _displayName);
        }
    }
}