using LinqToDB.Mapping;
using MusicStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Order")]
    public class Order
    {
        [PrimaryKey, Identity] public int Id { get; set; }
        [Column("CustomerId"), NotNull] public int CustomerId { get; set; }
        [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Customer.Id))]
        public Customer customer { get; set; }
        [Column("Status"), NotNull] public int Status { get; set; }
        [Column("Date"), NotNull] public DateTime Date { get; set; }
    }
}
