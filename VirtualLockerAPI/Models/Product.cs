using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VirtualLockerAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        
        [MaxLength(100)]
        public string Brand { get; set; } = null!;
        
        [MaxLength(100)]
        public string Category { get; set; } = null!;
        
        [Precision(16, 2)]
        public decimal Price { get; set; }
        
        
        public string? Description { get; set; } = null!;
        
        [MaxLength(100)]
        public string ImageFileName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
