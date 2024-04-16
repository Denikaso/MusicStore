using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Delivery")]
    public class Delivery
    {
        [PrimaryKey, Identity] public int Id { get; set; }
        [Column("OrderId"), NotNull] public int OrderId { get; set; }
        [Association(ThisKey = nameof(OrderId), OtherKey = nameof(Order.Id))]
        public Order order { get; set; }
        [Column("DeliveryDate"), NotNull] public DateTime DeliveryDate { get; set; }
        [Column("Address"), NotNull] public string Address { get; set; }
        [Column("Status"), NotNull] public bool Status { get; set; }
        [Column("Price"), NotNull] public float Price { get; set; }
    }
}
