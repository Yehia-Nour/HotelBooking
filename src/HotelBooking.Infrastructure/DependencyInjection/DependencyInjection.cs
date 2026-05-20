namespace HotelBooking.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions<ConnectionStringsOptions>()
                .BindConfiguration(ConnectionStringsOptions.SectionName)
                .Validate(options => !string.IsNullOrWhiteSpace(options.DefaultConnection),
                    "Default database connection string is required.")
                .ValidateOnStart();

            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .Validate(options => !string.IsNullOrWhiteSpace(options.Issuer),
                    "JWT issuer is required.")
                .Validate(options => !string.IsNullOrWhiteSpace(options.Audience),
                    "JWT audience is required.")
                .Validate(options => !string.IsNullOrWhiteSpace(options.SecretKey),
                    "JWT secret key is required.")
                .Validate(options => options.ExpiryInMinutes > 0,
                    "JWT expiry must be greater than zero.")
                .ValidateOnStart();

            var connectionStrings = configuration
                .GetRequiredSection(ConnectionStringsOptions.SectionName)
                .Get<ConnectionStringsOptions>()!;

            services.AddDbContext<HotelBookingDbContext>(options =>
                options.UseSqlServer(connectionStrings.DefaultConnection));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HotelBookingDbContext>();

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IJsonFileReader, JsonFileReader>();
            services.AddKeyedScoped<IDataInitializer, DataInitializer>("Default");
            services.AddKeyedScoped<IDataInitializer, IdentityDataInitializer>("Identity");

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
