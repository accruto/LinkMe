using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.JobAds;
using org.apache.lucene.document;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine.JobAds.Search
{
    public interface IJobAdSearchBooster
        : IBooster
    {
        void SetFeatureBoost(Document document, JobAdFeatureBoost boost);
        void SetSalaryBoost(Document document, bool hasSalary);
        LuceneQuery GetRecencyBoostingQuery(LuceneQuery query);
    }

    public class JobAdSearchBooster
        : Booster, IJobAdSearchBooster
    {
        public JobAdSearchBooster()
        {
            // Defaults.

            TitleBoost = 1.5F;
            ContentBoost = 1.5F;
            BulletPointsBoost = 1.5F;
            AdvertiserNameBoost = 1.5F;
            HighFeaturedBoost = 1.5F;
            LowFeaturedBoost = 1.2F;
            SalaryBoost = 1.4F;
            RecencyAlpha = 0.01;
            RecencyHalfDecay = 20 * 24;

            SetFieldBoosts(new Dictionary<string, Func<float?>>
                {
                    { FieldName.Title, () => TitleBoost },
                    { FieldName.TitleExact, () => TitleBoost },
                    { FieldName.Content, () => ContentBoost },
                    { FieldName.ContentExact, () => ContentBoost },
                    { FieldName.BulletPoints, () => BulletPointsBoost },
                    { FieldName.BulletPointsExact, () => BulletPointsBoost },
                    { FieldName.AdvertiserName, () => AdvertiserNameBoost },
                    { FieldName.AdvertiserNameExact, () => AdvertiserNameBoost }
                });
        }

        void IBooster.SetBoost(AbstractField field)
        {
            SetBoost(field);
        }

        void IJobAdSearchBooster.SetFeatureBoost(Document document, JobAdFeatureBoost boost)
        {
            switch (boost)
            {
                case JobAdFeatureBoost.High:
                    SetBoost(document, HighFeaturedBoost);
                    break;

                case JobAdFeatureBoost.Low:
                    SetBoost(document, LowFeaturedBoost);
                    break;
            }
        }

        void IJobAdSearchBooster.SetSalaryBoost(Document document, bool hasSalary)
        {
            if (hasSalary)
                SetBoost(document, SalaryBoost);
        }

        LuceneQuery IJobAdSearchBooster.GetRecencyBoostingQuery(LuceneQuery query)
        {
            return GetRecencyBoostingQuery(query, FieldName.CreatedTime, TimeGranularity.Hour, RecencyAlpha, RecencyHalfDecay);
        }

        public float? TitleBoost { get; set; }
        public float? ContentBoost { get; set; }
        public float? BulletPointsBoost { get; set; }
        public float? AdvertiserNameBoost { get; set; }
        public float? HighFeaturedBoost { get; set; }
        public float? LowFeaturedBoost { get; set; }
        public float? SalaryBoost { get; set; }
        public double? RecencyAlpha { get; set; }
        public double? RecencyHalfDecay { get; set; }
    }
}
