using System.ComponentModel.DataAnnotations;

namespace KudevolveWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
       
        public string Email { get; set; }

       
        public string Password { get; set; }

        
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
       
        public string UserName { get; set; }

       
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string County { get; set; }

       
        public string ConfirmPassword { get; set; }

         
        public string DateOfBirth { get; set; }

        
        public string PhoneNumber { get; set; }
    }
}
