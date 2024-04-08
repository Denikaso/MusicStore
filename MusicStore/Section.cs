using LinqToDB.Mapping;

namespace MusicStore
{
    [Table("Section")]
    public class Section
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("Title"), NotNull]
        public string Title { get; set; }
    }       
}