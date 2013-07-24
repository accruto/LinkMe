using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Roles.Orders
{
    public abstract class CouponException
        : UserException
    {
    }

    public class CouponNotFoundException
        : CouponException
    {
    }

    public class CouponExpiredException
        : CouponException
    {
    }

    public class CouponRedeemerException
        : CouponException
    {
    }

    public class CouponProductException
        : CouponException
    {
    }
}
