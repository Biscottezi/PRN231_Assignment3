using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModels
{
    public class ProductResponseModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Weight { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public string CategoryName { get; set; }
    }
}
