using System;
using HtmlAgilityPack;

namespace LinkMe.Framework.Utility.Preparation
{
    public class RemoveHtmlAttribute
        : PreparationAttribute
    {
        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (!(currentValue is string))
                return false;

            var s = (string) currentValue;
            if (s.Length == 0)
                return false;

            try
            {
                var document = new HtmlDocument();
                document.LoadHtml(s);
                value = document.DocumentNode.InnerText;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
