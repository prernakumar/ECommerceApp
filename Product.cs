using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp
{
    public class Product
    {
       [Key]
       public int ProductID           {get ; set;}
       public String ProductName      {get; set;}
public String ProductDescription     {get; set;}
       public int ProductQuantity     {get; set;}
       public decimal ProductPrice          {get; set;}
        //List of sellers
        //String Category;

        public  void deductQuantityFromInventory(int productQuantityBought)
        {
            ProductQuantity = ProductQuantity - productQuantityBought;

        }

        public decimal getAmountOfProduct(int productQuantityBought)
        {
            return (productQuantityBought * ProductPrice);
        }

        

    }

    

    






}
