using System.Collections.Generic;
using AutoMapper;
using BusinessObject.ResponseModels;
using DataAccess.DAOs;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;

        public UserRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }
        
        public IEnumerable<UserResponseModel> GetUsers()
        {
            var us = UserDAO.Instance.GetUsers();
            var users = mapper.Map<IEnumerable<UserResponseModel>>(us);
            return users;
        }

        public LoginResponseModel Login(string email, string password)
        {
            if (email.Equals("admin@estore.com") && password.Equals("admin@@"))
            {
                return new LoginResponseModel()
                {
                    Role = "Admin",
                    User = null,
                };
            }
            else
            {
                var u = UserDAO.Instance.LoginUser(email, password);
                if (u == null)
                {
                    return null;
                }

                return new LoginResponseModel()
                {
                    Role = "User",
                    User = mapper.Map<UserResponseModel>(u),
                };
            }
        }

        public void Create(string email, string password)
        {
            UserDAO.Instance.Create(email, password);
        }

        public UserResponseModel GetById(string id)
        {
            var u = UserDAO.Instance.GetById(id);
            var user = mapper.Map<UserResponseModel>(u);
            return user;
        }
    }
}