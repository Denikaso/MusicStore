using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("SubcategoryId")]
        public int SubcategoryId { get; set; }        
        [Association(ThisKey = nameof(SubcategoryId), OtherKey = nameof(Subcategory.Id))]
        public Subcategory Subcategory { get; set; }
        [Column("Title"), NotNull]
        public string Title { get; set; }
        [Column("Description"), NotNull]
        public string Description { get; set; }
        [Column("Price")]
        public double Price { get; set; }
        [Column("UnitsInCart")]
        public int UnitsInCart { get; set; }
        [Column("UnitsInStock")]
        public int UnitsInStock { get; set; }
        [Column("Rating")]
        public double Rating { get; set; }
        [Column("Picture"), NotNull]
        public string Picture { get; set; }
    }
}
