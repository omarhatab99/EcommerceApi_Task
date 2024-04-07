namespace Ecommerce.Web.Core.Dtos
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessageResourceType = typeof(Resources.GeneralResource) , ErrorMessageResourceName = "RequiredMSGERROR")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Resources.GeneralResource), ErrorMessageResourceName = "RequiredMSGERROR")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.GeneralResource) , ErrorMessageResourceName = "EmailMSGERROR")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Resources.GeneralResource), ErrorMessageResourceName = "RequiredMSGERROR")]
        [MinLength(5 ,ErrorMessageResourceType = typeof(Resources.GeneralResource), ErrorMessageResourceName = "PasswordLENMSGERROR")]
        public string Password { get; set; } = null!;


    }
}
