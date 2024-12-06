using AspnetCoreMvcStarter.Data;
using AspnetCoreMvcStarter.Models;
using System;

namespace AspnetCoreMvcStarter.Areas.User.DataAccessLayer
{
  public class ProfileGroupsDAL
  {
    private readonly ApplicationDbContext _context;
    
    public ProfileGroupsDAL(ApplicationDbContext context)
    {
      _context = context;
    }

    public List<ProfileGroupsDTO> GetAll()
    {
      return _context.ProfileGroups.ToList();
    }

    public ProfileGroupsDTO GetById(int id)
    {
      return _context.ProfileGroups.FirstOrDefault(pg => pg.Id == id);
    }

    public void Add(ProfileGroupsDTO modelDTO)
    {

      _context.ProfileGroups.Add(modelDTO);
      _context.SaveChanges();
    }

    public void Update(ProfileGroupsDTO modelDTO)
    {
      _context.ProfileGroups.Update(modelDTO);
      _context.SaveChanges();
    }

    public void Delete(int id)
    {
      var data = _context.ProfileGroups.FirstOrDefault(pg => pg.Id == id);
      if (data != null)
      {
        _context.ProfileGroups.Remove(data);
        _context.SaveChanges();
      }
    }


  }
}
