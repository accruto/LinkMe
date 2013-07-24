namespace LinkMe.Apps.Services.External.Apple.AppStore
{
    public interface ISendAppleCommand
    {
        JsonVerificationResponse Verify(string encodedTransactionReceipt);
    }
}
