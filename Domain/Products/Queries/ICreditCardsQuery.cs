namespace LinkMe.Domain.Products.Queries
{
    public interface ICreditCardsQuery
    {
        decimal GetSurcharge(CreditCardType creditCardType);
    }
}