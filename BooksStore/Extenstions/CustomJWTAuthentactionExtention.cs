using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BooksStore.Extenstions
{
    public static class CustomJWTAuthentactionExtention
    {
        public static void AddCustomJwtAuth(this IServiceCollection services , ConfigurationManager configuration)
        {
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op => { 
                op.RequireHttpsMetadata = false;
                op.SaveToken = true;
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SekretKey"]))
                };
            });
        }

        public static void AddSwaggerConfigsJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
             

                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In= ParameterLocation.Header,
                    Description = "Enter The Jwt Key"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",

                               
                            }, In = ParameterLocation.Header,
                           Name = "Bearer"

                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
