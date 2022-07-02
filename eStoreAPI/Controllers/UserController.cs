using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repo;

        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var users = repo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = repo.GetById(id);
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            var user = repo.Login(model.Email, model.Password);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestModel model)
        {
            repo.Create(model.Email, model.Password);
            return Ok();
        }
    }
}