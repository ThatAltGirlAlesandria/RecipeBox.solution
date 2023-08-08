using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
  public class RecipeBoxContext : DbContext
  {

    // public DbSet<Accounts> Accounts { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<RecipeTag> RecipeTags { get; set; }

    public RecipeBoxContext(DbContextOptions options) : base(options) { }

  }
}