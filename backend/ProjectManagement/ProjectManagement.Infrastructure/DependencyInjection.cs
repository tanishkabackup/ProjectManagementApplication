using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Interfaces.Repository;
using ProjectManagement.Infrastructure.Configuration;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Repository;


namespace ProjectManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var dbSettings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                options.UseSqlServer(dbSettings.DefaultConnection);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            return services;
        }
    }
}
