using System;
using System.Collections.Generic;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.Orders.Queries
{
    public enum JobAdFeaturePack
    {
        BaseFeaturePack,
        FeaturePack1,
        FeaturePack2,
    }

    public interface IEmployerOrdersQuery
    {
        IList<Product> GetContactProducts();
        Guid GetDefaultContactProductId();
        IList<Product> GetApplicantProducts();
        Guid GetDefaultApplicantProductId();
        IList<Product> GetJobAdProducts();
        Guid GetDefaultJobAdProductId();

        Product GetJobAdFeaturePackProduct(JobAdFeaturePack featurePack);
        JobAdFeatures GetJobAdFeatures(JobAdFeaturePack featurePack);
        JobAdFeatureBoost GetJobAdFeatureBoost(JobAdFeaturePack featurePack);
    }
}
