using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Customer")]
    public class Customer
    {
        [PrimaryKey, Identity] public int Id { get; set; }
        [Column("Name"), NotNull] public string Name { get; set; }
        [Column("Email"), NotNull] public string Email { get; set; }
        [Column("PhoneNumber")] public string PhoneNumber { get; set; }
        [Column("Address")] public string Address { get; set; }
        [Column("Password"), NotNull] public string Password { get; set; } 
        [Column("role"), NotNull] public string Role { get; set; } 
    }
}
