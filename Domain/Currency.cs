using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace LinkMe.Domain
{
    [Serializable]
    public class Currency
        : ISerializable, IObjectReference
    {
        private const int AUDIso4217Code = 36;
        private const int NZDIso4217Code = 554;
        private const int USDIso4217Code = 840;
        private const int GBPIso4217Code = 826;
//        private const int EURIso4217Code = 978;

        private static readonly IDictionary<int, Currency> _currenciesByIso4217Code = new Dictionary<int, Currency>
        {
            { AUDIso4217Code, new Currency("AUD", AUDIso4217Code, CultureInfo.CreateSpecificCulture("en-AU")) },
            { NZDIso4217Code, new Currency("NZD", NZDIso4217Code, CultureInfo.CreateSpecificCulture("en-NZ")) },
            { USDIso4217Code, new Currency("USD", USDIso4217Code, CultureInfo.CreateSpecificCulture("en-US")) },
            { GBPIso4217Code, new Currency("GBP", GBPIso4217Code, CultureInfo.CreateSpecificCulture("en-GB")) },
//            { EURIso4217Code, new Currency("EUR", EURIso4217Code, '\u20AC', CultureInfo.CreateSpecificCulture("en-AU")) },
        };

        private static readonly IDictionary<string, Currency> _currenciesByCode = _currenciesByIso4217Code.Values.ToDictionary(c => c.Code, c => c);

        public static readonly Currency AUD = _currenciesByIso4217Code[AUDIso4217Code];
        public static readonly Currency NZD = _currenciesByIso4217Code[NZDIso4217Code];
        public static readonly Currency USD = _currenciesByIso4217Code[USDIso4217Code];
        public static readonly Currency GBP = _currenciesByIso4217Code[GBPIso4217Code];
//        public static readonly Currency EUR = _currencies[EURIso4217Code];
        
        private readonly string _code;
        private readonly int _iso4217Code;
        private readonly CultureInfo _cultureInfo;

        private Currency(string code, int iso4217Code, CultureInfo cultureInfo)
        {
            _code = code;
            _iso4217Code = iso4217Code;
            _cultureInfo = cultureInfo;
        }

        #region Serialization

        protected Currency(SerializationInfo info, StreamingContext context)
        {
            _iso4217Code = info.GetInt32("iso4217Code");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("iso4217Code", _iso4217Code);
        }

        object IObjectReference.GetRealObject(StreamingContext context)
        {
            return GetCurrency(_iso4217Code);
        }

        #endregion

        public string Code
        {
            get { return _code; }
        }

        public int Iso4217Code
        {
            get { return _iso4217Code; }
        }

        public CultureInfo CultureInfo
        {
            get { return _cultureInfo; }
        }

        public override string ToString()
        {
            return _code;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Currency)) return false;
            return Equals((Currency) obj);
        }

        public bool Equals(Currency other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._code, _code);
        }

        public override int GetHashCode()
        {
            return (_code != null ? _code.GetHashCode() : 0);
        }

        public static Currency GetCurrency(string code)
        {
            Currency currency;
            _currenciesByCode.TryGetValue(code, out currency);
            return currency;
        }

        public static Currency GetCurrency(int iso4217Code)
        {
            Currency currency;
            _currenciesByIso4217Code.TryGetValue(iso4217Code, out currency);
            return currency;
        }
    }
}
