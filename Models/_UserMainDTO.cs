using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Models
{
  [Area("User")]
  public class _UserMainDTO
  {
    public ProfileGroupsDTO? ProfileGroupsDTO { get; set; }
    public List<ProfileGroupsDTO>? ProfileGroupsList { get; set; }
   
  }
}
