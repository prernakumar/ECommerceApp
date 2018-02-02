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
        public static List<UserAccount> displayUsers()
        {
            return db.UserAccounts.ToList();

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

        /*public static void BuyProduct(int productID,int quantity)
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
            
            
        }*/

        public static ShoppingCartProduct CreateProductShoppingCart(int productID, int quantitySelected)
        {

            //var allUsers = db.UserAccounts.ToList();
            var user = new UserAccount();
            ShoppingCartProduct shoppingCartProduct = null;
            int prodID;
            var product = db.Products.SingleOrDefault(a=>a.ProductID==productID);
            
            //implement IsProductChoiceCorrect() and ChooseCorrectProductID() instead of the following:
            /*while (product == null)
            {
                Console.WriteLine("Enter correct Product ID");
                prodID = Convert.ToInt32(Console.ReadLine());
                product = db.Products.SingleOrDefault(a => a.ProductID.Equals(prodID));
            }*/
            //Console.WriteLine("Enter Email Address");
            // var email = Console.ReadLine(); 
            /* while(!(Console.ReadLine().Equals(String)))
             {
                 Console.WriteLine("Enter some value for Email Address");
                 email = Console.ReadLine();
             }
             */
            String email;
            do
            {
                Console.WriteLine("Enter Email Address");
                email = Console.ReadLine();
                //Console.WriteLine("Line {0}: {1}", ctr, s);
            } while (email == null || email.Contains('\n'));

            user = db.UserAccounts.SingleOrDefault(a => a.EmailAddress.Equals(email));
            while (user == null)
            {
                Console.WriteLine("Enter correct Email Address");
                email = Console.ReadLine();
                user = db.UserAccounts.SingleOrDefault(a => a.EmailAddress.Equals(email));
            }
            //var product=
          //  try
            //{
                //var user = db.UserAccounts.SingleOrDefault(a => a.EmailAddress.Equals(email));
                //int userID = 0;

                shoppingCartProduct = new ShoppingCartProduct()
                {
                    UserID = user.UserID,
                    ProductID = productID,
                    Quantity = quantitySelected,
                    ProductPrice=product.ProductPrice


                };
                //var newProduct=shoppingCartProduct;
                db.ShoppingCartProducts.Add(shoppingCartProduct);
                db.SaveChanges();
            //}
            /*catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }*/
            return shoppingCartProduct;
        }

        /*public static void AddToShoppingCart(String email,int productID, int productQuantitySelected)
        {
            //var product = db.Products.SingleOrDefault(a => a.ProductID == productID);
            //var user = db.UserAccounts.SingleOrDefault(a=>a.EmailAddress==email);
            //var productSelected=CreateProductPairForShoppingCart(productID, productQuantitySelected);
            /*var productSelected = new ShoppingCartProduct(productID, productQuantitySelected);
            List<ShoppingCartProduct> ShoppingCart = new List<ShoppingCartProduct>();
            user.ShoppingCart.Add(productSelected);*/

            

           /*var newProduct=CreateProductShoppingCart(email, productID, productQuantitySelected);
            db.ShoppingCartProducts.Add(newProduct);*/
        //}

        public static List<ShoppingCartProduct> GetShoppingCartProducts(String emailAddress)
        {
            var user = db.UserAccounts.SingleOrDefault(a => a.EmailAddress == emailAddress);
            ///List<ShoppingCartProduct> shoppingCart = user.ShoppingCart;
            /*foreach (ShoppingCartProduct currProduct in user.ShoppingCart)
            {
                var product = db.Products.SingleOrDefault(a => a.ProductID == currProduct.ProductID);
                Console.WriteLine("ProductName          ProductDescription              ProductPrice        ProductQuantity");
                Console.WriteLine("{product.ProductName}    {product.ProductDescription}    {product.ProductPrice}  {currProduct.Quantity}");

            }*/
            /*foreach (KeyValuePair<int, int> kvp in user.ShoppingCart)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.WriteLine("Product= {0}, Quantity = {1}", kvp.Key, kvp.Value);
            }*/

            List<ShoppingCartProduct> list = new List<ShoppingCartProduct>();
            
             return db.ShoppingCartProducts.Where(a => a.UserID == user.UserID).ToList();

        }
        public static Transaction CreateTransaction(String emailAddress, decimal sum)
        {
            var user=db.UserAccounts.SingleOrDefault(a => a.EmailAddress == emailAddress);

            Transaction transaction = new Transaction()
            {
                UserID = user.UserID,
                TransactionAmount = sum

            };
            db.Transactions.Add(transaction);
            db.SaveChanges();
            ReduceQuantityFromInventoryAfterTransaction(user.UserID);
            DeleteUserShoppingCartItemsAfterTransaction(user.UserID);
            
            return transaction;
        }
       
        public static void DeleteUserShoppingCartItemsAfterTransaction(int userID)
        {
            var shoppingCartProducts = db.ShoppingCartProducts.AsEnumerable().Where(a => a.UserID == userID).ToList();
            foreach (var row in shoppingCartProducts)
            {
                db.ShoppingCartProducts.Remove(row);
                db.SaveChanges();
                    
            }
        }
        public static void ReduceQuantityFromInventoryAfterTransaction(int userID)
        {
            var shoppingCartProducts = db.ShoppingCartProducts.AsEnumerable().Where(a => a.UserID == userID).ToList();
            foreach (ShoppingCartProduct row in shoppingCartProducts)
            {
                var product = db.Products.SingleOrDefault(p=>p.ProductID== row.ProductID);
                product.deductQuantityFromInventory(row.Quantity);
                db.SaveChanges();
                    
            }
        }

        public static void DeleteProductFromCart(String emailAddress, int productID)
        {
            var user = db.UserAccounts.SingleOrDefault(a => a.EmailAddress == emailAddress);
            var shoppingCartProducts = db.ShoppingCartProducts.AsEnumerable().Where(a => a.UserID == user.UserID).ToList();

            foreach (ShoppingCartProduct row in shoppingCartProducts)
            {
                if(row.ProductID==productID)
                    db.ShoppingCartProducts.Remove(row);

                //var product = db.Products.SingleOrDefault(p => p.ProductID == row.ProductID);
                //product.deductQuantityFromInventory(row.Quantity);
                db.SaveChanges();

            }

        }

        public static void EditQuantityOfProductInCart(String emailAddress, int productID,int newProductQuantity)
        {
            var user = db.UserAccounts.SingleOrDefault(a => a.EmailAddress == emailAddress);
            var shoppingCartProducts = db.ShoppingCartProducts.AsEnumerable().Where(a => a.UserID == user.UserID).ToList();

            foreach (ShoppingCartProduct row in shoppingCartProducts)
            {
                if (row.ProductID == productID)
                // var product = db.Products.SingleOrDefault(p => p.productID == row.ProductID);
                
                row.EditQuantityinCart(newProductQuantity);
                db.SaveChanges();
                //db.ShoppingCartProducts.EditProductQuantity(row);

                //var product = db.Products.SingleOrDefault(p => p.ProductID == row.ProductID);
                //product.deductQuantityFromInventory(row.Quantity);
               // db.SaveChanges();

            }

        }
    }
 }

