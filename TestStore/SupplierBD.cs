using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using System.Collections.Generic;
using System.Linq;
using MusicStoreLibrary;

namespace TestStore
{
    public class SupplierBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(string name, string email, string phoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Value(s => s.Name, name)
                    .Value(s => s.Email, email)
                    .Value(s => s.PhoneNumber, phoneNumber)
                    .Insert();
            }
        }

        public List<Supplier> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>().ToList();
            }
        }

        public Supplier? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(s => s.Id == id)
                    .FirstOrDefault();
            }
        }
        public Supplier? SearchByName(string name)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.Name == name)
                    .FirstOrDefault();
            }
        }

        public Supplier? SearchByEmail(string email)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.Email == email)
                    .FirstOrDefault();
            }
        }

        public Supplier? SearchByPhoneNumber(string phoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.PhoneNumber == phoneNumber)
                    .FirstOrDefault();
            }
        }

        public int UpdateSupplier(int id, string name, string email, string phoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(s => s.Id == id)
                    .Set(s => s.Name, name)
                    .Set(s => s.Email, email)
                    .Set(s => s.PhoneNumber, phoneNumber)
                    .Update();
            }
        }

        public int Delete(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(s => s.Id == id)
                    .Delete();
            }
        }
    }
}
