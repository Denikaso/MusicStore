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
    public class CartItemBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int cart, int product, int quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                                
                if (cart != null && product != null)
                {
                    return db.GetTable<CartItem>()
                        .Value(p => p.CartId, cart)
                        .Value(p => p.ProductId, product)
                        .Value(p => p.Quantity, quantity)
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

        public CartItem? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public List<CartItem>? SearchByCart(int cart)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>()
                    .Where(p => p.CartId == cart)
                    .ToList();
            }
        }

        public List<CartItem>? SearchByProduct(int product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>().LoadWith(request => request.product)
                    .Where(p => p.product.Id == product)
                    .ToList();
            }
        }

        public int UpdateCartItem(int id, int cart, int product, int quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                if (cart != null && product != null)
                {
                    return db.GetTable<CartItem>()
                        .Where(p => p.Id == id)
                        .Set(p => p.CartId, cart)
                        .Set(p => p.ProductId, product)
                        .Set(p => p.Quantity, quantity)
                        .Update();
                }
                else
                {
                    return -1;
                }
            }
        }

        public int Delete(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<CartItem>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}
