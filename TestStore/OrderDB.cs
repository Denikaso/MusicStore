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

        public int Create(string Customer, int Status, DateTime Date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(Customer);
                if (customer != null)
                {
                    return db.GetTable<Order>()
                        .Value(p => p.CustomerId, customer.Id)
                        .Value(p => p.Status, Status)
                        .Value(p => p.Date, Date)
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

        public Order? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public List<Order>? SearchBySubCustomer(string Customer)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>().LoadWith(request => request.customer)
                    .Where(p => p.customer.Name == Customer)
                    .ToList();
            }
        }

        public List<Order>? SearchByStatus(int Status)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Status == Status)
                    .ToList();
            }
        }

        public List<Order>? SearchByTotalDate(DateTime Date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Order>()
                    .Where(p => p.Date == Date)
                    .ToList();
            }
        }

        public int UpdateOrder(int Id, string Customer, int Status, DateTime Date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(Customer);
                if (customer != null)
                {
                    return db.GetTable<Order>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.CustomerId, customer.Id)
                        .Set(p => p.Status, Status)
                        .Set(p => p.Date, Date)
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
                return db.GetTable<Order>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}