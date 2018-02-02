using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp
{
    class Program
    {
        System.IO.StreamReader file;
        static void Main(string[] args)
        {
            Console.WriteLine("***************");
            Console.WriteLine("Welcome to Amazon");
            Console.WriteLine("***************");
            while (true)
            {
                Console.WriteLine("0.Exit");
                Console.WriteLine("1.Create an Account");
                Console.WriteLine("2.Buy Product");
                Console.WriteLine("3.View Shopping Cart");
                Console.WriteLine("4.Add Product to Inventory");
                var choice = Console.ReadLine();
                //  System.IO.StreamReader file;
                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        Console.WriteLine("Enter First Name");
                        var firstName = Console.ReadLine();
                        Console.WriteLine("Enter Last Name");
                        var lastName = Console.ReadLine();
                        Console.WriteLine("Enter UserName");
                        var userName = Console.ReadLine();
                        Console.WriteLine("Enter EmailAddress");
                        var emailAddress = Console.ReadLine();

                        //var userAccount = Amazon.CreateAccount(firstName, lastName, userName, emailAddress);
                        Amazon.CreateAccount(firstName, lastName, userName, emailAddress);
                       // Console.WriteLine($"FN:{userAccount.FirstName},LN:{userAccount.LastName},UN:{userAccount.UserName},EA:{userAccount.EmailAddress}");
                        PrintAllUsers();
                        break;
                    case "2":
                        Console.WriteLine("Select products to add to cart");
                        Amazon.DisplayProducts();
                        Console.WriteLine("Select a product");
                        int productChoice=Convert.ToInt32(Console.ReadLine());
                        //to do*******************
                        //if (!IsProductChoiceCorrect(productChoice) || IsProductInShoppingCart)
                            
                        Console.WriteLine("Select it's quantity");
                        int productQuantity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Add to Cart? Enter Y or N");
                        var addToCartChoice = Console.ReadLine();
                        //if (addToCartChoice.Equals('Y'))
                        //{
                            
                            Amazon.CreateProductShoppingCart(productChoice, productQuantity);
                        //}
                        break;
                    case "3":
                       // Console.WriteLine("View Shopping Cart");
                        // Console.WriteLine("Enter Email Address");
                        //var emailAdd = Console.ReadLine();
                        //Amazon.DisplayShoppingCartProducts(emailAdd);
                        PrintAllProductsInShoppingCart();
                        break;
                    case "4":
                        //Console.WriteLine("Add Product to Inventory");
                        System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\akbhatia\Documents\Prerna\KAL Academy\doc.txt");
                        AddProductToInventory(file);
                        Console.WriteLine("Products Added");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }

            }
        }

        public static void AddProductToInventory(System.IO.StreamReader file)
        {
           
            String line = String.Empty;
            //String[] tokens = new String[line.Length];
            List<Product> data = new List<Product>();
            while ((line = file.ReadLine()) != null)
            {
                //System.Console.WriteLine(line);
                // List<String> list = new List<string>();
                //var tokensCount = line.Split().Count(x => x.Equals('\t'));
                //String[] tokens = new string[tokensCount];
                //tokens = line.Split('\t');
                String[] tokens = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> list = new List<string>(tokens);
                
                Amazon.addProductToInventory(list);
                //data.Add(product);
                //counter++;
            }

            //return data;
        }

        public static void PrintAllProductsInShoppingCart()
        {
            Console.WriteLine("Enter Email Address");
            var emailAdd=Console.ReadLine();
            var shoppingCartProducts=Amazon.GetShoppingCartProducts(emailAdd);
            decimal sum = 0;
            foreach (ShoppingCartProduct product in shoppingCartProducts)
            {
                Console.WriteLine("ProductID                  ProductQuantity       ProductPrice");
                Console.WriteLine($"{product.ProductID},        {product.Quantity},     {product.ProductPrice*product.Quantity}");
                sum += product.Quantity * product.ProductPrice;
            }
            Console.WriteLine("Total Amount is:" +  sum);

            /*Console.WriteLine("Do you want to buy all the above items? Y or N");
            String choice= Console.ReadLine();
            if (choice.Equals("Y"))
             {
                 Amazon.CreateTransaction(emailAdd,sum);                   
             }*/

            while (true)
            {
                Console.WriteLine("1.Buy All Items in Cart");
                Console.WriteLine("2.Delete a product from cart");
                Console.WriteLine("3.Edit Quantity of Product");
                Console.WriteLine("Select an option");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Amazon.CreateTransaction(emailAdd, sum);
                        break;
                    case "2":
                        Console.WriteLine("Enter choice of product");
                        var productChoice = Convert.ToInt32(Console.ReadLine());
                        Amazon.DeleteProductFromCart(emailAdd, productChoice);
                        break;
                    case "3":
                        Console.WriteLine("Enter choice of product");
                        productChoice = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter new quantity of product");
                        var newProductQuantity = Convert.ToInt32(Console.ReadLine());
                        Amazon.EditQuantityOfProductInCart(emailAdd, productChoice, newProductQuantity);
                        break;
                    
                    default:
                        Console.WriteLine("Invalid Choice");
                        return;
                }
            }
         }

        public static void PrintAllUsers()
        {
            var users = Amazon.displayUsers();
            foreach (UserAccount user in users)
            {
                Console.WriteLine($"FN:{user.FirstName},LN{user.LastName},UN:{user.UserName},EA:{user.EmailAddress} ");
            }

        }

    }
}





