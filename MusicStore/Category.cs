using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("Title"), NotNull]
        public string Title { get; set; }
        [Column("SectionId"), NotNull]
        public int SectionId { get; set; }

        [Association(ThisKey = nameof(SectionId), OtherKey = nameof(Section.Id))]
        public Section Section { get; set; }
    }
}
