namespace Ecommerce.Web.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.ADMIN.ToString()});
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.USER.ToString()});
            }
        }
    }
}
