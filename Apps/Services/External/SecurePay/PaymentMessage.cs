using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public enum TxnType
    {
        StandardPayment = 0,
        MobilePayment = 1,
        BatchPayment = 2,
        PeriodicPayment = 3,
        Refund = 4,
        ErrorReversal = 5,
        ClientReversal = 6,
        Preauthorise = 10,
        PreauthComplete = 11,
        RecurringPayment = 14,
        DirectEntryDebit = 15,
        DirectEntryCredit = 17,
        CardPresentPayment = 19,
        IvrPayment = 20,
        AntiFraudPayment = 21,
        AntiFraudOnlyPayment = 22,
    }

    public class SettlementDate
    {
        private const string Pattern = "^([0-9]{4})(0[1-9]|10|11|12)(0[1-9]|[1-2][0-9]|30|31)$";
        private static readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);

        private readonly DateTime _value;

        public SettlementDate(DateTime value)
        {
            // Only take the bits you need.

            _value = new DateTime(value.Year, value.Month, value.Day);
        }

        public static SettlementDate Parse(string value)
        {
            var match = _regex.Match(value);
            if (!match.Success)
                throw new ArgumentException("The value '" + value + "' is not a valid format.");

            // Pick out the pieces.

            var year = int.Parse(match.Groups[1].Value);
            var month = int.Parse(match.Groups[2].Value);
            var day = int.Parse(match.Groups[3].Value);
            return new SettlementDate(new DateTime(year, month, day));
        }

        public override string ToString()
        {
            return _value.ToString("yyyyMMdd");
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SettlementDate);
        }

        public bool Equals(SettlementDate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._value.Equals(_value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    [DataContract(Namespace = "")]
    public abstract class Txn
        : ICloneable
    {
        public abstract TxnType TxnType { get; }

        [NumericValue(1)]
        public int Amount { get; set; }

        [DataMember(Order = 1)]
        private int ID
        {
            get { return 1; }
            set { }
        }

        [DataMember(Order = 2)]
        internal string txnType
        {
            get { return ((int)TxnType).ToString(); }
            set { }
        }

        [DataMember(Order = 3)]
        internal string txnSource
        {
            get { return 23.ToString(); }
            set { }
        }

        [DataMember(Order = 4)]
        internal string amount
        {
            get { return Amount.ToString(); }
            set { Amount = int.Parse(value); }
        }

        public abstract object Clone();

        protected void CopyTo(Txn clone)
        {
            clone.Amount = Amount;
        }
    }

    [DataContract(Name = "TxnList", Namespace = "")]
    public class TxnList<TTxn>
        : ICloneable
        where TTxn : Txn, new()
    {
        public TxnList()
        {
            Txn = new TTxn();
        }

        [DataMember(Order = 1)]
        private int count
        {
            get { return 1; }
            set { }
        }

        [DataMember(Order = 2), Prepare, Validate]
        public TTxn Txn { get; set; }

        public object Clone()
        {
            var clone = new TxnList<TTxn>();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(TxnList<TTxn> txnList)
        {
            txnList.Txn = (TTxn)Txn.Clone();
        }
    }

    [DataContract(Name = "Payment", Namespace = "")]
    public class Payment<TTxn>
        : ICloneable
        where TTxn : Txn, new()
    {
        public Payment()
        {
            TxnList = new TxnList<TTxn>();
        }

        [DataMember(Order = 1), Prepare, Validate]
        public TxnList<TTxn> TxnList { get; set; }

        public object Clone()
        {
            var clone = new Payment<TTxn>();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(Payment<TTxn> payment)
        {
            payment.TxnList = (TxnList<TTxn>)TxnList.Clone();
        }
    }
}