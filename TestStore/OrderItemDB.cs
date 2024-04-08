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
    internal class OrderItemDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int Order, string Product, double Quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var order = new OrderDB().SearchById(Order);
                var product = new ProductDB().SearchByTitle(Product);
                if (order != null && product != null)
                {
                    return db.GetTable<OrderItem>()
                        .Value(p => p.OrderId, Order)
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

        public OrderItem? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public List<OrderItem>? SearchByOrder(int Order)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>()
                    .Where(p => p.OrderId == Order)
                    .ToList();
            }
        }

        public List<OrderItem>? SearchByProduct(string Product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<OrderItem>().LoadWith(request => request.product)
                    .Where(p => p.product.Title == Product)
                    .ToList();
            }
        }

        public int UpdateOrderItem(int Id, int Order, string Product, double Quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var order = new OrderDB().SearchById(Order);
                var product = new ProductDB().SearchByTitle(Product);
                if (order != null && product != null)
                {
                    return db.GetTable<OrderItem>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.OrderId, Order)
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
                return db.GetTable<OrderItem>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
