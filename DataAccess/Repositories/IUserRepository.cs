using System.Collections.Generic;
using BusinessObject.ResponseModels;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserResponseModel> GetUsers();
        LoginResponseModel Login(string email, string password);
        void Create(string email, string password);
        UserResponseModel GetById(string id);
    }
}