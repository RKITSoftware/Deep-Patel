using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Data;

// Create a new WebApplication instance.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure the application to use an in-memory database for the ApiContext.
builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("BookingDb"));

// Add controllers to the service container.
builder.Services.AddControllers();

// Add services required for API versioning and Swagger/OpenAPI documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger and Swagger UI only in the development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enable authentication and authorization.
app.UseAuthorization();

// Map controllers to the request pipeline.
app.MapControllers();

// Run the application.
app.Run();
