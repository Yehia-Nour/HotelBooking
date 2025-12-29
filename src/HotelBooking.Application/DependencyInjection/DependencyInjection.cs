using FluentValidation;
using HotelBooking.Application.Validators.UserValidators;
using Microsoft.Extensions.DependencyInjection;


namespace HotelBooking.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }

}
