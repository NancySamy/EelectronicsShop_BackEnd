using LinkDevelopmentWorkshop.Application.Constants;
using LinkDevelopmentWorkshop.Security.IOC;
using Microsoft.AspNetCore.Authentication;

namespace LinkDevelopmentWorkshop.Security.Helpers
{
    public static class SecurityStartupHelpers
    {
        public static AuthenticationBuilder AddAuthenticationSupport(this IServiceCollection services)
        {
            return services.HandleCustomAuthentication();
        }

        public static IServiceCollection AddAuthorizationSupport(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    AuthorizationPolicies.ProcessAdministrators,
                    policy => policy.RequireRole(CustomRoles.Adminstrator));

                options.AddPolicy(
                    AuthorizationPolicies.CustomerUser,
                    policy => policy.RequireRole(CustomRoles.User));

                options.AddPolicy(AuthorizationPolicies.ProcessAdministratorsOrCustomerUser, policy => policy.RequireAssertion(
                      context =>
                      context.User.IsInRole(CustomRoles.Adminstrator) ||
                      context.User.IsInRole(CustomRoles.User)));




            });

            return services;

          
        }
    }
}
