using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspnetCoreMvcStarter.Models
{
  [Area("User")]
  public class _UserMainDTO
  {
    public IEnumerable<SelectListItem>? ProfileGroupSelectList { get; set; }

    public ProfileOrbitasDTO? ProfileOrbitas { get; set; }
    public List<ProfileOrbitasDTO>? ProfileOrbitasList { get; set; }
    public IEnumerable<AccountSocialGroupsDTO>? AccountSocialGroups { get; set; }
    public IEnumerable<object>? AccountSocialGroupDetails { get; set; }

    public IEnumerable<object>? ProfileOrbitasDetails { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

  }
}
