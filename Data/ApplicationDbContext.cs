using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AspnetCoreMvcStarter.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext()
    {

    }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<ProfileOrbitasDTO> ProfileOrbitas { get; set; }
    public DbSet<FacebookAccountsDTO> FacebookAccounts { get; set; }
    public DbSet<AccountSocialGroupsDTO> AccountSocialGroups { get; set; }
    public DbSet<AccountSocialTypeDTO> AccountSocialTypes { get; set; }
  }
}
