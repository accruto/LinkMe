using System;

namespace LinkMe.Framework.Utility.Preparation
{
    public class TrimAttribute
        : PreparationAttribute
    {
        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (!(currentValue is string))
                return false;

            value = ((string) currentValue).Trim();
            return true;
        }
    }
}
