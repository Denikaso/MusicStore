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
    public class SectionBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";
        public int Create(string title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                if (SearchByTitle(title) != null)
                    return db.GetTable<Section>()
                        .Value(p => p.Title, title)
                        .Insert();
                else
                    return -1;
            }
        }
        public List<Section> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>().ToList();
            }
        }

        public Section? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public Section? SearchByTitle(string title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(p => p.Title == title)
                    .FirstOrDefault();
            }
        }

        public int UpdateSection(int id, string title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(c => c.Id == id)
                    .Set(c => c.Title, title)
                    .Update();
            }
        }

        public int Delete(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}