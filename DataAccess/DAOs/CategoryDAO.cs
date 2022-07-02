using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.DAOs
{
    public class CategoryDAO
    {
        private static CategoryDAO _instance = null;
        private static readonly object InstanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new CategoryDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<Category> GetCategoryList()
        {
            List<Category> categories = null;
            try
            {
                var db = new ProductStoreDbContext();
                categories = db.Categories.ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - GetCategoryList - CategoryDAO -> " + e.Message);
            }
            return categories;
        }
    }
}
