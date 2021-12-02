using CareerCase.Core.Services;
using CareerCase.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CareerCase.Bootstrapper
{
    public static class BusinessServiceExtension
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IUnfavorableWordService, UnfavorableWordService>();
            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}