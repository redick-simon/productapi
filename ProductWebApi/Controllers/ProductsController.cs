using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProductWebApi.Authentication;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/Products
        public async Task<IHttpActionResult> GetProducts()
        {
            var products = await Task.FromResult(_service.GetProducts());

            return Ok(products);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        [BasicAuthentication]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await Task.FromResult(_service.GetProductById(id));
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        [BasicAuthentication]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            if (!_service.ProductExists(id))
            {
                return NotFound();
            }

            await Task.Run(() => _service.ModifyProduct(product));

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        [BasicAuthentication]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Task.Run(() => _service.AddProduct(product));

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        [BasicAuthentication]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            if (!_service.ProductExists(id))
            {
                return NotFound();
            }

            var product = _service.GetProductById(id);

            await Task.Run(() => _service.DeleteProduct(product));

            return Ok(product);
        }
    }
}