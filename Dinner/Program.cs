using System.Data.Common;
using System.Data;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MyDefaultConnection");


builder.Services.AddControllers().AddNewtonsoftJson(x => 
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbCrud, DBDataOperations>();

builder.Services.AddDbContext<dinnerContext>(options =>
    options.UseSqlServer(connectionString));
//var serviceProvider = builder.Services.BuildServiceProvider();

// Добавление функциональности JWT-токенов
//public AuthOptions authOptions;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,

            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,

            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
//builder.Services.AddControllersWithViews();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
            builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
});

var app = builder.Build();

using (var datb = new dinnerContext())
{
    //var db = scope.ServiceProvider.GetRequiredService<dinnerContext>();
    if(datb.Database.GetPendingMigrations().Count() > 0)
    {
        datb.Database.Migrate();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dinnerv1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCors("AllowAllHeaders");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run(); 
