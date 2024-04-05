using AspDotNetCoreRequestProcessingPipeline.Extension;
using AspDotNetCoreRequestProcessingPipeline.Middleware;

namespace AspDotNetCoreRequestProcessingPipeline
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<CustomMiddleware>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Using custom middleware 
            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("Before first middleware executing" + Environment.NewLine);
                await next(context);
                //await context.Response.WriteAsync("After first middleware executing" + Environment.NewLine);
            });

            app.UseCustomMiddleware();

            app.MapControllerRoute(
                name: "default",
                pattern: "api/{controller}/{action}/{id?}");

            app.Run();
        }
    }
}