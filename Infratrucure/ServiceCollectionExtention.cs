using CatIstagram.Server.Data.Entites;
using CatIstagram.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using CatIstagram.Server.Features.Idintity;
using CatIstagram.Server.Features.Cats;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CatIstagram.Server.Infratrucure
{
    public static class ServiceCollectionExtention
    {
        public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var AppSettingsConfigration = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(AppSettingsConfigration);
            var AppSettings = AppSettingsConfigration.Get<AppSettings>();

            return AppSettings;

        }
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetDefaultConnectionString()));
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<user, IdentityRole>(Options =>
            {
                Options.Password.RequireDigit = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequiredLength = 4;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
        
            var Key = Encoding.ASCII.GetBytes(appSettings.secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters =  new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;

        }


        public static IServiceCollection applicationSevices(this IServiceCollection services)
          => services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ICatService, CatService>();

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CatInstagram API", Version = "v1" });
            });
    }
}
