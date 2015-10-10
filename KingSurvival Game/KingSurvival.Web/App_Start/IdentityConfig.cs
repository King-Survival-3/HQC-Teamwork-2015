namespace KingSurvival.Web
{
    using KingSurvival.Data;
    using KingSurvival.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    //// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<KingSurvivalUser>
    {
        public ApplicationUserManager(IUserStore<KingSurvivalUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<KingSurvivalUser>(context.Get<KingSurvivalDbContext>()));
            
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<KingSurvivalUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<KingSurvivalUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}