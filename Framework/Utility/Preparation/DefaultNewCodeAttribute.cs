using System;
using System.Security.Cryptography;

namespace LinkMe.Framework.Utility.Preparation
{
    public class DefaultNewCodeAttribute
        : PreparationAttribute
    {
        private readonly bool _useShortVersion;

        public DefaultNewCodeAttribute(bool useShortVersion)
        {
            _useShortVersion = useShortVersion;
        }

        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (currentValue is string && !string.IsNullOrEmpty((string)currentValue))
                return false;

            value = CreateCode();
            return true;
        }

        private string CreateCode()
        {
            var buffer = new byte[20];
            var generator = RandomNumberGenerator.Create();
            generator.GetNonZeroBytes(buffer);
            var uvalue = BitConverter.ToUInt64(buffer, 0);
            if (_useShortVersion)
            {
                var low = ulong.Parse(uvalue.ToString().Substring(0, 10));
                var high = ulong.Parse(uvalue.ToString().Substring(10));
                return (low ^ high).ToString("D10");
            }

            return uvalue.ToString("D20");
        }
    }
}