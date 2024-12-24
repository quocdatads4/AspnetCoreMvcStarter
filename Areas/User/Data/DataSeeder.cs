using AspnetCoreMvcStarter.Data;
using AspnetCoreMvcStarter.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspnetCoreMvcStarter.Areas.User.Data
{
  public class DataSeeder
  {
    private readonly ApplicationDbContext _context;

    public DataSeeder(ApplicationDbContext context)
    {
      _context = context;
    }

    public void SeedData()
    {
      var random = new Random();

      for (int i = 0; i < 10; i++)
      {
        var data = new ProfileOrbitasDTO
        {
          Name = "Profile " + (i + 1),
          ProfileGroupID = i + 1
  };

        _context.ProfileOrbitas.Add(data);
      }

      _context.SaveChanges();
    }
  }
}

