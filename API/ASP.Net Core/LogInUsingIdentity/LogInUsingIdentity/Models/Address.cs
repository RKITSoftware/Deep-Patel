using System.ComponentModel.DataAnnotations;

namespace LogInUsingIdentity.Models
{
    public class Address
    {
        [Key]
        public int? Id { get; set; }

        public string Street { get; set; }
    }
}
