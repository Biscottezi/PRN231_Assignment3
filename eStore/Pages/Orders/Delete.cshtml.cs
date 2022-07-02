using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace eStore.Pages.Orders
{
    [Authorize(Roles = "User,Admin")]
    public class DeleteModel : PageModel
    {
        // private readonly DataAccess.Models.ProductStoreDbContext _context;
        //
        // public DeleteModel(DataAccess.Models.ProductStoreDbContext context)
        // {
        //     _context = context;
        // }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["OrderId"] = id;
            // Order = await _context.Orders
            //     .Include(o => o.User).FirstOrDefaultAsync(m => m.OrderId == id);
            //
            // if (Order == null)
            // {
            //     return NotFound();
            // }
            return Page();
        }

        // public async Task<IActionResult> OnPostAsync(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     Order = await _context.Orders.FindAsync(id);
        //
        //     if (Order != null)
        //     {
        //         _context.Orders.Remove(Order);
        //         await _context.SaveChangesAsync();
        //     }
        //
        //     return RedirectToPage("./Index");
        // }
    }
}
