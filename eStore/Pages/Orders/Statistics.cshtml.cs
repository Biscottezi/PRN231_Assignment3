using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Orders
{
    public class Statistics : PageModel
    {
        [BindProperty]
        [Required]
        public DateTime startDate { get; set; }

        [BindProperty]
        [Required]
        public DateTime endDate { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            // DateTime minDate = new DateTime(1900, 1, 1);
            // Console.WriteLine(startDate);
            // if (startDate < minDate || endDate < minDate)
            // {
            //     ViewData["Message"] = string.Format("Please pick a proper date!");
            // } else
            // {
            //     orders = _db.Orders.Where(o => o.OrderDate > startDate && o.OrderDate < endDate)
            //         .Include(o => o.OrderDetails)
            //         .OrderByDescending(o => o.OrderDate)
            //         .ToList();
            //     foreach (Order order in orders)
            //     {
            //         decimal total = order.OrderDetails.Sum(orderDetail => orderDetail.UnitPrice * orderDetail.Quantity);
            //         SaleStatisticByDay salesItem = new()
            //         {
            //             Date = order.OrderDate,
            //             Total = total
            //         };
            //         sales.Add(salesItem);
            //     }
            // }
        }
    }
}