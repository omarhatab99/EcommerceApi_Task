namespace Ecommerce.Web.Seeds
{
    public class DefaultUsers
    {

        public static async Task SeedAdminUser(UserManager<ApplicationUser> userManager , ApplicationDbContext context)
        {
            using var transaction =  context.Database.BeginTransaction();

            ApplicationUser adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@Ecommerce.com",
                EmailConfirmed = true,
                PhoneNumber = "01004382527",
                PhoneNumberConfirmed = true,
            };

            try
            {
                ApplicationUser? user = await userManager.FindByNameAsync(adminUser.UserName);

                if (user == null)
                {

                    //add user admin
                    IdentityResult result = await userManager.CreateAsync(adminUser, "Omarhatab2023$");
                    if (result.Succeeded)
                        await userManager.AddToRolesAsync(adminUser, new List<string> {Roles.ADMIN.ToString()});
                }

                transaction.Commit();

            }
            catch (Exception)
            {

                transaction.Rollback();
            }


        }
    }
}
