using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModels
{
    public class OrderResponseModel
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int Freight { get; set; }
        public UserResponseModel User { get; set; }
        public virtual ICollection<OrderDetailResponseModel> OrderDetails { get; set; }
    }
}
