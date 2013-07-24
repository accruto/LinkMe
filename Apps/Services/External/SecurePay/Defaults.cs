using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class DefaultNewMessageIdAttribute
        : PreparationAttribute
    {
        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (currentValue != null)
                return false;

            value = MessageId.NewMessageId();
            return true;
        }
    }

    public class DefaultNowMessageTimestampAttribute
        : PreparationAttribute
    {
        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (currentValue != null)
                return false;

            value = new MessageTimestamp(DateTime.Now);
            return true;
        }
    }
}
