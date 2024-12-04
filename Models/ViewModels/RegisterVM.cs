using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcStarter.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "The Password and confirmation password do not math")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //public string ConfirmPassword { get; set; }
    }
}
