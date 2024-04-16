using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TestStore
{
    public class ReviewBD
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int customer, int product, int rating, string text)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (customer != null && product != null)
                {
                    return db.GetTable<Review>()
                        .Value(r => r.CustomerId, customer)
                        .Value(r => r.ProductId, product)
                        .Value(r => r.Rating, rating)
                        .Value(r => r.Text, text)
                        .Value(r => r.Date, DateTime.Now) 
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Review> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>()
                    .LoadWith(r => r.customer)
                    .LoadWith(r => r.product)
                    .ToList();
            }
        }

        public Review? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>()
                    .Where(r => r.Id == id)
                    .FirstOrDefault();
            }
        }

        public List<Review>? SearchByCustomer(int customer)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>().LoadWith(request => request.customer)
                    .Where(p => p.customer.Id == customer)
                    .ToList();
            }
        }

        public List<Review>? SearchByProduct(int product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>().LoadWith(request => request.product)
                    .Where(p => p.product.Id == product)
                    .ToList();
            }
        }

        public List<Review>? SearchByRating(int rating)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>()
                    .Where(r => r.Rating == rating)
                    .ToList();
            }
        }

        public int UpdateReview(int id, int customer, int product, int rating, string text)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (customer != null && product != null)
                {
                    return db.GetTable<Review>()
                        .Where(p => p.Id == id)
                        .Set(r => r.CustomerId, customer)
                        .Set(r => r.ProductId, product)
                        .Set(r => r.Rating, rating)
                        .Set(r => r.Text, text)
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
                return db.GetTable<Review>()
                    .Where(r => r.Id == id)
                    .Delete();
            }
        }
    }
}