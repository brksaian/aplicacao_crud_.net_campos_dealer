using Microsoft.AspNetCore.Mvc;

namespace Teste.Controllers
{
    public class VendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
