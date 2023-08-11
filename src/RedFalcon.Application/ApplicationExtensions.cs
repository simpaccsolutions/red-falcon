using Microsoft.Extensions.DependencyInjection;
using RedFalcon.Application.Interfaces.Services;
using RedFalcon.Application.Interfaces.Validator;
using RedFalcon.Application.Mappings;
using RedFalcon.Application.Services;
using RedFalcon.Application.Validators;

namespace RedFalcon.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IContactServices, ContactServices>();
            services.AddTransient<IOrganizationServices, OrganizationServices>();

            services.AddTransient<IContactValidator, ContactValidator>();
            services.AddTransient<IOrganizationValidator, OrganizationValidator>();

            return services;
        }
    }
}
