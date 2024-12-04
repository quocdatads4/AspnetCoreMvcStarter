using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  [Area("User")]
  public class ProfileGroupsController : Controller
  {
    
    private readonly ProfileGroupsBLL _profileGroupsBLL;

    public ProfileGroupsController(ProfileGroupsBLL profileGroupsBLL)
    {
      _profileGroupsBLL = profileGroupsBLL;
    }

    // GET: User/ProfileGroups
    public IActionResult Index()
    {
      var data = _profileGroupsBLL.GetAll(); // Returns IEnumerable<ProfileGroupsDTO>
      var userMainDTO = new _UserMainDTO
      {
        ProfileGroupsList = data.ToList() // Assuming ProfileGroups is a property of type List<ProfileGroupsDTO>
      };
      return View(userMainDTO);
    }

    // GET: User/ProfileGroups/Details/5
    public IActionResult Details(int id)
    {
      var data = _profileGroupsBLL.GetProfileGroupById(id);
      if (data == null)
      {
        return NotFound();
      }
      return View(data);
    }

    // GET: User/ProfileGroups/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: User/ProfileGroups/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProfileGroupsDTO profileGroup)
    {
      if (ModelState.IsValid)
      {
        profileGroup.CreatedDate = DateTime.UtcNow;
        _profileGroupsBLL.Add(profileGroup);
        return RedirectToAction(nameof(Index));
      }
      return View(profileGroup);
    }

    // GET: User/ProfileGroups/Edit/5


    [HttpGet]
    public IActionResult Edit(int id)
    {
      var getdata = _profileGroupsBLL.GetProfileGroupById(id);
      var listdata = _profileGroupsBLL.GetAll();
      if (getdata == null)
      {
        return NotFound();
      }

      var userMainDto = new _UserMainDTO
      {
        ProfileGroupsDTO = getdata,
        ProfileGroupsList = listdata.ToList()
      };

      return View(userMainDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Edit(_UserMainDTO userMainDTO)
    {

      if (!ModelState.IsValid)
      {
        return View(userMainDTO);
      }

      var updatedDTO = userMainDTO.ProfileGroupsDTO;
      if (updatedDTO == null)
      {
        return BadRequest();
      }
      updatedDTO.CreatedDate = DateTime.UtcNow;
      _profileGroupsBLL.Update(updatedDTO);
      var profileGroupsList = _profileGroupsBLL.GetAll();
      return View("Index", new _UserMainDTO { ProfileGroupsList = profileGroupsList });
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
      var data = _profileGroupsBLL.GetProfileGroupById(id);
      if (data == null)
      {
        return NotFound();
      }

      _profileGroupsBLL.Delete(id);
      return RedirectToAction(nameof(Index));
    }

  }
}
