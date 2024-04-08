using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore
{
    internal class CartItemDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int Cart, string Product, double Quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var cart = new CartDB().SearchById(Cart);
                var product = new ProductDB().SearchByTitle(Product);
                if (cart != null && product != null)
                {
                    return db.GetTable<CartItem>()
                        .Value(p => p.CartId, Cart)
                        .Value(p => p.ProductId, product.Id)
                        .Value(p => p.Quantity, Quantity)
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<CartItem> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>()
                    .LoadWith(request => request.cart)
                    .LoadWith(request => request.product)
                    .ToList();
            }
        }

        public CartItem? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public List<CartItem>? SearchByCart(int Cart)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>()
                    .Where(p => p.CartId == Cart)
                    .ToList();
            }
        }

        public List<CartItem>? SearchByProduct(string Product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>().LoadWith(request => request.product)
                    .Where(p => p.product.Title == Product)
                    .ToList();
            }
        }

        public int UpdateCartItem(int Id, int Cart, string Product, double Quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var cart = new CartDB().SearchById(Cart);
                var product = new ProductDB().SearchByTitle(Product);
                if (cart != null && product != null)
                {
                    return db.GetTable<CartItem>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.CartId, Cart)
                        .Set(p => p.ProductId, product.Id)
                        .Set(p => p.Quantity, Quantity)
                        .Update();
                }
                else
                {
                    return -1;
                }
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
