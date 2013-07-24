using System;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.Views.Shared
{
    public class CreditCard
        : ViewUserControl<LinkMe.Domain.Products.CreditCard>
    {
        private const int ExpiryYearsCount = 20;
        private static readonly string[] _expiryMonths;
        private static readonly string[] _expiryYears;
        private static readonly decimal _amexSurcharge;

        static CreditCard()
        {
            var currentYear = DateTime.Now.Year;
            _expiryYears = (from y in Enumerable.Range(currentYear, ExpiryYearsCount) select (y % 100).ToString("D2")).ToArray();
            _expiryMonths = (from m in Enumerable.Range(1, 12) select m.ToString("D2")).ToArray();

            _amexSurcharge = Container.Current.Resolve<ICreditCardsQuery>().GetSurcharge(CreditCardType.Amex) * 100;
        }

        public string[] ExpiryMonths
        {
            get { return _expiryMonths; }
        }

        public string[] ExpiryYears
        {
            get { return _expiryYears; }
        }

        public decimal AmexSurcharge
        {
            get { return _amexSurcharge; }
        }
    }
}
