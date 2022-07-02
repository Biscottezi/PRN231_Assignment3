using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}