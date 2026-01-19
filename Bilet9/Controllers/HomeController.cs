using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Bilet9.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
