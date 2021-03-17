using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsTest
{
    public class MockService : IProductService
    {
        private IList<Product> _products;
        private IList<user> _users; 
        public MockService()
        {
            _products = new List<Product> 
            { 
                new Product { Id = 1, Name = "ABC", Description = "xyz", Price =90.0m},
                new Product { Id = 2, Name = "ABC1", Description = "xyz1", Price =890},
                new Product { Id = 3, Name = "ABC2", Description = "xyz2", Price =89.00m},
                new Product { Id = 4, Name = "ABC3", Description = "xyz3", Price =null},
            };
            _users = new List<user>
            {
                new user { username = "test", password = "1234"}
            };
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _products.Remove(product);
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(prod => prod.Id == id);
        }

        public IList<Product> GetProducts()
        {
            return _products;
        }

        public IList<user> GetUsers()
        {
            return _users;
        }

        public void ModifyProduct(Product product)
        {
            var productFound = _products.FirstOrDefault(prod => prod.Id == product.Id);

            if(productFound != null)
            {
                productFound.Name = product.Name;
                productFound.Description = product.Description;
                productFound.Price = product.Price;
            }            
        }

        public bool ProductExists(int id)
        {
            return _products.Any(prod => prod.Id == id);
        }
    }
}
