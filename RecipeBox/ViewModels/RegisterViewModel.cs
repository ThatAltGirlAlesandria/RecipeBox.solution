using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RecipeBox.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=, *[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]){6,}$", ErrorMessage = "Your password must be at least six characters long, have a capital letter, a lower case letter, at least one number, and a special character(ex: @$!%*?&)")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The passwords you entered don't match. Please try again.")]
    public string ConfirmPassword { get; set; }
  }
}