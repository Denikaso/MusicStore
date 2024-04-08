using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStore;
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
        public int Create(string Title, string sectionTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var section = new SectionBD().SearchByTitle(sectionTitle);
                if (section != null && SearchByTitle(Title) != null)
                    return db.GetTable<Category>()
                        .Value(p => p.Title, Title)
                        .Value(p => p.SectionId, section.Id)
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

        public Category? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Category? SearchByTitle(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>()
                    .Where(p => p.Title == Title)
                    .FirstOrDefault();
            }
        }
        public Category? SearchBySection(string SectionTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>().LoadWith(request => request.Section)
                    .Where(p => p.Section.Title == SectionTitle)
                    .FirstOrDefault();
            }
        }

        public int UpdateCategory(int Id, string Title, string sectionTitle)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var section = new SectionBD().SearchByTitle(sectionTitle);
                if (section != null && SearchById(Id) != null)
                    return db.GetTable<Category>()
                        .Where(p => p.Id == Id)
                        .Set(p => p.Title, Title)
                        .Set(p => p.SectionId, section.Id)
                        .Update();
                else
                    return -1;
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Category>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}