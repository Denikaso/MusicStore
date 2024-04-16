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
    public class CartBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int customer, bool status, double totalPrice)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (SearchByCustomer(customer) == null)
                {
                    return db.GetTable<Cart>()
                        .Value(p => p.CustomerId, customer)
                        .Value(p => p.Status, status)
                        .Value(p => p.TotalPrice, totalPrice)
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

        public Cart? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public Cart? SearchByCustomer(int customer)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>().LoadWith(request => request.customer)
                    .Where(p => p.customer.Id == customer)
                    .FirstOrDefault();
            }
        }

        public List<Cart>? SearchByStatus(bool status)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>()
                    .Where(p => p.Status == status)
                    .ToList();
            }
        }

        public List<Cart>? SearchByTotalPrice(double totalPrice)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Cart>()
                    .Where(p => p.TotalPrice == totalPrice)
                    .ToList();
            }
        }        

        public int UpdateCart(int id, int customer, bool status, double totalPrice)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (customer != null)
                {
                    return db.GetTable<Cart>()
                        .Where(p => p.Id == id)
                        .Set(p => p.CustomerId, customer)
                        .Set(p => p.Status, status)
                        .Set(p => p.TotalPrice, totalPrice)
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
                return db.GetTable<Cart>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}
