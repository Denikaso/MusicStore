using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("CartItem")]
    public class CartItem
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("CartId"), NotNull]
        public int CartId { get; set; }
        [Column("ProductId"), NotNull]
        [Association(ThisKey = nameof(CartId), OtherKey = nameof(Cart.Id))]
        public Cart cart { get; set; }
        public int ProductId { get; set; }
        [Column("Quantity"), NotNull]
        [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id))]
        public Product product { get; set; }
        public int Quantity { get; set; }

    }
}
