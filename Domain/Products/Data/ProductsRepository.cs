using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Products.Data
{
    public class ProductsRepository
        : Repository, IProductsRepository
    {
        private readonly ICreditsQuery _creditsQuery;

        private static readonly DataLoadOptions ProductLoadOptions = DataOptions.CreateLoadOptions<ProductEntity>(p => p.ProductCreditAdjustmentEntities);

        private static readonly Func<ProductsDataContext, Guid, bool, Product> GetProductQuery
            = CompiledQuery.Compile((ProductsDataContext dc, Guid id, bool includeDisabled)
                => (from p in dc.ProductEntities
                    where p.id == id
                    && (p.enabled || includeDisabled)
                    select p.Map()).SingleOrDefault());

        private static readonly Func<ProductsDataContext, string, bool, Product> GetProductByNameQuery
            = CompiledQuery.Compile((ProductsDataContext dc, string name, bool includeDisabled)
                => (from p in dc.ProductEntities
                    where p.name == name
                    && (p.enabled || includeDisabled)
                    select p.Map()).SingleOrDefault());

        private static readonly Func<ProductsDataContext, Guid, ProductEntity> GetProductEntityQuery
            = CompiledQuery.Compile((ProductsDataContext dc, Guid id)
                => (from p in dc.ProductEntities
                    where p.id == id
                    select p).SingleOrDefault());

        private static readonly Func<ProductsDataContext, bool, IQueryable<Product>> GetProductsQuery
            = CompiledQuery.Compile((ProductsDataContext dc, bool includeDisabled)
                => from p in dc.ProductEntities
                   where (p.enabled || includeDisabled)
                   orderby p.name
                   select p.Map());

        private static readonly Func<ProductsDataContext, UserType, IQueryable<Product>> GetProductsByUserTypeQuery
            = CompiledQuery.Compile((ProductsDataContext dc, UserType userType)
                => from p in dc.ProductEntities
                   where p.enabled
                   && (p.userTypes & (int)userType) == (int)userType
                   orderby p.name
                   select p.Map());

        public ProductsRepository(IDataContextFactory dataContextFactory, ICreditsQuery creditsQuery)
            : base(dataContextFactory)
        {
            _creditsQuery = creditsQuery;
        }

        void IProductsRepository.CreateProduct(Product product)
        {
            using (var dc = CreateContext())
            {
                dc.ProductEntities.InsertOnSubmit(product.Map());
                dc.SubmitChanges();
            }
        }

        void IProductsRepository.DeleteProduct(Guid id)
        {
            using (var dc = CreateContext())
            {
                var entity = GetProductEntity(dc, id);
                if (entity != null)
                {
                    dc.ProductCreditAdjustmentEntities.DeleteAllOnSubmit(from e in entity.ProductCreditAdjustmentEntities select e);
                    dc.ProductEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        Product IProductsRepository.GetProduct(Guid id, bool includeDisabled)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetProduct(GetProduct(dc, id, includeDisabled));
            }
        }

        Product IProductsRepository.GetProduct(string name, bool includeDisabled)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetProduct(GetProduct(dc, name, includeDisabled));
            }
        }

        IList<Product> IProductsRepository.GetProducts(bool includeDisabled)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from p in GetProducts(dc, includeDisabled) select GetProduct(p)).ToList();
            }
        }

        IList<Product> IProductsRepository.GetProducts(UserType userType)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from p in GetProductsByUserType(dc, userType) select GetProduct(p)).ToList();
            }
        }

        void IProductsRepository.EnableProduct(Guid id)
        {
            EnableProduct(id, true);
        }

        void IProductsRepository.DisableProduct(Guid id)
        {
            EnableProduct(id, false);
        }

        private void EnableProduct(Guid id, bool enabled)
        {
            using (var dc = CreateContext())
            {
                var entity = new ProductEntity {id = id, enabled = !enabled};
                dc.ProductEntities.Attach(entity);
                entity.enabled = enabled;
                dc.SubmitChanges();
            }
        }

        private class AdjustmentComparer
            : IComparer<ProductCreditAdjustment>
        {
            private readonly Guid _contactCreditId;
            private readonly Guid _applicantCreditId;

            public AdjustmentComparer(ICreditsQuery creditsQuery)
            {
                _contactCreditId = creditsQuery.GetCredit<ContactCredit>().Id;
                _applicantCreditId = creditsQuery.GetCredit<ApplicantCredit>().Id;
            }

            public int Compare(ProductCreditAdjustment x, ProductCreditAdjustment y)
            {
                if (x.CreditId == y.CreditId)
                {
                    if (x.Quantity == y.Quantity)
                        return 0;

                    if (y.Quantity == null)
                        return -1;

                    if (x.Quantity == null)
                        return 1;

                    return x.Quantity.Value < y.Quantity.Value ? -1 : 1;
                }

                if (x.CreditId == _contactCreditId)
                    return -1;
                if (y.CreditId == _contactCreditId)
                    return 1;

                if (x.CreditId == _applicantCreditId)
                    return -1;
                if (y.CreditId == _applicantCreditId)
                    return 1;

                return 0;
            }
        }

        private Product GetProduct(Product product)
        {
            if (product == null)
                return null;

            // Order the adjustments by credit type and then quantity.

            product.CreditAdjustments = product.CreditAdjustments.OrderBy(a => a, new AdjustmentComparer(_creditsQuery)).ToList();
            return product;
        }

        private static Product GetProduct(ProductsDataContext dc, Guid id, bool includeDisabled)
        {
            dc.LoadOptions = ProductLoadOptions;
            return GetProductQuery(dc, id, includeDisabled);
        }

        private static Product GetProduct(ProductsDataContext dc, string name, bool includeDisabled)
        {
            dc.LoadOptions = ProductLoadOptions;
            return GetProductByNameQuery(dc, name, includeDisabled);
        }

        private static ProductEntity GetProductEntity(ProductsDataContext dc, Guid id)
        {
            dc.LoadOptions = ProductLoadOptions;
            return GetProductEntityQuery(dc, id);
        }

        private static IQueryable<Product> GetProducts(ProductsDataContext dc, bool includeDisabled)
        {
            dc.LoadOptions = ProductLoadOptions;
            return GetProductsQuery(dc, includeDisabled);
        }

        private static IQueryable<Product> GetProductsByUserType(ProductsDataContext dc, UserType userType)
        {
            dc.LoadOptions = ProductLoadOptions;
            return GetProductsByUserTypeQuery(dc, userType);
        }

        private ProductsDataContext CreateContext()
        {
            return CreateContext(c => new ProductsDataContext(c));
        }
    }
}
