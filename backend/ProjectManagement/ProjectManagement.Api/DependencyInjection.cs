using ProjectManagement.Application;
using ProjectManagement.Infrastructure;

namespace ProjectManagementAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureDI(configuration);
            services.AddApplicationDI();
            return services;
        }

    }
}
