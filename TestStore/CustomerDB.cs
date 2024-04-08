using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using System.Collections.Generic;
using System.Linq;
using MusicStoreLibrary;

namespace TestStore
{
    public class CustomerBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(string Name, string Email, string PhoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Value(c => c.Name, Name)
                    .Value(c => c.Email, Email)
                    .Value(c => c.PhoneNumber, PhoneNumber)
                    .Insert();
            }
        }

        public List<Customer> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>().ToList();
            }
        }

        public Customer SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == Id)
                    .FirstOrDefault();
            }
        }
        public Customer SearchByName(string Name)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Name == Name)
                    .FirstOrDefault();
            }
        }
        public Customer SearchByEmail(string Email)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Email == Email)
                    .FirstOrDefault();
            }
        }

        public Customer SearchByPhoneNumber(string PhoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.PhoneNumber == PhoneNumber)
                    .FirstOrDefault();
            }
        }

        public int UpdateCustomer(int Id, string Name, string Email, string PhoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == Id)
                    .Set(c => c.Name, Name)
                    .Set(c => c.Email, Email)
                    .Set(c => c.PhoneNumber, PhoneNumber)
                    .Update();
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
