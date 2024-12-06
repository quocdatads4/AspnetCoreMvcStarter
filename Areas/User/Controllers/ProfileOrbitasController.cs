using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  [Area("User")]
  public class ProfileOrbitasController : Controller
  {
    private readonly ProfileOrbitasBLL _dataBLL;

    public ProfileOrbitasController(ProfileOrbitasBLL profileOrbitasBLL)
    {
      _dataBLL = profileOrbitasBLL;
    }

    public IActionResult ProfileOrbitasList()
    {
      var data = _dataBLL.GetAll();
      var userMainDTO = new _UserMainDTO
      {
        ProfileOrbitasList = data.ToList()
      };
      return View(userMainDTO);
    }

    public IActionResult ProfileOrbitasCreate()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProfileOrbitasDTO modelDTO)
    {
      if (ModelState.IsValid)
      {
        _dataBLL.Add(modelDTO);
        return RedirectToAction(nameof(Index));
      }
      return View(modelDTO);
    }

  }
}
