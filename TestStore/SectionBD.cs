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
        public int Create(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                if (SearchByTitle(Title) != null)
                    return db.GetTable<Section>()
                        .Value(p => p.Title, Title)
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

        public Section? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(p => p.Id == Id)
                    .FirstOrDefault();
            }
        }

        public Section? SearchByTitle(string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(p => p.Title == Title)
                    .FirstOrDefault();
            }
        }

        public int UpdateSection(int Id, string Title)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(c => c.Id == Id)
                    .Set(c => c.Title, Title)
                    .Update();
            }
        }

        public int Delete(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Section>()
                    .Where(c => c.Id == Id)
                    .Delete();
            }
        }
    }
}