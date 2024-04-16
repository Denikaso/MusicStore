using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Supply")]
    public class Supply
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("SupplierId"), NotNull]
        public int SupplierId { get; set; }
        [Association(ThisKey = nameof(SupplierId), OtherKey = nameof(Supplier.Id))]
        public Supplier supplier { get; set; }
        [Column("ProductId"), NotNull]
        public int ProductId { get; set; }
        [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id))]
        public Product product { get; set; }        
        [Column("PricePerUnit"), NotNull]
        public float PricePerUnit { get; set; }
        [Column("Quantity"), NotNull]
        public int Quantity { get; set; }
        [Column("Date"), NotNull]
        public DateTime Date { get; set; }

    }
}
