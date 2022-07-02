using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using eStore.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace eStore.Pages.Products
{
    [Authorize(Roles = "User,Admin")]
    public class IndexModel : PageModel
    {
        //private readonly DataAccess.Models.ProductStoreDbContext _context;

        //public IndexModel(DataAccess.Models.ProductStoreDbContext context)
        //{
        //    _context = context;
        //}

        //public IList<Product> Product { get;set; }

        public IActionResult OnGet()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "User")
            {
                ViewData.Add("IsUser", true);
                var idString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ViewData["UserId"] = idString;
            }
            return Page();
        }

        public IActionResult OnGetBuy(int id, string name, int price)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
                cart.Add(new CartItem()
                {
                    ProductId = id,
                    ProductName = name,
                    Quantity = 1,
                    UnitPrice = price,
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = -1;
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].ProductId.Equals(id))
                    {
                        index = i;
                    }
                }
                if (index == -1)
                {
                    cart.Add(new CartItem()
                    {
                        ProductId = id,
                        ProductName = name,
                        Quantity = 1,
                        UnitPrice = price,
                    });
                }
                else
                {
                    var newQuantity = cart[index].Quantity + 1;
                    cart[index].Quantity = newQuantity;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return OnGet();
        }
    }
}
