using System.Diagnostics;
using BazaAudyt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using static System.Net.Mime.MediaTypeNames;

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
            //return RedirectToAction("Index","Audyty");
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
            return RedirectToAction("Index", "Audyty");
            //return View();
            //return RedirectToAction("Succes");
        }

        [HttpPost]
        public async Task<IActionResult> Konto(Konta model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Fail");
            }

            var user = _context.Konta.FirstOrDefault(s => s.Login == model.Login);

            if (user == null)
            {
                return RedirectToAction("Fail");
            }

            if (user.Password.Trim() != model.Password)
            {
                return RedirectToAction("Fail");
            }

            return RedirectToAction("Success");
        }
        
        public IActionResult Success()
        {
            return RedirectToAction("Index", "Audyty");
        }

        public IActionResult Fail()
        {
            return RedirectToAction("Index");
        }
    }
}
