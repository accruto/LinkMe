using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Products
{
    public interface IProductsRepository
    {
        void CreateProduct(Product product);
        void DeleteProduct(Guid id);
        Product GetProduct(Guid id, bool includeDisabled);
        Product GetProduct(string name, bool includeDisabled);
        IList<Product> GetProducts(bool includeDisabled);
        IList<Product> GetProducts(UserType userType);
        void EnableProduct(Guid id);
        void DisableProduct(Guid id);
    }
}
