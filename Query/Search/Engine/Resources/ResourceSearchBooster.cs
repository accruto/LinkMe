using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine.Resources
{
    public interface IResourceSearchBooster
        : IBooster
    {
        void SetHelpfulBoost(Document document, int helpful);
    }

    public class ResourceSearchBooster
        : Booster, IResourceSearchBooster
    {
        public ResourceSearchBooster()
        {
            // Defaults.

            HelpfulBoostFactor = 0.01F;
        }

        void IBooster.SetBoost(AbstractField field)
        {
        }

        void IResourceSearchBooster.SetHelpfulBoost(Document document, int helpful)
        {
            if (HelpfulBoostFactor != null)
                SetBoost(document, 1F + (helpful * HelpfulBoostFactor.Value));
        }

        public float? HelpfulBoostFactor { get; set; }
    }
}
