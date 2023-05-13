using Microsoft.Extensions.DependencyInjection;
using RedFalcon.Application.Interfaces.Services;
using RedFalcon.Application.Mappings;
using RedFalcon.Application.Services;

namespace RedFalcon.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IContactServices, ContactServices>();
            return services;
        }
    }
}
