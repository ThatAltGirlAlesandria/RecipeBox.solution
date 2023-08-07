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
    public class RecipesController : Controller
    {
        private readonly RecipeBoxContext _db;
        public RecipesController(RecipeBoxContext db)
        {
            _db = db
        }
        public ActionResult Index()
        {
            List<Recipe> model = _db.Accounts.Include(recipe => recipe.Account).ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.AccountsId = new SelectList(_db., "AccountId", "AccountName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "AccountName");
                return View(account);
            }
            else
            {
              _db.Accounts.Add (account);
              -_db.SaveChanges();
              return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int Id)
        {
          Account thisAccount = _db.Accounts
            .Include(Account => item.Recipe)
            .Include(Account => Account.JoinEntities)
            .ThenInclude(join => join.Tag)
            .FistOrDefault(thisAccount => thisAccount.AccountId == id);
            return View(thisAccount);
        }

        public ActionResult Edit(int id)
        {
          Account thisAccount = _db.Aaccounts.FirstOrDefault(account => account.accountId == id);
          ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
          return View (thisAccount);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
          _db.Accounts.Update(account);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
          Account thisAccount = _db.Accounts.FirstOrrDefult(account => account.AccountId == id);
          return View(thisItem);
        } 


    }
}