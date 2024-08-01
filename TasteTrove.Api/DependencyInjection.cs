

using Microsoft.AspNetCore.Mvc.Infrastructure;
using TasteTrove.Api.Common.Errors;
using TasteTrove.Api.Mapping;

namespace TasteTrove.Application;
  public static class DependencyInjection
  {
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

    services.AddControllers();
    services.AddSingleton<ProblemDetailsFactory, TasteTroveProblemDetailsFactory>();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddMappings();
    return services;
    }
  }
