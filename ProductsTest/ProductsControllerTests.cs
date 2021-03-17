using NUnit.Framework;
using ProductWebApi.Controllers;
using ProductWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace ProductsTest
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private ProductsController _productsController;
        private IProductService _mockService;
        [SetUp]
        public void Setup()
        {
            _mockService = new MockService();
            _productsController = new ProductsController(_mockService);
        }

        [Test]
        public void TestGetProducts()
        {
            var products = _productsController.GetProducts();

            Assert.IsNotNull(products.Result);

            var result = (OkNegotiatedContentResult<IList<Product>>)products.Result;

            Assert.IsNotNull(result);

            IList<Product> actualProducts = (IList<Product>)result.Content;

            Assert.IsNotNull(actualProducts);
            Assert.AreEqual(4, actualProducts.Count);
        }

        [Test]
        public void TestGetProductById()
        {
            var product = _productsController.GetProduct(2);

            var result = (OkNegotiatedContentResult<Product>)product.Result;

            Assert.IsNotNull(result);

            Product actualProduct = (Product)result.Content;

            Assert.IsNotNull(actualProduct);
            Assert.AreEqual(2, actualProduct.Id);
            Assert.AreEqual("ABC1", actualProduct.Name);
            Assert.AreEqual("xyz1", actualProduct.Description);
            Assert.AreEqual(890, actualProduct.Price);
        }

        [Test]
        public void TestDeleteProduct()
        {
            var httpResult = _productsController.DeleteProduct(1);

            Assert.IsNotNull(httpResult);

            Assert.False(_mockService.ProductExists(1));
        }

        [Test]
        public async Task TestPostProduct()
        {
            Assert.False(_mockService.ProductExists(5));

            var product = new Product { Id = 5, Name = "test", Description = "test desc", Price = 100.90m };
            var httpResult = await _productsController.PostProduct(product);

            Assert.IsNotNull(httpResult);

            Assert.True(_mockService.ProductExists(5));
        }

        [Test]
        public async Task TestPutProduct()
        {
            var product = new Product { Id = 1, Name = "test", Description = "test desc", Price = 100.90m };

            var httpResult = await _productsController.PutProduct(1, product);

            Assert.IsNotNull(httpResult);

            Assert.AreEqual("test", _mockService.GetProductById(1).Name);
            Assert.AreEqual("test desc", _mockService.GetProductById(1).Description);
            Assert.AreEqual(100.90m, _mockService.GetProductById(1).Price);
        }
    }
}