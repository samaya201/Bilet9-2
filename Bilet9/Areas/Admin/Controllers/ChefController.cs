using Microsoft.AspNetCore.Mvc;

namespace Bilet9.Areas.Admin.Controllers;
[Area("Admin")]
public class ChefController : Controller
{
   
    public IActionResult Index()
    {
        return View();
    }
}
