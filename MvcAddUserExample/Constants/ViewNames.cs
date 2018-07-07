namespace MvcAddUserExample.Constants
{
    /// <summary>
    /// Names of the Razor views.
    /// </summary>
    public static class ViewNames
    {
        /// <summary>
        /// The initial user registration form (home page).
        /// </summary>
        public const string REGISTRATION_FORM = "RegistrationForm";

        /// <summary>
        /// A notification that the user was successfully added.
        /// </summary>
        public const string REGISTRATION_SUCCESS = "RegistrationSuccess";

        /// <summary>
        /// An error message to be displayed if the user cannot be added.
        /// </summary>
        public const string REGISTRATION_FAILURE = "RegistrationFailure";
    }
}