using System.Text;

namespace LinkMe.Apps.Presentation.Converters
{
    public abstract class BaseAdSenseQueryGenerator
    {
        public abstract void GenerateAdSenseQuery(object model, StringBuilder sb);

        public string GenerateAdSenseQuery(object model)
        {
            var sb = new StringBuilder();
            GenerateAdSenseQuery(model, sb);
            return sb.ToString();
        }
    }

    public class AdSenseQueryGenerator
        : BaseAdSenseQueryGenerator
    {
        private readonly IConverter _converter;

        public AdSenseQueryGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public override void GenerateAdSenseQuery(object obj, StringBuilder sb)
        {
            _converter.Convert(obj, new AdSenseQuerySetValues(sb));
        }
    }
}