using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class SecurePayStatusException
        : PurchaseSystemException
    {
        private readonly int _code;

        public SecurePayStatusException(int code, string description)
            : base(description)
        {
            _code = code;
        }

        public int Code
        {
            get { return _code; }
        }
    }

    public class SecurePayNotApprovedException
        : PurchaseUserException
    {
        private readonly string _code;

        public SecurePayNotApprovedException(string code, string text)
            : base(text)
        {
            _code = code;
        }

        public string Code
        {
            get { return _code; }
        }
    }

    public class SecurePayFraudException
        : PurchaseUserException
    {
        private readonly string _code;
        private readonly FraudGuard _fraudGuard;

        public SecurePayFraudException(string code, string text, FraudGuard fraudGuard)
            : base(text)
        {
            _code = code;
            _fraudGuard = fraudGuard;
        }

        public string Code
        {
            get { return _code; }
        }

        public FraudGuard FraudGuard
        {
            get { return _fraudGuard; }
        }
    }
}
