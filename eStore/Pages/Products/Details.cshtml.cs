using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace eStore.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        // private readonly DataAccess.Models.ProductStoreDbContext _context;
        //
        // public DetailsModel(DataAccess.Models.ProductStoreDbContext context)
        // {
        //     _context = context;
        // }

        //public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = id;
            
            // Product = await _context.Products
            //     .Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id);
            //
            // if (Product == null)
            // {
            //     return NotFound();
            // }
            return Page();
        }
    }
}
