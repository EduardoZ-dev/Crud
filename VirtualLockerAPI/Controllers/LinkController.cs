using Microsoft.AspNetCore.Mvc;
using VirtualLockerAPI.Models;

namespace VirtualLockerAPI.Controllers
{
    public class LinkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinkController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LinkSubmission linkSubmission)
        {
            if (ModelState.IsValid)
            {
                _context.LinkSubmissions.Add(linkSubmission);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetList", "Item");
            }
            return View(linkSubmission);
        }
    }
}
