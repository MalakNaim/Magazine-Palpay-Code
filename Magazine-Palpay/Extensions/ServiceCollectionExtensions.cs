using Magazine_Palpay.Web.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Magazine_Palpay.Web.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static T GetOptions<T>(this IServiceCollection services, string sectionName)
          where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection(sectionName);
            var options = new T();
            section.Bind(options);

            return options;
        }

        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services, string ipAddress)
            where T : DbContext
        {
            var options = services.GetOptions<PersistenceSettings>(nameof(PersistenceSettings));
            if (options.UseOracle)
            {
                string connectionString = options.ConnectionStrings.Oracle;
                services.AddOracle<T>(ipAddress, connectionString);
            }

            return services;
        }

        private static IServiceCollection AddOracle<T>(this IServiceCollection services, string ipAddress, string connectionString)
           where T : DbContext
        { 
            services.AddDbContext<T>(m => m.UseOracle(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName).UseOracleSQLCompatibility("11")));
            return services;
        }
    }
}