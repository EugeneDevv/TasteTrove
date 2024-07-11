using Microsoft.Extensions.DependencyInjection;
using TasteTrove.Application.Services.Authentication;

namespace TasteTrove.Application;
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
  }
