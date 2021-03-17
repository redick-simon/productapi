using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductWebApi.Models
{
    public class ProductService : IProductService
    {
        private ProductDbEntities entity;
        public ProductService()
        {
            entity = new ProductDbEntities();
        }

        public void AddProduct(Product product)
        {
            entity.Products.Add(product);
            entity.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return entity.Products.Find(id);
        }

        public IList<Product> GetProducts()
        {
            return entity.Products.ToList();
        }

        public bool ProductExists(int id)
        {
            return entity.Products.Any(prod => prod.Id == id);
        }

        public void DeleteProduct(Product product)
        {
            entity.Products.Remove(product);
            entity.SaveChanges();
        }

        public void ModifyProduct(Product product)
        {
            entity.Entry(product).State = EntityState.Modified;
            entity.SaveChanges();
        }

        public IList<user> GetUsers()
        {
            return entity.users.ToList();
        }        
    }
}