using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualLockerAPI.DTO_s;
using VirtualLockerAPI.Models;


namespace VirtualLockerAPI.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public  ProductDTO ProductDTO { get; set; } = new ProductDTO();

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Brand = productDto.Brand,
                    Category = productDto.Category,
                    Price = productDto.Price,
                    Description = productDto.Description,
                    CreatedAt = DateTime.UtcNow
                };

                if (productDto.ImageFileName != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + productDto.ImageFileName.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.ImageFileName.CopyToAsync(fileStream);
                    }

                    product.ImageFileName = "images/" + uniqueFileName;
                }
                else
                {
                    product.ImageFileName = "images/default.png";
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Producto creado exitosamente.";
                return RedirectToAction("GetList");
            }

            TempData["ErrorMessage"] = "Por favor, corrija los errores en el formulario.";
            return View(productDto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Product product = await _context.Products.FirstAsync(x => x.Id == Id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile imageFileName)
        {

            var existingProduct = await _context.Products.FindAsync(product.Id);

            if(existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Brand = product.Brand;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;


            if(imageFileName != null && imageFileName.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/images", imageFileName.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFileName.CopyToAsync(stream);
                }
                existingProduct.ImageFileName = imageFileName.FileName;
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("GetList");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return NotFound();
            }
            
            return View(product);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(product.ImageFileName))
            {
                var filePath = Path.Combine("wwwroot/images", product.ImageFileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetList");

        }

        public IActionResult GetList()
        {

            Products = _context.Products.OrderByDescending(p => p.Id).ToList();

            return View(Products);
        } 

    }
}
