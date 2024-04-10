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

        public int Create(int order, DateTime deliveryDate, string address, bool status, double price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                               
                if (order != null)
                {
                    return db.GetTable<Delivery>()
                        .Value(p => p.OrderId, order)
                        .Value(p => p.DeliveryDate, deliveryDate)
                        .Value(p => p.Address, address)
                        .Value(p => p.Status, status)
                        .Value(p => p.Price, price)
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

        public Delivery? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public List<Delivery>? SearchByOrder(int order)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.OrderId == order)
                    .ToList();
            }
        }

        public List<Delivery>? SearchByDeliveryDate(DateTime date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.DeliveryDate == date)
                    .ToList();
            }
        }

        public List<Delivery>? SearchByAddress(string address)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Address == address)
                    .ToList();
            }
        }

        public List<Delivery>? SearchByStatus(bool status)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Status == status)
                    .ToList();
            }
        }
        public List<Delivery>? SearchByPrice(double price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Delivery>()
                    .Where(p => p.Price == price)
                    .ToList();
            }
        }

        public int UpdateDelivey(int id, int order, DateTime deliveryDate, string address, bool status, double price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (order != null)
                {
                    return db.GetTable<Delivery>()
                        .Where(p => p.Id == id)
                        .Set(p => p.OrderId, order)
                        .Set(p => p.DeliveryDate, deliveryDate)
                        .Set(p => p.Address, address)
                        .Set(p => p.Status, status)
                        .Set(p => p.Price, price)
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
                return db.GetTable<Delivery>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}
