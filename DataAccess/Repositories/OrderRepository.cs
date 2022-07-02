using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using BusinessObject.ResponseModels;
using AutoMapper;
using DataAccess.DAOs;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper mapper;

        public OrderRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void Create(OrderRequestModel order)
        {
            var o = mapper.Map<Order>(order);
            OrderDAO.Instance.Create(o);
        }

        public void Delete(int id)
        {
            OrderDAO.Instance.Delete(id);
        }

        public OrderResponseModel GetById(int id)
        {
            var o = OrderDAO.Instance.GetById(id);
            var order = mapper.Map<OrderResponseModel>(o);
            return order;
        }

        public IEnumerable<OrderResponseModel> GetOrders()
        {
            var o = OrderDAO.Instance.GetOrderList();
            var orders = mapper.Map<IEnumerable<OrderResponseModel>>(o);
            return orders;
        }

        public void Update(OrderRequestModel order)
        {
            var o = mapper.Map<Order>(order);
            OrderDAO.Instance.Update(o);
        }

        public IEnumerable<StatisticsModel> GetSaleStatistics(DateTime startDate, DateTime endDate)
        {
            var sales = OrderDAO.Instance.GetSaleStatistics(startDate, endDate);
            return sales;
        }
    }
}
