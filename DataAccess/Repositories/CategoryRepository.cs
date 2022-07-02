using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.ResponseModels;
using DataAccess.DAOs;
using AutoMapper;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper mapper;
        public CategoryRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public IEnumerable<CategoryResponseModel> GetCategories()
        {
            var cs = CategoryDAO.Instance.GetCategoryList();
            var categories = mapper.Map<IEnumerable<CategoryResponseModel>>(cs);
            return categories;
        }
    }
}
