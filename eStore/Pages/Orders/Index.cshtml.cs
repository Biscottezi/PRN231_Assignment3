using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace eStore.Pages.Orders
{
    [Authorize(Roles = "User,Admin")]
    public class IndexModel : PageModel
    {
        // private readonly DataAccess.Models.ProductStoreDbContext _context;
        //
        // public IndexModel(DataAccess.Models.ProductStoreDbContext context)
        // {
        //     _context = context;
        // }

        public IList<Order> Order { get;set; }

        public async Task<IActionResult> OnGetAsync()
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
    }
}
