using Microsoft.AspNetCore.Mvc;

namespace Br.Com.Merlot.Web.Controllers
{
    public class CategoriaController(ILogger<CategoriaController> logger) : Controller
    {
        private readonly ILogger<CategoriaController> _logger = logger;

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
