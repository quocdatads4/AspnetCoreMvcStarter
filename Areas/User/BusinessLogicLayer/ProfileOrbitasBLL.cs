using AspnetCoreMvcStarter.Areas.User.DataAccessLayer;
using AspnetCoreMvcStarter.Models;

namespace AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer
{
  public class ProfileOrbitasBLL
  {
    private readonly ProfileOrbitasDAL _dal;

    public ProfileOrbitasBLL(ProfileOrbitasDAL DAL)
    {
      _dal = DAL;
    }
    public List<ProfileOrbitasDTO> GetAll()
    {
      return _dal.GetAll();
    }

    public ProfileOrbitasDTO GetById(int id)
    {
      return _dal.GetById(id);
    }

    public void Add(ProfileOrbitasDTO modelDTO)
    {
      _dal.Add(modelDTO);
    }

    public void Update(ProfileOrbitasDTO modelDTO)
    {
      _dal.Update(modelDTO);
    }

    public void Delete(int id)
    {
      _dal.Delete(id);
    }
  }
}
