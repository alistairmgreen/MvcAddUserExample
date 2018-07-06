using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcAddUserExample.Models
{
    /// <summary>
    /// Viewmodel for the user registration form.
    /// </summary>
    public class UserRegistrationViewModel
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [DisplayName("Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a second copy of the password which the user
        /// must reenter to guard against typing errors.
        /// </summary>
        [DisplayName("Retype password to confirm")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}