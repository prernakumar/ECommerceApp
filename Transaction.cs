using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp
{
   public  class Transaction
    {
      [Key]
      public int TransactionID { get; set; }
      public int UserID { get; set; }
      public decimal TransactionAmount { get; set; }
    }


}
