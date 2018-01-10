using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECommerceApp
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String UserName { get; set; }
        public List<Product> ShoppingCart  { get; set;}

    }
}
