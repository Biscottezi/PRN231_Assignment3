using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessObject.ResponseModels;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class UserDAO
    {
        private static UserDAO _instance = null;
        private static readonly object InstanceLock = new object();

        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock(InstanceLock)
                {
                    if(_instance == null)
                    {
                        _instance = new UserDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> users = null;
            try
            {
                var db = new ProductStoreDbContext();
                users = db.Users.ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - GetUserList - UserDAO -> " + e.Message);
            }
            return users;
        }

        public User LoginUser(string email, string password)
        {
            var db = new ProductStoreDbContext();
            var u = db.Users.FirstOrDefault(u => u.Email.Equals(email)
                                                && u.PasswordHash.Equals(password));
            if (u == null)
            {
                return null;
            }
            else
            {
                return u;
            }
        }

        public void Create(string email, string password)
        {
            var db = new ProductStoreDbContext();
            try
            {
                var user = new User()
                {
                    Email = email,
                    UserName = email,
                    NormalizedEmail = email.ToUpper(),
                    NormalizedUserName = email.ToUpper(),
                    PasswordHash = password,
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - RegisterUser - UserDAO -> " + e.Message);
            }
        }
        
        public User GetById(string id)
        {
            User user = null;
            try
            {
                var db = new ProductStoreDbContext();
                user = db.Users.FirstOrDefault(u => u.Id.Equals(id));
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - GetUserById - UserDAO -> " + e.Message);
            }
            return user;
        }
    }
}