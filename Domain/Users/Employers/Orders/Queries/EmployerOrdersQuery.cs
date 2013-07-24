using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.Orders.Queries
{
    public class EmployerOrdersQuery
        : IEmployerOrdersQuery
    {
        private readonly ICreditsQuery _creditsQuery;
        private readonly IProductsQuery _productsQuery;

        public EmployerOrdersQuery(IProductsQuery productsQuery, ICreditsQuery creditsQuery)
        {
            _productsQuery = productsQuery;
            _creditsQuery = creditsQuery;
        }

        IList<Product> IEmployerOrdersQuery.GetContactProducts()
        {
            return GetContactProducts();
        }

        Guid IEmployerOrdersQuery.GetDefaultContactProductId()
        {
            // Product with smallest amount of credits.

            return (from p in GetContactProducts()
                    orderby p.CreditAdjustments[0].Quantity
                    select p.Id).First();
        }

        IList<Product> IEmployerOrdersQuery.GetApplicantProducts()
        {
            return GetApplicantProducts();
        }

        Guid IEmployerOrdersQuery.GetDefaultApplicantProductId()
        {
            // Product with smallest amount of applicant credits.

            var credit = _creditsQuery.GetCredit<ApplicantCredit>();
            return (from p in GetApplicantProducts()
                    orderby GetCredits(p, credit.Id)
                    select p.Id).First();
        }

        IList<Product> IEmployerOrdersQuery.GetJobAdProducts()
        {
            return GetJobAdProducts();
        }

        Guid IEmployerOrdersQuery.GetDefaultJobAdProductId()
        {
            // Product with smallest amount of job ad credits.

            var credit = _creditsQuery.GetCredit<JobAdCredit>();
            return (from p in GetJobAdProducts()
                    orderby GetCredits(p, credit.Id)
                    select p.Id).First();
        }

        Product IEmployerOrdersQuery.GetJobAdFeaturePackProduct(JobAdFeaturePack featurePack)
        {
            // Get all products for employers.

            var products = _productsQuery.GetProducts(UserType.Employer);

            return (from p in products
                    where p.Name == featurePack.ToString()
                    select p).SingleOrDefault();
        }

        JobAdFeatures IEmployerOrdersQuery.GetJobAdFeatures(JobAdFeaturePack featurePack)
        {
            switch (featurePack)
            {
                case JobAdFeaturePack.FeaturePack2:
                    return JobAdFeatures.Logo | JobAdFeatures.ExtendedExpiry | JobAdFeatures.Refresh | JobAdFeatures.Highlight;

                case JobAdFeaturePack.FeaturePack1:
                    return JobAdFeatures.Logo | JobAdFeatures.ExtendedExpiry;

                default:
                    return JobAdFeatures.None;
            }
        }

        JobAdFeatureBoost IEmployerOrdersQuery.GetJobAdFeatureBoost(JobAdFeaturePack featurePack)
        {
            switch (featurePack)
            {
                case JobAdFeaturePack.FeaturePack2:
                    return JobAdFeatureBoost.High;

                case JobAdFeaturePack.FeaturePack1:
                    return JobAdFeatureBoost.Low;

                default:
                    return JobAdFeatureBoost.None;
            }
        }

        private IList<Product> GetContactProducts()
        {
            // Get all products for employers.

            var products = _productsQuery.GetProducts(UserType.Employer);

            // Select all products which have only a single contact credit limited adjustment.

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            return (from p in products
                    let contactCredits = GetCredits(p, credit.Id) 
                    where p.CreditAdjustments.Count == 1
                    && contactCredits != null && contactCredits.Value != 0
                    orderby p.Price
                    select p).ToList();
        }

        private static int? GetCredits(Product product, Guid creditId)
        {
            var adjustment = (from a in product.CreditAdjustments where a.CreditId == creditId select a).SingleOrDefault();
            return adjustment == null ? 0 : adjustment.Quantity;
        }

        private IList<Product> GetApplicantProducts()
        {
            // Get all products for employers.

            var products = _productsQuery.GetProducts(UserType.Employer);

            // From those products select those that have unlimited job ad credits but limited applicants.

            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            return (from p in products
                    let applicantCredits = GetCredits(p, applicantCredit.Id)
                    let jobAdCredits = GetCredits(p, jobAdCredit.Id)
                    where p.CreditAdjustments.Count == 2
                    && jobAdCredits == null
                    && applicantCredits != null && applicantCredits.Value != 0
                    orderby p.Price
                    select p).ToList();
        }

        private IList<Product> GetJobAdProducts()
        {
            // Get all products for employers.

            var products = _productsQuery.GetProducts(UserType.Employer);

            // From those products select those that have unlimited applicant credits but limited job ads.

            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            return (from p in products
                    let applicantCredits = GetCredits(p, applicantCredit.Id)
                    let jobAdCredits = GetCredits(p, jobAdCredit.Id)
                    where p.CreditAdjustments.Count == 2
                    && jobAdCredits != null && jobAdCredits.Value != 0
                    && applicantCredits == null
                    orderby p.Price
                    select p).ToList();
        }
    }
}
