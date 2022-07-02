using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace eStore.Pages.Products
{
    [Authorize(Roles = "Admin")]
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
        // ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // public async Task<IActionResult> OnPostAsync()
        // {
            // if (!ModelState.IsValid)
            // {
            //      return Page();
            // }
            //
            // _context.Products.Add(Product);
            // await _context.SaveChangesAsync();
            //
            // return RedirectToPage("./Index");
        // }
    }
}
