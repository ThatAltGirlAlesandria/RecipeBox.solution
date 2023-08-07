using System.ComponentModel.DataAnnotation;

namespace RecipeBox.ViewModels
{
  public class LogInViewModel
  {
    [Required]
    [EmailAddress]
    [Display(nameof = "Email Address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}