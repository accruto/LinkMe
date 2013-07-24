using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Products.Commands
{
    public class ProductsCommand
        : IProductsCommand
    {
        private readonly IProductsRepository _repository;

        public ProductsCommand(IProductsRepository repository)
        {
            _repository = repository;
        }

        void IProductsCommand.CreateProduct(Product product)
        {
            // Prepare.

            product.Prepare();
            product.Validate();

            // Create.

            _repository.CreateProduct(product);
        }

        void IProductsCommand.EnableProduct(Guid id)
        {
            _repository.EnableProduct(id);
        }

        void IProductsCommand.DisableProduct(Guid id)
        {
            _repository.DisableProduct(id);
        }

        Product IProductsCommand.GetProduct(Guid id)
        {
            return _repository.GetProduct(id, true);
        }

        Product IProductsCommand.GetProduct(string name)
        {
            return _repository.GetProduct(name, true);
        }

        IList<Product> IProductsCommand.GetProducts()
        {
            return _repository.GetProducts(true);
        }
    }
}