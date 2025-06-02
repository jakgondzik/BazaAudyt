using Microsoft.AspNetCore.Mvc;

namespace BazaAudyt.Controllers
{
    public class KontroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
