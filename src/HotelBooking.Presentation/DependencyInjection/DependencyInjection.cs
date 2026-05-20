namespace HotelBooking.Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
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

            services.AddOptions<GoogleAuthOptions>()
                .BindConfiguration(GoogleAuthOptions.SectionName)
                .Validate(options => !string.IsNullOrWhiteSpace(options.ClientId),
                    "Google authentication client id is required.")
                .Validate(options => !string.IsNullOrWhiteSpace(options.ClientSecret),
                    "Google authentication client secret is required.")
                .ValidateOnStart();

            var jwtOptions = configuration
                .GetRequiredSection(JwtOptions.SectionName)
                .Get<JwtOptions>()!;

            var googleAuthOptions = configuration
                .GetRequiredSection(GoogleAuthOptions.SectionName)
                .Get<GoogleAuthOptions>()!;

            services.AddControllers().AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOi...\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            services.AddFluentValidationAutoValidation();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = googleAuthOptions.ClientId;
                options.ClientSecret = googleAuthOptions.ClientSecret;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });

            return services;
        }
    }
}
