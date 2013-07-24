using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Commands;
using LinkMe.Domain.Products.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Products
{
    [TestClass]
    public class CreateProductsTests
        : TestClass
    {
        private const string ProductNameFormat = "Product{0}";
        private readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();
        private readonly IProductsCommand _productsCommand = Resolve<IProductsCommand>();
        private readonly IProductsRepository _productsRepository = Resolve<IProductsRepository>();

        [TestMethod]
        public void TestCreateProductNoAdjustments()
        {
            var product = CreateProduct(1, UserType.Member, 1, null);
            AssertProducts(_productsQuery.GetProducts(), product);
            AssertProducts(_productsQuery.GetProducts(UserType.Member), product);
        }

        [TestMethod]
        public void TestCreateProductNoAdjustmentsWithDuration()
        {
            var product = CreateProduct(1, UserType.Member, 1);
            AssertProducts(_productsQuery.GetProducts(), product);
            AssertProducts(_productsQuery.GetProducts(UserType.Member), product);
        }

        [TestMethod]
        public void TestCreateProductDisabled()
        {
            CreateProduct(1, false, UserType.Member);
            AssertProducts(_productsQuery.GetProducts());
            AssertProducts(_productsQuery.GetProducts(UserType.Member));
        }

        [TestMethod]
        public void TestCreateProduct()
        {
            var product = CreateProduct(1, UserType.Member, 1, new ProductCreditAdjustment{CreditId = Guid.NewGuid(), Quantity = 10});
            AssertProducts(_productsQuery.GetProducts(), product);
            AssertProducts(_productsQuery.GetProducts(UserType.Member), product);
        }

        [TestMethod]
        public void TestCreateProductMultipleAdjustments()
        {
            var product = CreateProduct(1, UserType.Member, 1, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 10 }, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = null }, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 40 });
            AssertProducts(_productsQuery.GetProducts(), product);
            AssertProducts(_productsQuery.GetProducts(UserType.Member), product);
        }

        [TestMethod]
        public void TestCreateMultipleProducts()
        {
            var product1 = CreateProduct(1, UserType.Member, 1, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 10 });
            var product2 = CreateProduct(2, UserType.Member, 2, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 20 });
            var product3 = CreateProduct(3, UserType.Member, 3, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 30 });
            AssertProducts(_productsQuery.GetProducts(), product1, product2, product3);
            AssertProducts(_productsQuery.GetProducts(UserType.Member), product1, product2, product3);
        }

        [TestMethod]
        public void TestCreateProductsDifferentUserTypes()
        {
            var product1 = CreateProduct(1, UserType.Member, 1, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 10 });
            var product2 = CreateProduct(2, UserType.Employer, 2, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 20 });
            var product3 = CreateProduct(3, UserType.Member, 3, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 30 });
            var product4 = CreateProduct(4, UserType.Member, 4, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 40 });
            var product5 = CreateProduct(5, UserType.Employer, 5, new ProductCreditAdjustment { CreditId = Guid.NewGuid(), Quantity = 50 });
            AssertProducts(_productsQuery.GetProducts(), product1, product2, product3, product4, product5);
            AssertProducts(_productsQuery.GetProducts(UserType.Member), product1, product3, product4);
            AssertProducts(_productsQuery.GetProducts(UserType.Employer), product2, product5);
            AssertProducts(_productsQuery.GetProducts(UserType.Administrator));
        }

        private Product CreateProduct(int index, UserType userType, decimal price, params ProductCreditAdjustment[] adjustments)
        {
            DeleteProduct(index);

            var product = new Product
            {
                Name = string.Format(ProductNameFormat, index),
                Description = string.Format(ProductNameFormat, index),
                UserTypes = userType,
                Price = price,
                Currency = Currency.AUD,
                CreditAdjustments = adjustments == null || adjustments.Length == 0 ? null : adjustments.ToList(),
                IsEnabled = true,
            };
            _productsCommand.CreateProduct(product);
            return product;
        }

        private void CreateProduct(int index, bool isEnabled, UserType userType)
        {
            DeleteProduct(index);

            var product = new Product
            {
                Name = string.Format(ProductNameFormat, index),
                Description = string.Format(ProductNameFormat, index),
                IsEnabled = isEnabled,
                UserTypes = userType,
                Currency = Currency.AUD
            };
            _productsCommand.CreateProduct(product);
        }

        private void DeleteProduct(int index)
        {
            // If it already exists then delete it.

            var name = string.Format(ProductNameFormat, index);
            var product = _productsQuery.GetProduct(name);
            if (product != null)
              _productsRepository.DeleteProduct(product.Id);
        }

        private static void AssertProducts(IEnumerable<Product> products, params Product[] expectedProducts)
        {
            // The expected products should be in there somewhere.

            foreach (var expectedProduct in expectedProducts)
            {
                var expectedName = expectedProduct.Name;
                var product = (from p in products where p.Name == expectedName select p).Single();
                AssertProducts(expectedProduct, product);
            }
        }

        private static void AssertProducts(Product expectedProduct, Product product)
        {
            Assert.AreEqual(expectedProduct.Id, product.Id);
            Assert.AreEqual(expectedProduct.Name, product.Name);
            Assert.AreEqual(expectedProduct.IsEnabled, product.IsEnabled);
            Assert.AreEqual(expectedProduct.UserTypes, product.UserTypes);
            Assert.AreEqual(expectedProduct.Price, product.Price);
            expectedProduct.CreditAdjustments.NullableCollectionEqual(product.CreditAdjustments, Equals);
        }

        private static bool Equals(ProductCreditAdjustment a1, ProductCreditAdjustment a2)
        {
            return a1.CreditId == a2.CreditId
                   && Equals(a1.Quantity, a2.Quantity)
                   && Equals(a1.Duration, a2.Duration);
        }
    }
}
