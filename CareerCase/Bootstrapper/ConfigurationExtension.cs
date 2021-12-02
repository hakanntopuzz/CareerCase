using CareerCase.Domain.Settings;
using CareerCase.Infrastructure;
using CareerCase.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CareerCase.Bootstrapper
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = new ConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

            #region Database

            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseNpgsql(new NpgsqlConnectionStringBuilder(connectionStrings.ApplicationDbConnection).ConnectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDbInitializer, DbInitializer>();

            #endregion

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            #region Redis
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost:6379";
            });
            #endregion

            //services.Configure<AWSServiceConfiguration>(configuration.GetSection("AWSS3Configuration"));

            return services;
        }
    }
}