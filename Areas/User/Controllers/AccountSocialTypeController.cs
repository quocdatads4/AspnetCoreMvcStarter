using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Areas.User.Controllers
{
  public class AccountSocialTypeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
    public List<object> GetAccountSocialGroupDetails(
    IEnumerable<AccountSocialGroupsDTO> getAllAccountSocialGroups,
    IEnumerable<AccountSocialTypeDTO> getAllAccountSocialTypes)
    {
      if (getAllAccountSocialGroups == null || getAllAccountSocialTypes == null)
      {
        throw new ArgumentNullException("Danh sách nhóm hoặc loại tài khoản không được null.");
      }

      var accountSocialGroupDetails = getAllAccountSocialGroups.Select(group =>
      {
        var accountType = getAllAccountSocialTypes.FirstOrDefault(at => at.Id == group.AccountTypeID);
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
