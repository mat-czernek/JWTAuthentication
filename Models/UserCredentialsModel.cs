using System.ComponentModel.DataAnnotations;

namespace JWTWebApiAuth.Models
{
    /// <summary>
    /// Model used to authenticate user in API
    /// </summary>
    public class UserCredentialsModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email {get; set;}

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password {get; set;}
    }
}