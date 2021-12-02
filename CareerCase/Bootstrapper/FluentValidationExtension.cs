using CareerCase.ActionFilters;
using CareerCase.Domain.Requests.Company;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CareerCase.Bootstrapper
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
        {
            services
                .AddMvc(option => option.Filters.Add<ValidationFilter>())
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<CreateCompanyRequestValidator>();
                });

            return services;
        }
    }
}