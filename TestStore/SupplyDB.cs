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

        public int Create(string Supplier, string Product, double PricePerUnit, int Quantity, DateTime Date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var supplier = new SupplierBD().SearchByName(Supplier);
                var product = new ProductDB().SearchByTitle(Product);
                if (supplier != null && product != null)
                {
                    return db.GetTable<Supply>()
                        .Value(r => r.SupplierId, supplier.Id)
                        .Value(r => r.ProductId, product.Id)
                        .Value(r => r.PricePerUnit, PricePerUnit)
                        .Value(r => r.Quantity, Quantity)
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

        public Supply? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.Id == Id)
                    .FirstOrDefault();
            }
        }

        public List<Supply>? SearchBySupplier(string Supplier)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>().LoadWith(request => request.supplier)
                    .Where(p => p.supplier.Name == Supplier)
                    .ToList();
            }
        }

        public List<Supply>? SearchByProduct(string Product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>().LoadWith(request => request.product)
                    .Where(p => p.product.Title == Product)
                    .ToList();
            }
        }

        public List<Supply>? SearchByPricePerUnit(double PricePerUnit)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.PricePerUnit == PricePerUnit)
                    .ToList();
            }
        }
        public List<Supply>? SearchByQuantity(int Quantity)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.Quantity == Quantity)
                    .ToList();
            }
        }

        public List<Supply>? SearchByDate(DateTime Date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Supply>()
                    .Where(r => r.Date == Date)
                    .ToList();
            }
        }

        public int UpdateSupply(int Id, string Supplier, string Product, double PricePerUnit, int Quantity, DateTime Date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var supplier = new SupplierBD().SearchByName(Supplier);
                var product = new ProductDB().SearchByTitle(Product);
                if (supplier != null && product != null)
                {
                    return db.GetTable<Supply>()
                        .Where(s => s.Id == Id)
                        .Set(r => r.SupplierId, supplier.Id)
                        .Set(r => r.ProductId, product.Id)
                        .Set(r => r.PricePerUnit, PricePerUnit)
                        .Set(r => r.Quantity, Quantity)
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
