using VirtualLockerAPI.DTO_s;

namespace VirtualLockerAPI.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageFileName { get; set; }
        public string SuccessMessage { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
    }
}
