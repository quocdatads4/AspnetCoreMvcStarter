using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  [Area("User")]
  public class AccountSocialGroupsController : Controller
  {
    private readonly SocialGroupsService<AccountSocialGroupsDTO> _bll;
    private readonly SocialGroupsService<AccountSocialTypeDTO> _blltype;

    public AccountSocialGroupsController(SocialGroupsService<AccountSocialGroupsDTO> bll, SocialGroupsService<AccountSocialTypeDTO> blltype)
    {
      _bll = bll;
      _blltype = blltype;
    }
    public IActionResult List()
    {
      // Đặt tên cột
      ViewBag.ColumnNames = new List<string> { "ID", "Tên", "Ngày tạo", "Loại tài khoản", "Hành động" };

      // Lấy tất cả các nhóm tài khoản
      var groups = _bll.GetAll();

      // Lấy tất cả các loại tài khoản
      var accountTypes = _blltype.GetAll(); // Giả định _blltype có phương thức GetAll()

      // Lấy thông tin chi tiết loại tài khoản từ AccountTypeID của từng nhóm
      var groupDetails = groups.Select(group =>
      {
        if (group is AccountSocialGroupsDTO accountGroup)
        {
          var accountType = accountTypes.FirstOrDefault(at => at.Id == accountGroup.AccountTypeID);
          return new
          {
            Group = accountGroup,
            AccountTypeName = accountType?.Name ?? "N/A" // Lấy tên loại tài khoản hoặc gán "N/A" nếu không tìm thấy
          };
        }
        return null;
      }).Where(detail => detail != null).ToList();

      // Tạo model
      var model = new _UserMainDTO
      {
        AccountSocialGroups = groupDetails.Select(d => d.Group).ToList(),
        AccountSocialGroupDetails = groupDetails.Select(d => new
        {
          d.Group.Id,
          d.Group.Name,
          d.Group.CreatedDate,
          d.AccountTypeName
        }).ToList()
      };
      // Đặt dữ liệu loại tài khoản vào ViewBag
      ViewBag.AccountTypes = accountTypes.Select(at => new SelectListItem
      {
        Value = at.Id.ToString(),
        Text = at.Name
      }).ToList();

      // Trả về view
      return View("AccountSocialGroupsShared", model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
      var result = await _bll.DeleteAsync(id);
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
        // Lấy dữ liệu nhóm từ service
        var data = _bll.GetById(id);

        if (data == null)
        {
          return Json(new
          {
            success = false,
            message = "Không tìm thấy nhóm với ID được cung cấp."
          });
        }

        return Json(new
        {
          success = true,
          data = new
          {
            Id = data.Id,
            Name = data.Name,
            AccountTypeID = data.AccountTypeID
          }
        });
      }
      catch (Exception ex)
      {
        return Json(new
        {
          success = false,
          message = "Đã xảy ra lỗi khi lấy thông tin nhóm. Chi tiết lỗi: " + ex.Message
        });
      }
    }

    [HttpPost]
    public IActionResult Save(AccountSocialGroupsDTO group)
    {
      //if (!ModelState.IsValid)
      //{
      //  TempData["Error"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
      //  return RedirectToAction("List");
      //}

      try
      {
        if (group.Id == 0)
        {
          // Add a new group
          group.CreatedDate = DateTime.Now;
          _bll.Add(group);
          TempData["Success"] = "Nhóm mới đã được thêm thành công.";
        }
        else
        {
          // Update an existing group
          var existingGroup = _bll.GetById(group.Id);
          if (existingGroup == null)
          {
            TempData["Error"] = "Nhóm tài khoản không tồn tại.";
            return RedirectToAction("List");
          }

          existingGroup.Name = group.Name;
          existingGroup.AccountTypeID = group.AccountTypeID;
          _bll.Update(existingGroup);

          TempData["Success"] = "Nhóm đã được cập nhật thành công.";
        }
      }
      catch (Exception ex)
      {
        TempData["Error"] = $"Đã xảy ra lỗi: {ex.Message}";
      }

      return RedirectToAction("List");
    }

    public List<object> GetAccountSocialGroupDetails( IEnumerable<AccountSocialGroupsDTO> _getAccountSocialGroups)
    {
      if (_getAccountSocialGroups == null)
      {
        throw new ArgumentNullException("Danh sách nhóm hoặc loại tài khoản không được null.");
      }

      var accountSocialGroupDetails = _getAccountSocialGroups.Select(group =>
      {
        var accountType = _getAccountSocialGroups.FirstOrDefault(at => at.Id == group.AccountTypeID);
        return new
        {
          Group = group,
          AccountTypeName = accountType?.Name ?? "N/A" // Lấy tên loại tài khoản hoặc gán "N/A" nếu không tìm thấy
        };
      }).Where(detail => detail != null).ToList();

      return accountSocialGroupDetails.Cast<object>().ToList();
    }


  }
}
