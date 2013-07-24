namespace LinkMe.Apps.Services.External.SecurePay
{
    public interface ISendSecurePayCommand
    {
        EchoResponseMessage Send(EchoRequestMessage requestMessage);

        PaymentResponseMessage<TResponseTxn> Send<TRequestTxn, TResponseTxn>(PaymentRequestMessage<TRequestTxn> requestMessage)
            where TResponseTxn : PaymentResponseTxn, new()
            where TRequestTxn : PaymentRequestTxn, new();
    }
}
