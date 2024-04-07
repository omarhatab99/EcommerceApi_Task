namespace Ecommerce.Web.Core.Dtos
{
    public class LoginUserDTO
    {
		[Required(ErrorMessageResourceType = typeof(Resources.GeneralResource), ErrorMessageResourceName = "RequiredMSGERROR")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessageResourceType = typeof(Resources.GeneralResource), ErrorMessageResourceName = "RequiredMSGERROR")]
        public string Password { get; set;}  = null!;
    }
}
