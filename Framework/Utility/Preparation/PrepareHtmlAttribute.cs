namespace LinkMe.Framework.Utility.Preparation
{
    public class PrepareHtmlAttribute
        : PreparationAttribute
    {
        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (!(currentValue is string || currentValue is string[]))
                return false;

            var s = currentValue as string;
            if (s != null)
            {
                if (s.Length == 0)
                    return false;

                // Close any open HTML tags.

                value = HtmlUtil.CloseHtmlTags(s);
                return true;
            }

            var a = currentValue as string[];
            if (a.Length == 0)
                return false;

            var v = new string[a.Length];
            for (var index = 0; index < a.Length; ++index)
                v[index] = HtmlUtil.CloseHtmlTags(a[index]);

            value = v;
            return true;
        }
    }
}
