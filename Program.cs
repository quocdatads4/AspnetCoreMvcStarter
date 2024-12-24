using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
using AspnetCoreMvcStarter.Areas.User.Data;
using AspnetCoreMvcStarter.Areas.User.DataAccessLayer;
using AspnetCoreMvcStarter.Data;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<IdentityOptions>(otp =>
{
  otp.Password.RequireDigit = false;
  otp.Password.RequireLowercase = false;
  otp.Password.RequireNonAlphanumeric = false;
  otp.Lockout.MaxFailedAccessAttempts = 3;
  otp.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
  otp.SignIn.RequireConfirmedEmail = false;
}
);

builder.Services.AddScoped(typeof(ProfileOrbitasRepository<>));
builder.Services.AddScoped(typeof(ProfileOrbitasService<>));

builder.Services.AddScoped<ProfileOrbitasBLL>();
builder.Services.AddScoped<ProfileOrbitasDAL>();

//builder.Services.AddScoped(typeof(AccountSocialGroupsRepository<>));
//builder.Services.AddScoped(typeof(AccountSo<>));

builder.Services.AddScoped(typeof(SocialGroupsRepository<>));
builder.Services.AddScoped(typeof(SocialGroupsService<>));

var app = builder.Build();

// Seed data


//using (var scope = app.Services.CreateScope())
//{
//  var services = scope.ServiceProvider;

//  try
//  {
//    var context = services.GetRequiredService<ApplicationDbContext>();
//    var seeder = new DataSeeder(context);
//    seeder.SeedData();
//  }
//  catch (Exception ex)
//  {
//    // Log or handle the exception as needed
//  }
//}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=ProfileOrbitas}/{action=List}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
