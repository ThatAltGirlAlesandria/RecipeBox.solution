using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using RecipeBox.Models;
using RecipeBox.ViewModels;

namespace RecipeBox.Controllers

{
  public class AccountController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SingInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Register ()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        ApplicationUser user = new ApplicationUser { UserName = model.Email};
        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          foreach (IdentityError error in result.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
          return View(model);
        }
      }
    }
    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LogInViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockOutOnFailure: false);
        if (result.Succeeded)
        {
          return RedirectToAction("Index", "Home");
        }
        else
        {
          ModelState.AddModelError("", "We do not have an account matching that username and password. Please make sure you spelled both of them correctly.");
          return View(model);
        }
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}