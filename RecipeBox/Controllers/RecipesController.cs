using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace RecipeBox.Controllers
{
  [Authorize]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public RecipesController(UserManager<Application> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      string userId = userId.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Recipe> userRecipes = _db.Recipes
        .Where(entry => entry.User.Id == currentUser.Id)
        .Include(RecipeBox => RecipeBox.JoinEntities)
        .ThenInclude(join => join.Tag)
        .ToList();
      return View(userRecipes);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe, int RecipeId)
    {
      if (!ModelState.IsValid)
      {
        return View(recipe);
      }
      else
      {
        string userId = UserId.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        recipe.User = currentUser;
        _db.Recipes.Add(recipe);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    public ActionResult Details(int Id)
    {
      Recipe thisRecipe = _db.Recipes
        .Include(recipe => recipe.Recipe)
        .Include(Account => Account.JoinEntities)
        .ThenInclude(join => join.Tag)
        .FistOrDefault(thisAccount => thisAccount.AccountId == id);
      return View(thisAccount);
    }
      
    public ActionResult Edit(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.recipeId == id);
      return View (thisAccount);
    }
    

    [HttpPost]
    public ActionResult Edit(Recipe recipe)
    {
      _db.Recipes.Update(recipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Delete(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    } 

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddTag(int id)
    {
      string userId = userId.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Tag> userTags = _db.Tag
        .Where(entry => entry.User.Id == currentUser.Id)
        .Include(recipe => recipe.JoinEntities)
        .ToList();
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipes.RecipeId == id);
      ViewBag.TagId = new SelectList(userTags, "TagId", "Title");
      return View(thisAccount);
    }

    [HttpPost]
    public ActionResult AddTag(Recipe recipe, int tagId)
    {
      #nullable enable
      RecipeTag? joinEntity = _db.AccountTags.FirstOrDefault(join => (join.TagId == tagId && join.RecipeId == recipe.RecipeId));
      #nullable disable
      if (joinEntity == null && tagId != 0)
      {
        _db.RecipeTags.Add(new RecipeTag() {TagId = tagId, RecipeId = recipe.RecipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = recipe.RecipeId});
    }
    
    [HttpPost]
    public ActionResult DeleteJoin(int JoinId)
    {
      RecipeTag entry = _db.RecipeTags.FirstOrDefault(entry => entry.RecipeTagId == joinId);
      _db.RecipeTags.Remove(entry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}