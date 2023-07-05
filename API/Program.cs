
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Intrefaces;
using Infrastrucure.Data;
using API.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connection strings
var connectionstring = builder.Configuration.GetConnectionString("con1");
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlite(connectionstring);
});

//Services
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Cors Support
builder.Services.AddCors(otp => 
{
    otp.AddPolicy("CorsPolicy", policy =>
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Exception MiddleWare
app.UseMiddleware<ExceptionMiddleWare>();

//StatesCode for handling Errors
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
//MiddleWare to Serve static files wwwroot
app.UseStaticFiles();
//cors
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();
//Genrate Migration and update database 
using var scope = app.Services.CreateScope();
var servives = scope.ServiceProvider;
var context = servives.GetRequiredService<StoreContext>();
var logger = servives.GetRequiredService<ILogger<Program>>();
try 
{
    await context.Database.MigrateAsync();
    await StoreSeedContext.SeedAsync(context);
} 
catch (Exception ex)
{
    logger.LogError(ex, "Error,Migrating process");
}
app.Run();
