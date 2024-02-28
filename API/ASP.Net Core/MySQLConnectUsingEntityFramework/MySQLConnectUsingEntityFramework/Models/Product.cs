using System.ComponentModel.DataAnnotations;

namespace MySQLConnectUsingEntityFramework.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
