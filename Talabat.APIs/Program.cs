using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middleware;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.IdentityData;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>( (connection) =>
            {
                var connect = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connect);
            });
            builder.Services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString"));
            });
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddService();
            #endregion

            #region Configuration Services
            var app = builder.Build();
            using var Scope = app.Services.CreateScope();
            var Servecies = Scope.ServiceProvider;
            var _dbContext = Servecies.GetRequiredService<StoreDbContext>();
            var _IdentityContext = Servecies.GetRequiredService<UserDbContext>();
            var Logger = Servecies.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await _IdentityContext.Database.MigrateAsync();
                await StoreContectSeeding.SeedAsnc(_dbContext);
                var userManager = Servecies.GetRequiredService<UserManager<AppUser>>();
                await AppUserSeeding.SeedUser(userManager);
            }
            catch (Exception ex)
            {
                var logger = Logger.CreateLogger<Program>();
                logger.LogError(ex, "an error occured during apply Migration");

            }; 
            #endregion

            #region Middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}