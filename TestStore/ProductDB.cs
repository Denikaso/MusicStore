using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStore;
using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore
{
    internal class ProductDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(string subcategoryTitle, string Title, string Description, double Price, int UnitsInCart, int UnitsInStock, int Rating, string Picture)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var subcategory = new CategoryBD().SearchByTitle(subcategoryTitle);
                if (subcategory != null && SearchByTitle(Title) == null)
                {
                    return db.GetTable<Product>()
                        .Value(p => p.SubcategoryId, subcategory.Id)
                        .Value(p => p.Title, Title)
                        .Value(p => p.Description, Description)
                        .Value(p => p.Price, Price)
                        .Value(p => p.UnitsInCart, UnitsInCart)
                        .Value(p => p.UnitsInStock, UnitsInStock)
                        .Value(p => p.Rating, Rating)
                        .Value(p => p.Picture, Picture)
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
                return db.GetTable<Product>().LoadWith(request => request.subcategory).ToList();
            }
        }

        public Product? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Product? SearchBySubCategory(string SubCategoryTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>().LoadWith(request => request.subcategory)
                    .Where(p => p.subcategory.Title == SubCategoryTitle)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByTitle(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Title == Title)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByDescription(string Description)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Description == Description)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByPrice(double Price)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Price == Price)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByUnitsInCart(int UnitsInCart)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.UnitsInCart == UnitsInCart)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByUnitsInStock(int UnitsInStock)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.UnitsInStock == UnitsInStock)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByRating(int Rating)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Rating == Rating)
                    .FirstOrDefault();
            }
        }

        public Product? SearchByPicture(string Picture)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Product>()
                    .Where(p => p.Picture == Picture)
                    .FirstOrDefault();
            }
        }

        public int UpdateProduct(int Id, string subcategoryTitle, string Title, string Description, double Price, int UnitsInCart, int UnitsInStock, int Rating, string Picture)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var subcategory = new SubcategoryBD().SearchByTitle(subcategoryTitle);
                if (subcategory != null && SearchById(Id) != null)
                {
                    return db.GetTable<Product>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.SubcategoryId, subcategory.Id)
                        .Set(p => p.Title, Title)
                        .Set(p => p.Description, Description)
                        .Set(p => p.Price, Price)
                        .Set(p => p.UnitsInCart, UnitsInCart)
                        .Set(p => p.UnitsInStock, UnitsInStock)
                        .Set(p => p.Rating, Rating)
                        .Set(p => p.Picture, Picture)                        
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
                return db.GetTable<Product>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}
