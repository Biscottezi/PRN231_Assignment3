using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.ResponseModels;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryResponseModel> GetCategories();
    }
}
