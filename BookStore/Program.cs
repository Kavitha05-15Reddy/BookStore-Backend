using BussinessLayer.Interface;
using BussinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System.Text;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<BookContext>();

            builder.Services.AddTransient<IUserRL, UserRL>();
            builder.Services.AddTransient<IUserBL, UserBL>();

            builder.Services.AddTransient<IBookRL, BookRL>();
            builder.Services.AddTransient<IBookBL, BookBL>();

            builder.Services.AddTransient<IFeedbackRL, FeedbackRL>();
            builder.Services.AddTransient<IFeedbackBL, FeedbackBL>();

            builder.Services.AddTransient<ICartRL, CartRL>();
            builder.Services.AddTransient<ICartBL, CartBL>();

            builder.Services.AddTransient<IAddressRL, AddressRL>();
            builder.Services.AddTransient<IAddressBL, AddressBL>();

            builder.Services.AddTransient<IOrderRL, OrderRL>();
            builder.Services.AddTransient<IOrderBL, OrderBL>();

            builder.Services.AddTransient<IWishlistRL, WishlistRL>();
            builder.Services.AddTransient<IWishlistBL, WishlistBL>();

            builder.Services.AddTransient<IAdminRL, AdminRL>();
            builder.Services.AddTransient<IAdminBL, AdminBL>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
