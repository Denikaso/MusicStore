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
    public class ProductBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int subcategory, string title, string description, double price, int unitsInCart, int unitsInStock, double rating, string picture)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (subcategory != null && SearchByTitle(title) == null)
                {
                    return db.GetTable<Product>()
                        .Value(p => p.SubcategoryId, subcategory)
                        .Value(p => p.Title, title)
                        .Value(p => p.Description, description)
                        .Value(p => p.Price, price)
                        .Value(p => p.UnitsInCart, unitsInCart)
                        .Value(p => p.UnitsInStock, unitsInStock)
                        .Value(p => p.Rating, rating)
                        .Value(p => p.Picture, picture)
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Product> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                         .LoadWith(p => p.Subcategory)
                         .ThenLoad(subcategory => subcategory.Category)
                         .ThenLoad(category => category.Section)
                         .ToList();
            }
        }

        public Product? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public List<Product>? SearchBySubCategory(int subCategory)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>().LoadWith(request => request.Subcategory)
                    .Where(p => p.Subcategory.Id == subCategory)
                    .ToList();
            }
        }

        public Product? SearchByTitle(string title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Title == title)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByDescription(string description)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Description == description)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByPrice(double price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Price == price)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByUnitsInCart(int unitsInCart)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.UnitsInCart == unitsInCart)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByUnitsInStock(int unitsInStock)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.UnitsInStock == unitsInStock)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByRating(double rating)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Rating == rating)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByPicture(string picture)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Picture == picture)
                    .FirstOrDefault();
            }
        }

        public int UpdateProduct(int id, int subcategory, string title, string description, double price, int unitsInCart, int unitsInStock, double rating, string picture)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (subcategory != null && SearchById(id) != null)
                {
                    return db.GetTable<Product>()
                        .Where(p => p.Id == id)
                        .Set(p => p.SubcategoryId, subcategory)
                        .Set(p => p.Title, title)
                        .Set(p => p.Description, description)
                        .Set(p => p.Price, price)
                        .Set(p => p.UnitsInCart, unitsInCart)
                        .Set(p => p.UnitsInStock, unitsInStock)
                        .Set(p => p.Rating, rating)
                        .Set(p => p.Picture, picture)                        
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
                return db.GetTable<Product>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}
