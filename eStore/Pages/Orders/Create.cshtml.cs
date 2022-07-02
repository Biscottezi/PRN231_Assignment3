using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Models;
using eStore.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace eStore.Pages.Orders
{
    [Authorize(Roles = "User,Admin")]
    public class CreateModel : PageModel
    {
        // private readonly DataAccess.Models.ProductStoreDbContext _context;
        //
        // public CreateModel(DataAccess.Models.ProductStoreDbContext context)
        // {
        //     _context = context;
        // }

        public IActionResult OnGet()
        {
            Cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "User")
            {
                ViewData.Add("IsUser", true);
                var idString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ViewData["UserId"] = idString;
            }
            return Page();
        }

        public IActionResult OnGetRemove(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = -1;
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == id)
                {
                    index = i;
                }
            }
            if (index != -1)
            {
                cart.Remove(cart[index]);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            Cart = cart;
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }
        
        [BindProperty]
        public List<CartItem> Cart { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        // public async Task<IActionResult> OnPostAsync()
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return Page();
        //     }
        //
        //     _context.Orders.Add(Order);
        //     await _context.SaveChangesAsync();
        //
        //     return RedirectToPage("./Index");
        // }
    }
}
