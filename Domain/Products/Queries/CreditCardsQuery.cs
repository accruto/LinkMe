namespace LinkMe.Domain.Products.Queries
{
    public class CreditCardsQuery
        : ICreditCardsQuery
    {
        // Should go into configuration.

        private const decimal AmexSurcharge = (decimal)0.025;

        decimal ICreditCardsQuery.GetSurcharge(CreditCardType creditCardType)
        {
            switch (creditCardType)
            {
                case CreditCardType.Amex:
                    return AmexSurcharge;

                default:
                    return 0;
            }
        }
    }
}