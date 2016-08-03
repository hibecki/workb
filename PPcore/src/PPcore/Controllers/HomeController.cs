using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPcore.Models;

namespace PPcore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PalangPanyaDBContext _context;

        public HomeController(PalangPanyaDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mcode = User.Identity.Name;
            var m = _context.member;
            return RedirectToAction(nameof(membersController.Index), "members");
        }
    }
}
