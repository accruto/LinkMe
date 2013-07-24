using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Products.Commands
{
    public interface IProductsCommand
    {
        void CreateProduct(Product product);
        void EnableProduct(Guid id);
        void DisableProduct(Guid id);

        // This will return all products, whether they are enabled or not.  IProductsQuery only returns enabled products.

        Product GetProduct(Guid id);
        Product GetProduct(string name);
        IList<Product> GetProducts();
    }
}