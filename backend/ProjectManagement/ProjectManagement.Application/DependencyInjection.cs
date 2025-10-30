using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Services;

namespace ProjectManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<AccountService>();
            return services;
        }
    }
}
