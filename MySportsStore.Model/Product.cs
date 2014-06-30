using System.ComponentModel.DataAnnotations;

namespace MySportsStore.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }
    }
}