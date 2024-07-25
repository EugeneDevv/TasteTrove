using System.Reflection;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TasteTrove.Application.Authentication.Commands.Register;
using TasteTrove.Application.Authentication.Common;
using TasteTrove.Application.Common.Behaviors;

namespace TasteTrove.Application;
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

    services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    services.AddScoped(
        typeof(IPipelineBehavior<,>),
        typeof(ValidationBehavior<,>));

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    return services;
    }
  }
