using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.ResponseModels;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class OrderDAO
    {
        private static OrderDAO _instance;
        private static readonly object InstanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if(_instance == null)
                    {
                        _instance = new OrderDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<Order> GetOrderList()
        {
            List<Order> orders = null;
            try
            {
                var db = new ProductStoreDbContext();
                orders = db.Orders.Include(o => o.OrderDetails)
                                .ThenInclude(d => d.Product)
                                .Include(o => o.User)
                                .ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - GetOrderList - OrderDAO -> " + e.Message);
            }
            return orders;
        }

        public Order GetById(int id)
        {
            Order order = null;
            try
            {
                var db = new ProductStoreDbContext();
                order = db.Orders.Include(o => o.OrderDetails)
                                .ThenInclude(d => d.Product)
                                .Include(o => o.User)
                                .FirstOrDefault(o => o.OrderId == id); 
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - GetById - OrderDAO -> " + e.Message);
            }
            return order;
        }

        public void Create(Order order)
        {
            try
            {
                var db = new ProductStoreDbContext();
                db.Add(order);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - CreateOrder - OrderDAO -> " + e.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                var o = GetById(order.OrderId);
                if (o == null)
                {
                    throw new Exception("Order not found");
                }
                else
                {
                    var db = new ProductStoreDbContext();
                    db.Entry<Order>(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error - UpdateOrder - OrderDAO -> " + e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var o = GetById(id);
                if (o == null)
                {
                    throw new Exception("Order not found");
                }
                else
                {
                    var db = new ProductStoreDbContext();
                    db.Remove(o);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error - DeleteOrder - OrderDAO -> " + e.Message);
            }
        }

        public IEnumerable<StatisticsModel> GetSaleStatistics(DateTime startDate, DateTime endDate)
        {
            List<StatisticsModel> sales = new List<StatisticsModel>();
            var db = new ProductStoreDbContext();
            try
            {
                var orders = db.Orders.Where(o => o.OrderedDate > startDate && o.OrderedDate < endDate)
                    .Include(o => o.OrderDetails)
                    .OrderByDescending(o => o.OrderedDate)
                    .ToList();
                foreach (Order order in orders)
                {
                    var total = order.OrderDetails.Sum(orderDetail => orderDetail.UnitPrice * orderDetail.Quantity);
                    StatisticsModel salesItem = new()
                    {
                        Date = order.OrderedDate,
                        Total = total
                    };
                    sales.Add(salesItem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error - GetSaleStatistics - OrderDAO -> " + e.Message);
            }

            return sales;
        }
    }
}
