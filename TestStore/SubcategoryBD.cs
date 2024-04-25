using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStoreLibrary;
using System.Collections.Generic;
using System.Linq;

namespace TestStore
{
    public class SubcategoryBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(string title, int category)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (category != null && SearchByTitle(title) == null)
                {
                    return db.GetTable<Subcategory>()
                        .Value(p => p.Title, title)
                        .Value(p => p.CategoryId, category)
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

        public Subcategory? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Subcategory>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public Subcategory? SearchByTitle(string title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Subcategory>()
                    .Where(p => p.Title == title)
                    .FirstOrDefault();
            }
        }

        public List<Subcategory>? SearchByCategory(int category)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Subcategory>().LoadWith(request => request.Category)
                    .Where(p => p.Category.Id == category)
                    .ToList();
            }
        }

        public int UpdateSubcategory(int id, string title, int category)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (category != null && SearchById(id) != null)
                {
                    return db.GetTable<Subcategory>()
                        .Where(p => p.Id == id)
                        .Set(p => p.Title, title)
                        .Set(p => p.CategoryId, category)
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
                return db.GetTable<Subcategory>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}