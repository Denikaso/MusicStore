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

        public int Create(string name, string email, string phoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Value(c => c.Name, name)
                    .Value(c => c.Email, email)
                    .Value(c => c.PhoneNumber, phoneNumber)
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

        public Customer? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == id)
                    .FirstOrDefault();
            }
        }
        public Customer? SearchByName(string name)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Name == name)
                    .FirstOrDefault();
            }
        }
        public Customer? SearchByEmail(string email)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Email == email)
                    .FirstOrDefault();
            }
        }

        public Customer? SearchByPhoneNumber(string phoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.PhoneNumber == phoneNumber)
                    .FirstOrDefault();
            }
        }

        public int UpdateCustomer(int id, string name, string email, string phoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == id)
                    .Set(c => c.Name, name)
                    .Set(c => c.Email, email)
                    .Set(c => c.PhoneNumber, phoneNumber)
                    .Update();
            }
        }

        public int Delete(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Customer>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}
