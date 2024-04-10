using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Common.Configuration;

namespace TestStore
{
    internal class OrderDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int customer, int status, DateTime date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (customer != null)
                {
                    return db.GetTable<Order>()
                        .Value(p => p.CustomerId, customer)
                        .Value(p => p.Status, status)
                        .Value(p => p.Date, date)
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Order> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>().LoadWith(request => request.customer).ToList();
            }
        }

        public Order? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public List<Order>? SearchByCustomer(string customer)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>().LoadWith(request => request.customer)
                    .Where(p => p.customer.Name == customer)
                    .ToList();
            }
        }

        public List<Order>? SearchByStatus(int status)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Status == status)
                    .ToList();
            }
        }

        public List<Order>? SearchByDate(DateTime date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Date == date)
                    .ToList();
            }
        }

        public int UpdateOrder(int id, int customer, int status, DateTime date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (customer != null)
                {
                    return db.GetTable<Order>()
                        .Where(p => p.Id == id)
                        .Set(p => p.CustomerId, customer)
                        .Set(p => p.Status, status)
                        .Set(p => p.Date, date)
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
                return db.GetTable<Order>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}