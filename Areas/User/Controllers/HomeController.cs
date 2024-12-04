using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  [Area("User")]
  public class HomeController : Controller
  {
    
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult DatatablesBasic() => View();
  }
}
