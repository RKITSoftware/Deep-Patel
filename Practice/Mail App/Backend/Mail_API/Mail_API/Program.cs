using Mail_API.Data;
using Mail_API.Extensions;
using Mail_API.Interface;
using Mail_API.Repository;
using Mail_API.Service;
using Microsoft.EntityFrameworkCore;

namespace Mail_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Getting connection string
            string defaultConnectionString = builder.Configuration.GetConnectionString("default");

            // Add services to the container.
            builder.Services.AddTransient<IEncryptionService, EncryptionService>();
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                config.JwtConfiguration();
            });

            builder.Services.AddJwtAuthentication(builder.Configuration);

            // App build
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}