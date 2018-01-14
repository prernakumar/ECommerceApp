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
                        Console.WriteLine("Enter EmailAddress");
                        var emailAddress = Console.ReadLine();
                        Console.WriteLine("Enter UserName");
                        var userName = Console.ReadLine();
                        var userAccount = Amazon.CreateAccount(firstName, lastName, userName, emailAddress);
                        Console.WriteLine($"FN:{userAccount.FirstName},LN:{userAccount.LastName},UN:{userAccount.UserName},EA:{userAccount.EmailAddress}");
                        break;
                    case "2":
                        Console.WriteLine("Buy Product");
                        Amazon.DisplayProducts();
                        Console.WriteLine("Select a product");
                        int productChoice=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Select it's quantity");
                        int productQuantity = Convert.ToInt32(Console.ReadLine());
                        Amazon.BuyProduct(productChoice,productQuantity);
                        break;
                    case "3":
                        Console.WriteLine("View Shopping Cart");
                        break;
                    case "4":
                        //Console.WriteLine("Add Product to Inventory");
                        System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\akbhatia\Documents\Prerna\KAL Academy\doc.txt");
                        getTokensFromFileAndAddProduct(file);
                        Console.WriteLine("Products Added");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }

            }
        }

        public static void getTokensFromFileAndAddProduct(System.IO.StreamReader file)
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


    }
}





