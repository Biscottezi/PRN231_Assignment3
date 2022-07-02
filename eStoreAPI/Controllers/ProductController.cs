using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace eStoreAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repo;

        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            var products = repo.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = repo.GetById(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProductRequestModel model)
        {
            repo.Create(model);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]ProductRequestModel model)
        {
            repo.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repo.Delete(id);
            return Ok();
        }
    }
}
