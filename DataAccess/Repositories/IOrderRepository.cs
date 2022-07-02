using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using BusinessObject.ResponseModels;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderResponseModel> GetOrders();
        OrderResponseModel GetById(int id);
        void Create(OrderRequestModel order);
        void Update(OrderRequestModel order);
        void Delete(int id);
        IEnumerable<StatisticsModel> GetSaleStatistics(DateTime startDate, DateTime endDate);
    }
}
