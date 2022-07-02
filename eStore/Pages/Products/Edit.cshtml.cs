using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace eStore.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        // private readonly DataAccess.Models.ProductStoreDbContext _context;
        //
        // public EditModel(DataAccess.Models.ProductStoreDbContext context)
        // {
        //     _context = context;
        // }
        //
        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = id;

           //  Product = await _context.Products
           //      .Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id);
           //
           //  if (Product == null)
           //  {
           //      return NotFound();
           //  }
           // ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return Page();
        }

        // public async Task<IActionResult> OnPostAsync()
        // {
            // if (!ModelState.IsValid)
            // {
            //      return Page();
            // }
            //
            // _context.Attach(Product).State = EntityState.Modified;
            //
            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!ProductExists(Product.ProductId))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }
            //
            // return RedirectToPage("./Index");
        // }
    }
}
