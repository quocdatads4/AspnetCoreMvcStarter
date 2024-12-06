using AspnetCoreMvcStarter.Data;
using AspnetCoreMvcStarter.Models;
using System;

namespace AspnetCoreMvcStarter.Areas.User.DataAccessLayer
{
  public class ProfileOrbitasDAL
  {
    private readonly ApplicationDbContext _context;

    public ProfileOrbitasDAL(ApplicationDbContext context)
    {
      _context = context;
    }
    public List<ProfileOrbitasDTO> GetAll()
    {
      return _context.ProfileOrbitas.ToList();
    }

    public ProfileOrbitasDTO GetById(int id)
    {
      return _context.ProfileOrbitas.FirstOrDefault(pg => pg.Id == id);
    }

    public void Add(ProfileOrbitasDTO modelDTO)
    {

      _context.ProfileOrbitas.Add(modelDTO);
      _context.SaveChanges();
    }

    public void Update(ProfileOrbitasDTO modelDTO)
    {
      _context.ProfileOrbitas.Update(modelDTO);
      _context.SaveChanges();
    }

    public void Delete(int id)
    {
      var data = _context.ProfileOrbitas.FirstOrDefault(pg => pg.Id == id);
      if (data != null)
      {
        _context.ProfileOrbitas.Remove(data);
        _context.SaveChanges();
      }
    }


  }
}
