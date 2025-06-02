using System.Diagnostics;
using BazaAudyt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BazaAudyt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
            //return RedirectToAction("~/View/Index", "AudytyController");
        }

        [HttpPost]
        public async Task<IActionResult> Konto(Konto model)
        {
            if (ModelState.IsValid)
            {
                var User = from m in _context.Konto select m;
                User = User.Where(s => s.Login.Contains(model.Login));
                if (User.Count() != 0)
                {
                    if (User.First().Password== model.Password)
                    {
                        return RedirectToAction("Success");
                    }
                }
            }
            return RedirectToAction("Fail");
        }

        public IActionResult Success()
        {
            return RedirectToAction("~/View/Index","AudytyController");
            //return View();
        }

        public IActionResult Fail()
        {
            return View();
        }
    }
}
