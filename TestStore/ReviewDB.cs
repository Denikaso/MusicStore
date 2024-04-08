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

        public int Create(string Customer, string Product, int Rating, string Text)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(Customer);
                var product = new ProductDB().SearchByTitle(Product);

                if (customer != null && product != null)
                {
                    return db.GetTable<Review>()
                        .Value(r => r.CustomerId, customer.Id)
                        .Value(r => r.ProductId, product.Id)
                        .Value(r => r.Rating, Rating)
                        .Value(r => r.Text, Text)
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

        public Review? SearchById(int Id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>()
                    .Where(r => r.Id == Id)
                    .FirstOrDefault();
            }
        }

        public List<Review>? SearchByCustomer(string Customer)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>().LoadWith(request => request.customer)
                    .Where(p => p.customer.Name == Customer)
                    .ToList();
            }
        }

        public List<Review>? SearchByProduct(string Product)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>().LoadWith(request => request.product)
                    .Where(p => p.product.Title == Product)
                    .ToList();
            }
        }

        public List<Review>? SearchByRating(int Rating)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Review>()
                    .Where(r => r.Rating == Rating)
                    .ToList();
            }
        }

        public int UpdateReview(int Id, string Customer, string Product, int Rating, string Text)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                var customer = new CustomerBD().SearchByName(Customer);
                var product = new ProductDB().SearchByTitle(Product);

                if (customer != null && product != null)
                {
                    return db.GetTable<Review>()
                        .Where(p => p.Id == Id)
                        .Set(r => r.CustomerId, customer.Id)
                        .Set(r => r.ProductId, product.Id)
                        .Set(r => r.Rating, Rating)
                        .Set(r => r.Text, Text)
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