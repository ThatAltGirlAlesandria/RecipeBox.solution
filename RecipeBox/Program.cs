using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeBox.Models;

namespace RecipeBox
{
  class Program
  {
    static void Main(string[] args)
    {

      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllersWithViews();

      builder.Services.AddDbContext<RecipeBoxContext>(dbContextOptions => dbContextOptions.UseMySql(builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"])));

      builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<RecipeBoxContext>().AddDefaultTokenProviders();

      builder.Services.Configure<IdentityOptions(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLength = 0;
        options.Password.RequireUniqueCharacter = 0;
      });

      WebApplication app = builder.Build();

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication(); 
      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}"
        );

      app.Run();
    }
  }
}