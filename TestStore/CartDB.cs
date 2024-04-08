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
    internal class CartDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(string Customer, bool Status, double TotalPrice)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(Customer);
                if (customer != null)
                {
                    return db.GetTable<Cart>()
                        .Value(p => p.CustomerId, customer.Id)
                        .Value(p => p.Status, Status)
                        .Value(p => p.TotalPrice, TotalPrice)
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Cart> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>().LoadWith(request => request.customer).ToList();
            }
        }

        public Cart? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Cart? SearchBySubCustomer(string Customer)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>().LoadWith(request => request.customer)
                    .Where(p => p.customer.Name == Customer)
                    .FirstOrDefault();
            }
        }

        public List<Cart>? SearchByStatus(bool Status)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>()
                    .Where(p => p.Status == Status)
                    .ToList();
            }
        }

        public List<Cart>? SearchByTotalPrice(double TotalPrice)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>()
                    .Where(p => p.TotalPrice == TotalPrice)
                    .ToList();
            }
        }        

        public int UpdateCart(int Id, string Customer, bool Status, double TotalPrice)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(Customer);
                if (customer != null)
                {
                    return db.GetTable<Cart>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.CustomerId, customer.Id)
                        .Set(p => p.Status, Status)
                        .Set(p => p.TotalPrice, TotalPrice)
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
                return db.GetTable<Cart>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
