using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestStore
{
    internal class DeliveryDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int Order, DateTime DeliveryDate, string Address, bool Status, double Price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var order = new OrderDB().SearchById(Order);                
                if (order != null)
                {
                    return db.GetTable<Delivery>()
                        .Value(p => p.OrderId, Order)
                        .Value(p => p.DeliveryDate, DeliveryDate)
                        .Value(p => p.Address, Address)
                        .Value(p => p.Status, Status)
                        .Value(p => p.Price, Price)
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Delivery> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .LoadWith(request => request.order)                    
                    .ToList();
            }
        }

        public Delivery? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public List<Delivery>? SearchByOrder(int Order)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.OrderId == Order)
                    .ToList();
            }
        }

        public List<Delivery>? SearchByDeliveryDate(DateTime Date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.DeliveryDate == Date)
                    .ToList();
            }
        }

        public List<Delivery>? SearchByAddress(string Address)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Address == Address)
                    .ToList();
            }
        }

        public List<Delivery>? SearchByStatus(bool Status)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Status == Status)
                    .ToList();
            }
        }
        public List<Delivery>? SearchByPrice(double Price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Price == Price)
                    .ToList();
            }
        }

        public int UpdateDelivey(int Id, int Order, DateTime DeliveryDate, string Address, bool Status, double Price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var order = new OrderDB().SearchById(Order);
                if (order != null)
                {
                    return db.GetTable<Delivery>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.OrderId, Order)
                        .Set(p => p.DeliveryDate, DeliveryDate)
                        .Set(p => p.Address, Address)
                        .Set(p => p.Status, Status)
                        .Set(p => p.Price, Price)
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
                return db.GetTable<Delivery>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
