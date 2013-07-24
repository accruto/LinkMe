namespace LinkMe.Domain.Spam.Queries
{
    public interface ISpamQuery
    {
        bool IsSpam(string text);
        bool IsSpammer(Spammer possibleSpammer);
    }
}