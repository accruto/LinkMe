using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class MessageId
    {
        private const int MaxHashedLength = 15; // Max length of 30 divided by 2 for hex conversion.
        private readonly string _value;

        public MessageId(string value)
            : this(value, true)
        {
        }

        public static MessageId NewMessageId()
        {
            // Maximum length of a message id is 30 characters so generate a hash of a guid for the id.

            var bytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());

            var hashBytes = new byte[MaxHashedLength];
            for (var index = 0; index < bytes.Length; ++index)
                hashBytes[index % MaxHashedLength] = (byte)(hashBytes[index % MaxHashedLength] ^ bytes[index]);

            return new MessageId(hashBytes.ToHexString(), false);
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MessageId);
        }

        public bool Equals(MessageId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._value, _value);
        }

        public override int GetHashCode()
        {
            return (_value != null ? _value.GetHashCode() : 0);
        }

        private MessageId(string value, bool validate)
        {
            // Validate the value.

            if (validate)
            {
                if (value == null || value.Length != 30 || !RegularExpressions.CompleteAlphaNumeric.IsMatch(value))
                    throw new ArgumentException("The value '" + value + "' is not a valid message id.");
            }

            _value = value;
        }
    }

    public class MessageTimestamp
    {
        //                               YYYY      DD                             MM                  HH                      NN          SS          KKK       000s     OOO
        private const string Pattern = "^([0-9]{4})(0[1-9]|[1-2]{1}[0-9]{1}|30|31)(0[1-9]{1}|10|11|12)([0-1][0-9]|20|21|22|23)([0-5][0-9])([0-5][0-9])([0-9]{3})000([+-])([0-9]{3})$";
        private static readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);

        private readonly DateTime _value;

        public MessageTimestamp(DateTime value)
        {
            // Only take the bits you need.

            _value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond);
        }

        public static MessageTimestamp Parse(string value)
        {
            var match = _regex.Match(value);
            if (!match.Success)
                throw new ArgumentException("The value '" + value + "' is not a valid format.");

            // Pick out the pieces.

            var year = int.Parse(match.Groups[1].Value);
            var day = int.Parse(match.Groups[2].Value);
            var month = int.Parse(match.Groups[3].Value);
            var hour = int.Parse(match.Groups[4].Value);
            var minute = int.Parse(match.Groups[5].Value);
            var second = int.Parse(match.Groups[6].Value);
            var millisecond = int.Parse(match.Groups[7].Value);
            return new MessageTimestamp(new DateTime(year, month, day, hour, minute, second, millisecond));
        }

        public override string ToString()
        {
            return _value.ToString("yyyyddMMHHmmssfff000+600");
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MessageTimestamp);
        }

        public bool Equals(MessageTimestamp other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._value.Equals(_value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public DateTime ToDateTime()
        {
            return _value;
        }
    }

    [DataContract(Namespace = "")]
    public abstract class MessageInfo
        : ICloneable
    {
        [Required, DefaultNewMessageId]
        public MessageId MessageId { get; set; }

        [Required, DefaultNowMessageTimestamp]
        public MessageTimestamp MessageTimestamp { get; set; }

        [Required, ApiVersion, DefaultValue("xml-4.2")]
        public string ApiVersion { get; set; }

        [DataMember(Order = 1)]
        internal string messageID
        {
            get { return MessageId == null ? null : MessageId.ToString(); }
            set { MessageId = new MessageId(value); }
        }

        [DataMember(Order = 2)]
        internal string messageTimestamp
        {
            get { return MessageTimestamp == null ? null : MessageTimestamp.ToString(); }
            set { MessageTimestamp = MessageTimestamp.Parse(value); }
        }

        [DataMember(Order = 3)]
        internal string apiVersion
        {
            get { return ApiVersion; }
            set { ApiVersion = value; }
        }

        public abstract object Clone();

        protected void CopyTo(MessageInfo clone)
        {
            clone.MessageId = MessageId;
            clone.MessageTimestamp = MessageTimestamp;
            clone.ApiVersion = ApiVersion;
        }
    }

    [DataContract(Namespace = "")]
    public class RequestMessageInfo
        : MessageInfo
    {
        [DefaultValue(60), NumericValue(1, 999)]
        public int Timeout { get; set; }

        [DataMember(Order = 3)]
        private int timeoutValue
        {
            get { return Timeout; }
            set { Timeout = value; }
        }

        public override object Clone()
        {
            var clone = new RequestMessageInfo();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(RequestMessageInfo clone)
        {
            base.CopyTo(clone);
            clone.Timeout = Timeout;
        }
    }

    [DataContract(Namespace = "")]
    public class ResponseMessageInfo
        : MessageInfo
    {
        public override object Clone()
        {
            var clone = new ResponseMessageInfo();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(ResponseMessageInfo clone)
        {
            base.CopyTo(clone);
        }
    }
}
