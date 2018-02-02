using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp
{
    public class ShoppingCartProduct
    {
        [Key]
        [Column(Order = 1)]        
        public int UserID { get; set;}
        [Key]
        [Column(Order = 2)]
        public int ProductID   {get;set;}
        public int Quantity    {get;set;}
        public decimal ProductPrice { get; set; }

        /* public ShoppingCartProduct(int userID,int productId, int quantitySelected)
         {
             UserID = userID;
             ProductID = productId;
             Quantity = quantitySelected;
         }*/

        public void EditQuantityinCart(int newQuantity)
        {
            Quantity = newQuantity;
        }

    }
  

}
