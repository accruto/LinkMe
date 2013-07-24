using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Products.Queries
{
    public class ProductsQuery
        : IProductsQuery
    {
        private readonly IProductsRepository _repository;

        public ProductsQuery(IProductsRepository repository)
        {
            _repository = repository;
        }

        Product IProductsQuery.GetProduct(Guid id)
        {
            return _repository.GetProduct(id, true);
        }

        Product IProductsQuery.GetProduct(string name)
        {
            return _repository.GetProduct(name, false);
        }

        IList<Product> IProductsQuery.GetProducts()
        {
            return _repository.GetProducts(false);
        }

        IList<Product> IProductsQuery.GetProducts(UserType userType)
        {
            return _repository.GetProducts(userType);
        }
    }
}