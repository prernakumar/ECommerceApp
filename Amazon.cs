using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp
{
   static class Amazon  
    {
        private static ECommerceModel db = new ECommerceModel();
        
        public static UserAccount CreateAccount(string firstName, string lastName, string userName, string
            emailAddress)

        {
            var userAccount = new UserAccount()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                EmailAddress = emailAddress
            };
             db.UserAccounts.Add(userAccount);
             db.SaveChanges();
            return userAccount;
        }
        public static Product addProductToInventory(List<String> listOfDetails)
        {
                


            var product = new Product()
            {
                
                ProductName = listOfDetails[0],
                ProductDescription = listOfDetails[1],
                ProductQuantity = Convert.ToInt32(listOfDetails[2]),
                ProductPrice=Convert.ToDecimal(listOfDetails[3])
                                             
                };
            db.Products.Add(product);
            db.SaveChanges();
            return product;

        }
        public static void DisplayProducts()
        {
           var productList= db.Products.ToList();
            Console.Write("Count    Name                    Description                          Quantity       Price  ");
            Console.WriteLine();
            for (int i=0;i<productList.Count;i++)
            {
                Console.WriteLine($"{productList.ElementAt(i).ProductID}   {productList.ElementAt(i).ProductName}  {productList.ElementAt(i).ProductDescription}  {productList.ElementAt(i).ProductQuantity}  {productList.ElementAt(i).ProductPrice}");
            }

        }

        public static void BuyProduct(int productID,int quantity)
        {
            var product=db.Products.SingleOrDefault(a => a.ProductID == productID);
            //Product.deductQuantityFromInventory(quantity);
            if (product != null)
            {
                //product.ProductQuantity = product.ProductQuantity - quantity;
                product.deductQuantityFromInventory(quantity);
                db.SaveChanges();
            }
            Console.WriteLine("Pay" + product.getAmountOfProduct(quantity));
            
            
        }

        }
      }

