using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using BusinessObject.ResponseModels;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace eStore.Pages.Login
{
    public class Login : PageModel
    {
        private readonly HttpClient apiClient;

        public Login(HttpClient apiClient)
        {
            this.apiClient = apiClient;
        }

        [BindProperty]
        public string Email { get; set; }
        
        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await apiClient.PostAsJsonAsync("users/login", new LoginRequestModel()
            {
                Email = this.Email,
                Password = this.Password,
            });
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Message = "Wrong email or password";
                ViewData["Message"] = Message;
                return Page();
            }
            var dataString = await response.Content.ReadAsStringAsync();
            var login = JsonSerializer.Deserialize<LoginResponseModel>(dataString, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });

            if (login.Role.Equals("User"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, login.User.Id),
                    new Claim(ClaimTypes.Role, "User"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                //TODO: fill in url
                return RedirectToPage("../Orders/Index");
            }

            if (login.Role.Equals("Admin"))
            {
                var claims = new List<Claim>
                { 
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return RedirectToPage("../Products/Index");
            }
            return Page();
        }
    }
}