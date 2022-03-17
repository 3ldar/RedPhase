using System.Reflection;
using RedPhase.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RedPhase.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddAutoMapper(Assembly.GetExecutingAssembly())
         .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
         .AddMediatR(Assembly.GetExecutingAssembly())
         .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
         .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>))
         .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
         .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
}
