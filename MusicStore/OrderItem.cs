using LinqToDB.Mapping;
using MusicStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [PrimaryKey, Identity] public int Id { get; set; }
        [Column("OrderId"), NotNull] public int OrderId { get; set; }
        [Association(ThisKey = nameof(OrderId), OtherKey = nameof(Order.Id))]
        public Order order { get; set; }
        [Column("ProductId"), NotNull] public int ProductId { get; set; }
        [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id))]
        public Product product { get; set; }
        [Column("Quantity"), NotNull] public int Quantity { get; set; }
    }
}
