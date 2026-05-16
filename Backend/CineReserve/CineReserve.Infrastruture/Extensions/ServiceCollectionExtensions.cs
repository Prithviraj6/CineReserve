using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;
using CineReserve.Application.Mappings;
using CineReserve.Application.Settings;
using CineReserve.Infrastruture.Data;
using CineReserve.Infrastruture.Repositories;
using CineReserve.Infrastruture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CineReserve.Infrastruture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ITicketDetailRepository, TicketDetailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITheaterHallRepository, TheaterHallRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IShowtimeService, ShowtimeService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // AutoMapper
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            // JWT Authentication
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            return services;
        }
    }
}
