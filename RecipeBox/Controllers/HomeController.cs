using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    public class HomeController : Controller
{
    [httpGet("/")]
    public ActionResult Index()
        {
            return View();
        }
    }

}