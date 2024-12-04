using AspnetCoreMvcStarter.Areas.User.DataAccessLayer;
using AspnetCoreMvcStarter.Models;

namespace AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer
{
  public class ProfileGroupsBLL
  {
    private readonly ProfileGroupsDAL _dal;

    public ProfileGroupsBLL(ProfileGroupsDAL profileGroupsDAL)
    {
      _dal = profileGroupsDAL;
    }

    public List<ProfileGroupsDTO> GetAll()
    {
      return _dal.GetAll();
    }

    public ProfileGroupsDTO GetProfileGroupById(int id)
    {
      return _dal.GetById(id);
    }

    public void Add(ProfileGroupsDTO profileGroup)
    {
      _dal.Add(profileGroup);
    }

    public void Update(ProfileGroupsDTO profileGroup)
    {
      _dal.Update(profileGroup);
    }

    public void Delete(int id)
    {
      _dal.Delete(id);
    }
  }
}
