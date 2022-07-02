using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Login
{
    public class Register : PageModel
    {
        private readonly HttpClient apiClient;

        public Register(HttpClient apiClient)
        {
            this.apiClient = apiClient;
        }

        [BindProperty]
        public string Email { get; set; }
        
        [BindProperty]
        public string Password { get; set; }
        
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await apiClient.PostAsJsonAsync("users/register", new RegisterRequestModel()
            {
                Email = this.Email,
                Password = this.Password,
            });
            if (response.StatusCode == HttpStatusCode.OK)
            {
                RedirectToPage("Login");
            }
            return Page();
        }
    }
}