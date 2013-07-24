using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Products.Queries
{
    public interface IProductsQuery
    {
        Product GetProduct(Guid id);
        Product GetProduct(string name);
        IList<Product> GetProducts();
        IList<Product> GetProducts(UserType userType);
    }
}