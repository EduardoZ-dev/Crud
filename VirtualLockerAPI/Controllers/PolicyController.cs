using Microsoft.AspNetCore.Mvc;

namespace VirtualLockerAPI.Controllers
{
    public class PolicyController : Controller
    {
        public IActionResult Policy()
        {
            return View();
        }
    }
}
