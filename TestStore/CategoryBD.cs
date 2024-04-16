using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace TestStore
{
    public class CategoryBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";
        public int Create(string title, int section)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (section != null && SearchByTitle(title) != null)
                    return db.GetTable<Category>()
                        .Value(p => p.Title, title)
                        .Value(p => p.SectionId, section)
                        .Insert();
                else
                    return -1;
            }
        }
        public List<Category> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>().LoadWith(request => request.Section).ToList();
            }
        }

        public Category? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public Category? SearchByTitle(string title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>()
                    .Where(p => p.Title == title)
                    .FirstOrDefault();
            }
        }
        public Category? SearchBySection(int section)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>().LoadWith(request => request.Section)
                    .Where(p => p.Section.Id == section)
                    .FirstOrDefault();
            }
        }

        public int UpdateCategory(int id, string title, int section)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (section != null && SearchById(id) != null)
                    return db.GetTable<Category>()
                        .Where(p => p.Id == id)
                        .Set(p => p.Title, title)
                        .Set(p => p.SectionId, section)
                        .Update();
                else
                    return -1;
            }
        }

        public int Delete(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}