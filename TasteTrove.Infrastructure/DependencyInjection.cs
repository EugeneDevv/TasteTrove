using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
    services.AddAuth(configuration);
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }

  public static IServiceCollection AddAuth(
    this IServiceCollection services,
    ConfigurationManager configuration
  )
  {
    var jwtSettings = new JwtSettings();

    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    services.AddSingleton(Options.Create(jwtSettings));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtSettings.Issuer,
      ValidAudience = jwtSettings.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtSettings.Secret)
      )
    });

    return services;
  }
}
