using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModels
{
    public class OrderRequestModel
    {
        public string UserId { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int Freight { get; set; }
        public virtual ICollection<OrderDetailRequestModel> OrderDetails { get; set; }
    }
}
