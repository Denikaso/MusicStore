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
    public class OrderItemBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int order, int product, int quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                if (order != null && product != null)
                {
                    return db.GetTable<OrderItem>()
                        .Value(p => p.OrderId, order)
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

        public List<OrderItem> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>()
                    .LoadWith(request => request.order)
                    .LoadWith(request => request.product)
                    .ToList();
            }
        }

        public OrderItem? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public List<OrderItem>? SearchByOrder(int order)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>()
                    .Where(p => p.OrderId == order)
                    .ToList();
            }
        }

        public List<OrderItem>? SearchByProduct(string product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product)
                    .Where(p => p.product.Title == product)
                    .ToList();
            }
        }

        public int UpdateOrderItem(int id, int order, int product, int quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (order != null && product != null)
                {
                    return db.GetTable<OrderItem>()
                        .Where(p => p.Id == id)
                        .Set(p => p.OrderId, order)
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
                return db.GetTable<OrderItem>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}
