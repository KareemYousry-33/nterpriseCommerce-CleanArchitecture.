using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseCommerce.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // تسجيل كل الـ Handlers بتاعة MediatR تلقائياً من المشروع ده
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // تسجيل كل كلاسات الـ Validators بتاعة FluentValidation تلقائياً
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}