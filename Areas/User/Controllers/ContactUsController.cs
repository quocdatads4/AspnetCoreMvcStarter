using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  public class ContactUsController : Controller
  {
    public IActionResult ContactUsPage()
    {
      return View();
    }
  }
}
