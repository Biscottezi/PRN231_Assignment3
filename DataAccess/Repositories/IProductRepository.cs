using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.RequestModels;
using BusinessObject.ResponseModels;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductResponseModel> GetProducts();
        ProductResponseModel GetById(int id);
        void Create(ProductRequestModel product);
        void Update(ProductRequestModel product);
        void Delete(int id);
    }
}
