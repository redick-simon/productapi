using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductWebApi.Models
{
    public interface IProductService
    {
        IList<Product> GetProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        void ModifyProduct(Product product);
        bool ProductExists(int id);
        IList<user> GetUsers();

    }
}
