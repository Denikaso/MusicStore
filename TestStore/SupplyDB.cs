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
    internal class SupplyDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int supplier, int product, double pricePerUnit, int quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                if (supplier != null && product != null)
                {
                    return db.GetTable<Supply>()
                        .Value(r => r.SupplierId, supplier)
                        .Value(r => r.ProductId, product)
                        .Value(r => r.PricePerUnit, pricePerUnit)
                        .Value(r => r.Quantity, quantity)
                        .Value(r => r.Date, DateTime.Now)
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Supply> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .LoadWith(r => r.supplier)
                    .LoadWith(r => r.product)
                    .ToList();
            }
        }

        public Supply? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.Id == id)
                    .FirstOrDefault();
            }
        }

        public List<Supply>? SearchBySupplier(int supplier)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>().LoadWith(request => request.supplier)
                    .Where(p => p.supplier.Id == supplier)
                    .ToList();
            }
        }

        public List<Supply>? SearchByProduct(int product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>().LoadWith(request => request.product)
                    .Where(p => p.product.Id == product)
                    .ToList();
            }
        }

        public List<Supply>? SearchByPricePerUnit(double pricePerUnit)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.PricePerUnit == pricePerUnit)
                    .ToList();
            }
        }
        public List<Supply>? SearchByQuantity(int quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.Quantity == quantity)
                    .ToList();
            }
        }

        public List<Supply>? SearchByDate(DateTime date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.Date == date)
                    .ToList();
            }
        }

        public int UpdateSupply(int id, int supplier, int product, double pricePerUnit, int quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {               
                if (supplier != null && product != null)
                {
                    return db.GetTable<Supply>()
                        .Where(s => s.Id == id)
                        .Set(r => r.SupplierId, supplier)
                        .Set(r => r.ProductId, product)
                        .Set(r => r.PricePerUnit, pricePerUnit)
                        .Set(r => r.Quantity, quantity)
                        .Set(r => r.Date, DateTime.Now)
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
                return db.GetTable<Supply>()
                    .Where(r => r.Id == id)
                    .Delete();
            }
        }
    }
}
