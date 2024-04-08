using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStore;
using System.Collections.Generic;
using System.Linq;

namespace TestStore
{
    public class SubcategoryBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(string Title, string categoryTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var category = new CategoryBD().SearchByTitle(categoryTitle);
                if (category != null && SearchByTitle(Title) == null)
                {
                    return db.GetTable<Subcategory>()
                        .Value(p => p.Title, Title)
                        .Value(p => p.CategoryId, category.Id)
                        .Insert();
                }
                else
                {
                    return -1; 
                }
            }
        }

        public List<Subcategory> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Subcategory>().LoadWith(s => s.Category).ToList();
            }
        }

        public Subcategory? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Subcategory>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Subcategory? SearchByTitle(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Subcategory>()
                    .Where(p => p.Title == Title)
                    .FirstOrDefault();
            }
        }

        public Subcategory? SearchByCategory(string CategoryTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Subcategory>().LoadWith(request => request.Category)
                    .Where(p => p.Category.Title == CategoryTitle)
                    .FirstOrDefault();
            }
        }

        public int UpdateSubcategory(int Id, string Title, string categoryTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var category = new CategoryBD().SearchByTitle(categoryTitle);
                if (category != null && SearchById(Id) != null)
                {
                    return db.GetTable<Subcategory>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.Title, Title)
                        .Set(p => p.CategoryId, category.Id)
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
                return db.GetTable<Subcategory>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}