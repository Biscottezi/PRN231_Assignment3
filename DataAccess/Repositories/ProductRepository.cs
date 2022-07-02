using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject.RequestModels;
using BusinessObject.ResponseModels;
using DataAccess.DAOs;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper mapper;
        public ProductRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void Create(ProductRequestModel product)
        {
            var p = mapper.Map<Product>(product);
            ProductDAO.Instance.Create(p);
        }

        public void Delete(int id)
        {
            ProductDAO.Instance.Delete(id);
        }

        public ProductResponseModel GetById(int id)
        {
            var p = ProductDAO.Instance.GetById(id);
            var product = mapper.Map<ProductResponseModel>(p);
            return product;
        }

        public IEnumerable<ProductResponseModel> GetProducts()
        {
            var ps = ProductDAO.Instance.GetProductList();
            var products = mapper.Map<IEnumerable<ProductResponseModel>>(ps);
            return products;
        }

        public void Update(ProductRequestModel product)
        {
            var p = mapper.Map<Product>(product);
            ProductDAO.Instance.Update(p);
        }
    }
}
