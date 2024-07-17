using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TasteTrove.Application.Common.Interfaces.Authentication;
using TasteTrove.Application.Common.Interfaces.Persistence;
using TasteTrove.Application.Common.Interfaces.Services;
using TasteTrove.Infrastructure.Authentication;
using TasteTrove.Infrastructure.Persistence;
using TasteTrove.Infrastructure.Services;

namespace TasteTrove.Infrastructure;
public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services,
  ConfigurationManager configuration)
  {

    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    
    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }
}
