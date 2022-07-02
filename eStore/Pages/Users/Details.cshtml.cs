using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Users
{
    [Authorize(Roles = "User")]
    public class Details : PageModel
    {
        public IActionResult OnGet()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "User")
            {
                RedirectToPage("../Error");
            }
            ViewData.Add("IsUser", true);

            var idString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idString == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = idString;
            return Page();
        }
    }
}