using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore
{
    [Table("Subcategory")]
    public class Subcategory
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("Title"), NotNull]
        public string Title { get; set; }
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Association(ThisKey = nameof(CategoryId), OtherKey = nameof(Category.Id))]
        public Category Category { get; set; }

    }
}
