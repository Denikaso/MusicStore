using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Supplier")]
    public class Supplier
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("Name"), NotNull]
        public string Name { get; set; }
        [Column("Email"), NotNull]
        public string Email { get; set; }
        [Column("PhoneNumber"), NotNull]
        public string PhoneNumber { get; set; }
    }
}
