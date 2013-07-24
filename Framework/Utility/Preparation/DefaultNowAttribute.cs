using System;

namespace LinkMe.Framework.Utility.Preparation
{
    public class DefaultNowAttribute
        : PreparationAttribute
    {
        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (currentValue is DateTime && (DateTime)currentValue != DateTime.MinValue)
                return false;

            value = DateTime.Now;
            return true;
        }
    }
}