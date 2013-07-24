using System;

namespace LinkMe.Framework.Utility.Preparation
{
    public class DefaultNewGuidAttribute
        : PreparationAttribute
    {
        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (currentValue is Guid && (Guid)currentValue != Guid.Empty)
                return false;

            value = Guid.NewGuid();
            return true;
        }
    }
}