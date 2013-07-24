using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine
{
    public interface IBooster
    {
        void SetBoost(AbstractField field);
    }
}
