using AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer;
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

builder.Services.AddScoped<ProfileGroupsBLL>();
builder.Services.AddScoped<ProfileGroupsDAL>();

var app = builder.Build();

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
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
