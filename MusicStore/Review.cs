using LinqToDB.Mapping;
using MusicStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Review")]
    public class Review
    {
        [PrimaryKey, Identity] public int Id { get; set; }
        [Column("CustomerId"), NotNull] public int CustomerId { get; set; }
        [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Customer.Id))]
        public Customer customer { get; set; }
        [Column("ProductId"), NotNull] public int ProductId { get; set; }
        [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id))]
        public Product product { get; set; }
        [Column("Rating"), NotNull] public int Rating { get; set; }
        [Column("Text"), NotNull] public string Text { get; set; }
        [Column("Date"), NotNull] public DateTime Date { get; set; }
    }
}
