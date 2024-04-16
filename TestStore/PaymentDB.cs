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
    public class PaymentDB
    {
        private const string CONNECTION_STRING = @"Server=SHAMA;DataBase=MusicStore;Trusted_Connection=True;";

        public int Create(int order, double totalPrice, DateTime paymentDate, int paymentMethod)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (SearchByOrder(order) == null)
                {
                    return db.GetTable<Payment>()
                        .Value(p => p.OrderId, order)
                        .Value(p => p.TotalPrice, totalPrice)
                        .Value(p => p.PaymentDate, paymentDate)
                        .Value(p => p.PaymentMethod, paymentMethod)
                        .Insert();
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Payment> Read()
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Payment>().LoadWith(request => request.customer).ToList();
            }
        }

        public Payment? SearchById(int id)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Payment>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public Payment? SearchByOrder(int order)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Payment>().LoadWith(request => request.order)
                    .Where(p => p.order.Id == order)
                    .FirstOrDefault();
            }
        }

        public List<Payment>? SearchByTotalPrice(double totalPrice)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Payment>()
                    .Where(p => p.TotalPrice == totalPrice)
                    .ToList();
            }
        }

        public List<Payment>? SearchByPaymentDate(DateTime date)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Payment>()
                    .Where(p => p.PaymentDate == date)
                    .ToList();
            }
        }

        public List<Payment>? SearchByPaymentMethod(int method)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {
                return db.GetTable<Payment>()
                    .Where(p => p.PaymentMethod == method)
                    .ToList();
            }
        }

        public int UpdatePayment(int id, int order, double totalPrice, DateTime paymentDate, int paymentMethod)
        {
            using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
            {                
                if (SearchByOrder(order) != null)
                {
                    return db.GetTable<Payment>()
                        .Where(p => p.Id == id)
                        .Set(p => p.OrderId, order)
                        .Set(p => p.TotalPrice, totalPrice)
                        .Set(p => p.PaymentDate, paymentDate)
                        .Set(p => p.PaymentMethod, paymentMethod)
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
                return db.GetTable<Payment>()
                    .Where(c => c.Id == id)
                    .Delete();
            }
        }
    }
}
