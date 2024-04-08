using LinqToDB.Mapping;
using MusicStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Payment")]
    public class Payment
    {
        [PrimaryKey, Identity] public int PaymentId { get; set; }
        [Column("OrderId"), NotNull] public int OrderId { get; set; }
        [Association(ThisKey = nameof(OrderId), OtherKey = nameof(Order.Id))]
        public Order order { get; set; }
        [Column("TotalPrice"), NotNull] public float TotalPrice { get; set; }
        [Column("PaymentDate"), NotNull] public DateTime PaymentDate { get; set; }
        [Column("PaymentMethod"), NotNull] public int PaymentMethod { get; set; }
    }
}
