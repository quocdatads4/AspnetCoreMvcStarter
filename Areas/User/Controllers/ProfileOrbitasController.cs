using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  [Area("User")]
  public class ProfileOrbitasController : Controller
  {
    private readonly ProfileOrbitasBLL _profileOrbitasBLL;

    public ProfileOrbitasController(ProfileOrbitasBLL profileOrbitasBLL)
    {
      _profileOrbitasBLL = profileOrbitasBLL;
    }

    public IActionResult Index()
    {
      var data = _profileOrbitasBLL.GetAll(); // Returns IEnumerable<ProfileGroupsDTO>
      var userMainDTO = new _UserMainDTO
      {
        ProfileOrbitasList = data.ToList() // Assuming ProfileGroups is a property of type List<ProfileGroupsDTO>
      };
      return View(userMainDTO);
    }
  
  }
}
