using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Identity;
using AspnetCoreMvcStarter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AspnetCoreMvcStarter.Controllers;
[Area("User")]
public class AuthController : Controller
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;

  public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
  {
    _userManager = userManager;
    _signInManager = signInManager;
  }
  [HttpGet]
  public IActionResult ForgotPasswordBasic() => View();

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ForgotPasswordBasic(ForgotPasswordVM model, string returnurl = null)
  {
   
    return View();
  }
  private bool CheckEmailExists(string email)
  {
    // Kiểm tra email trong database
    return true; // Thay bằng logic thực tế
  }

  private void SaveResetToken(string email, string token)
  {
    // Lưu token vào bảng ResetTokens trong database
    // Thay bằng logic thực tế
  }
  public IActionResult ForgotPasswordCover() => View();
  public IActionResult LoginBasic(string returnurl = null)
  {
    ViewData["ReturnUrl"] = returnurl;
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> LoginBasic(LoginVM model, string returnurl = null)
  {
    ViewData["ReturnUrl"] = returnurl;
    returnurl = returnurl ?? Url.Content("~/");
    if (ModelState.IsValid)
    {
      var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,model.RememberMe, lockoutOnFailure:true);
      if (result.Succeeded)
      {
        return LocalRedirect(returnurl);
      }
      if (result.IsLockedOut)
      {
        return View("Lockout");
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
      }
    }

    return View(model);
  }
  [HttpGet]
  public  IActionResult Lockout()
  {
    return View();
  }
  public IActionResult LoginCover() => View();
  public IActionResult RegisterBasic() => View();

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> RegisterBasic(RegisterVM model)
  {
    if (ModelState.IsValid)
    {
      var user = new ApplicationUser
      {
        UserName = model.Email,
        Email = model.Email,
        Name = model.Name
      };

      var result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");
      }

      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }

    return View(model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> LogoutBasic(RegisterVM model)
  {
    await _signInManager.SignOutAsync();
    return RedirectToAction("LoginBasic", "Auth");
  }
  public IActionResult RegisterCover() => View();
  public IActionResult RegisterMultiSteps() => View();

  [Authorize]
  public IActionResult ResetPasswordBasic() => View();
  public IActionResult ResetPasswordCover() => View();
  public IActionResult TwoStepsBasic() => View();
  public IActionResult TwoStepsCover() => View();
  public IActionResult VerifyEmailBasic() => View();
  public IActionResult VerifyEmailCover() => View();
}
