using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repo;

        public OrderController(IOrderRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string userId)
        {
            var orders = repo.GetOrders();
            if (userId != null)
            {
                orders = orders.Where(o => o.UserId == userId);
            }
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = repo.GetById(id);
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create([FromBody]OrderRequestModel model)
        {
            repo.Create(model);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]OrderRequestModel model)
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

        [HttpGet("statistics")]
        public IActionResult GetSaleStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var sales = repo.GetSaleStatistics(startDate, endDate);
            return Ok(sales);
        }
    }
}
