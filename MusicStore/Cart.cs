using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Cart")]
    public class Cart
    {
        [PrimaryKey, Identity] public int Id { get; set; }
        [Column("CustomerId"), NotNull] public int CustomerId { get; set; }
        [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Customer.Id))]
        public Customer customer { get; set; }
        [Column("Status"), NotNull] public bool Status { get; set; }
        [Column("TotalPrice"), NotNull] public float TotalPrice { get; set; }
    }
}
