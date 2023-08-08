using Microsoft.AspNetCore.Identity;

namespace RecipeBox.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string Accounts { get; set; }
  }
}