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

        public int Create(string Name, string Email, string PhoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Value(s => s.Name, Name)
                    .Value(s => s.Email, Email)
                    .Value(s => s.PhoneNumber, PhoneNumber)
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

        public Supplier SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(s => s.Id == Id)
                    .FirstOrDefault();
            }
        }
        public Supplier SearchByName(string Name)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.Name == Name)
                    .FirstOrDefault();
            }
        }

        public Supplier SearchByEmail(string Email)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.Email == Email)
                    .FirstOrDefault();
            }
        }

        public Supplier SearchByPhoneNumber(string PhoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(c => c.PhoneNumber == PhoneNumber)
                    .FirstOrDefault();
            }
        }

        public int UpdateSupplier(int Id, string Name, string Email, string PhoneNumber)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(s => s.Id == Id)
                    .Set(s => s.Name, Name)
                    .Set(s => s.Email, Email)
                    .Set(s => s.PhoneNumber, PhoneNumber)
                    .Update();
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supplier>()
                    .Where(s => s.Id == Id)
                    .Delete();
            }
        }
    }
}
