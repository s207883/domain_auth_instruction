using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MyApp.Constants;

namespace MyApp.WebApi
{
    public static class MixedAuthenticationExtensions
    {
        public static AuthenticationBuilder AddMixed(this AuthenticationBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<CookieAuthenticationOptions>, PostConfigureCookieAuthenticationOptions>());
            return builder.AddScheme<CookieAuthenticationOptions, MixedAuthenticationHandler>(AuthentificationContants.AuthenticationScheme, AuthentificationContants.AuthenticationScheme, null);
        }
    }
}
