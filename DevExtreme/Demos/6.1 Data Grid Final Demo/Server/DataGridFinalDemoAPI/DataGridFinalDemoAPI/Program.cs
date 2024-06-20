using DataGridFinalDemoAPI.Business_Logic.Handlers;
using DataGridFinalDemoAPI.Business_Logic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICTY01Service, BLCTY01Handler>();
builder.Services.AddSingleton<ISTU01Service, BLSTU01Handler>();
builder.Services.AddSingleton<IEDC01Service, BLEDC01Handler>();

string developerCorsPolicy = "DevCors";
builder.Services.AddCors(action =>
{
    action.AddPolicy(developerCorsPolicy, policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .SetPreflightMaxAge(TimeSpan.FromDays(7));
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors(developerCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();