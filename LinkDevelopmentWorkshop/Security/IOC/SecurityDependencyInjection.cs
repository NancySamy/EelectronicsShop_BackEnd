using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDevelopmentWorkshop.Security.IOC
{
    public static class SecurityDependencyInjection
    {
        public static AuthenticationBuilder HandleCustomAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(SecurityCustomConstants.AuthenticationScheme)
                           .AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(SecurityCustomConstants.AuthenticationScheme, options =>
                           {

                           });
        }
    }
}
