using Blog.Common.Domain.Policies;
using Blog.Common.Domain.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog.Common.Application.Auth
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonAuth(this IServiceCollection services, IConfiguration config)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = config["JWT:ValidAudience"],
                    ValidIssuer = config["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"] ?? ""))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApplicationPolicyDefaults.RequireAdminRole,
                    policy => policy.RequireRole(ApplicationRoleDefaults.Admin));

                options.AddPolicy(ApplicationPolicyDefaults.RequireUserOrAdminRole,
                    policy => policy.RequireRole(ApplicationRoleDefaults.Admin, ApplicationRoleDefaults.User));
            });

            return services;
        }
    }
}
