using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
using AspnetCoreMvcStarter.Migrations;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  [Area("User")]
  public class ProfileOrbitasController : Controller
  {
    private readonly SocialGroupsService<AccountSocialGroupsDTO> _bll_accountsocialgroups;
    private readonly SocialGroupsService<AccountSocialTypeDTO> _bll_accountsocialtype;
    private readonly ProfileOrbitasService<ProfileOrbitasDTO> _bll_profileorbitas;
    public ProfileOrbitasController(
                                      SocialGroupsService<AccountSocialGroupsDTO> bll_accountsocialgroups,
                                      SocialGroupsService<AccountSocialTypeDTO> bll_accountsocialtype,
                                      ProfileOrbitasService<ProfileOrbitasDTO> bll_profileorbitas)
    {
      _bll_accountsocialgroups = bll_accountsocialgroups;
      _bll_accountsocialtype = bll_accountsocialtype;
      _bll_profileorbitas = bll_profileorbitas;
    }
    public IActionResult List()
    {
      // Đặt tên cột
      ViewBag.ColumnNames = new List<string> { "Chọn", "ID", "Tên", "Ngày tạo", "Trạng thái", "Địa chỉ IP", "Quốc gia", "Nhóm tài khoản", "Hành động" };

      // Lấy tất cả các nhóm tài khoản và thông tin hồ sơ
      var accountSocialGroups = _bll_accountsocialgroups.GetAll();
      var accountSocialType = _bll_accountsocialtype.GetAll();
      var profileOrbitas = _bll_profileorbitas.GetAll();


      // Lấy thông tin chi tiết loại tài khoản từ AccountTypeID của từng nhóm
      var profileOrbitasDetails = profileOrbitas.Select(profileO =>
      {
        if (profileO is ProfileOrbitasDTO profileOrbitas)
        {
          var accountgroup = accountSocialGroups.FirstOrDefault(at => at.Id == profileOrbitas.ProfileGroupID);
          return new
          {
            ProfileOrbitas = profileOrbitas,
            GroupName = accountgroup?.Name ?? "N/A" // Lấy tên loại tài khoản hoặc gán "N/A" nếu không tìm thấy
          };
        }
        return null;
      }).Where(detail => detail != null).ToList();

      // Tạo model
      var model = new _UserMainDTO
      {
        //ProfileOrbitasList = profileOrbitasDetails.Select(d => d.Group).ToList(),
        ProfileOrbitasDetails = profileOrbitasDetails.Select(d => new
        {
          d.ProfileOrbitas.Id,
          d.ProfileOrbitas.Name,
          d.ProfileOrbitas.CreatedDate,
          d.ProfileOrbitas.IPAddress,
          d.ProfileOrbitas.Country,
          d.ProfileOrbitas.Status,
          d.GroupName
        }).ToList()
      };

      // Đặt dữ liệu loại tài khoản vào ViewBag
      ViewBag.AccountSocialGroups = accountSocialGroups.Select(at => new SelectListItem
      {
        Value = at.Id.ToString(),
        Text = at.Name
      }).ToList();

      // Đặt dữ liệu loại tài khoản vào ViewBag
      ViewBag.AccountSocialType = accountSocialType.Select(at => new SelectListItem
      {
        Value = at.Id.ToString(),
        Text = at.Name
      }).ToList();

      // Trả về view
      return View("ProfileOrbitasShared", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
      var result = await _bll_profileorbitas.DeleteAsync(id);
      if (result)
      {
        TempData["Success"] = "Deleted successfully!";
      }
      else
      {
        TempData["Error"] = "Entity not found.";
      }

      return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      try
      {
        // Lấy dữ liệu hồ sơ từ service
        var data = _bll_profileorbitas.GetById(id);
        var accountSocialGroups = _bll_accountsocialgroups.GetAll();
        var profileOrbitas = _bll_profileorbitas.GetAll();

        if (data == null)
        {
          return NotFound("Không tìm thấy hồ sơ với ID được cung cấp.");
        }


        // Chuyển dữ liệu sang DTO hoặc ViewModel (nếu cần)
        var model = new _UserMainDTO
        {
          ProfileOrbitas = new ProfileOrbitasDTO
          {
            Id = data.Id,
            Name = data.Name,
          }
        };
        
        // Sử dụng hàm để tạo danh sách SelectListItem
        ViewBag.AccountSocialGroups = GenerateSelectListItems(
            accountSocialGroups,
            at => at.Id.ToString(),
            at => at.Name
        );
        // Trả về view
        return View("ProfileOrbitasEdit", model);
      }
      catch (Exception ex)
      {
        // Xử lý lỗi và hiển thị thông báo
        ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi: " + ex.Message);
        return View();
      }
    }

    [HttpPost]
    public IActionResult Edit(_UserMainDTO model)
    {
      if (!ModelState.IsValid)
      {
        return View(model); // Trả lại form nếu có lỗi
      }
      // Tìm và cập nhật dữ liệu
      var data = _bll_profileorbitas.GetById(model.ProfileOrbitas.Id);

      if (data == null)
      {
        return NotFound("Không tìm thấy hồ sơ để cập nhật.");
      }
      data.Name = model.ProfileOrbitas.Name;

      data.CreatedDate = DateTime.Now;
      try
      {
        _bll_profileorbitas.Update(data);  // Gọi phương thức Update từ service
        // Trả về view
        return RedirectToAction("List");
      }
      catch (Exception ex)
      {
        // Xử lý lỗi nếu có
        ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi: " + ex.Message);
        // Trả về view
        return RedirectToAction("List");
      }
    }

    public List<SelectListItem> GenerateSelectListItems<T>(
    IEnumerable<T>? items,
    Func<T, string> valueSelector,
    Func<T, string?> textSelector)
    {
      if (items == null)
        return new List<SelectListItem>();

      return items.Select(item => new SelectListItem
      {
        Value = valueSelector(item),
        Text = textSelector(item) ?? string.Empty // Cung cấp giá trị mặc định nếu null
      }).ToList();
    }

  }
}
