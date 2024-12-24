using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  public class FacebookAccountsController : Controller
  {
  
    public IActionResult Add()
    {
    
      return View();
    }
  }
}
