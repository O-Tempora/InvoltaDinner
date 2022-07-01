using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = builder.Configuration.GetConnectionString("MyDefaultConnection");


builder.Services.AddControllers().AddNewtonsoftJson(x => 
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// builder.Services.AddScoped<IRepository<DinnerMenu>, DinnerMenuRepos>();
// builder.Services.AddScoped<IRepository<DishMenu>, DishMenuRepos>();
// builder.Services.AddScoped<IRepository<Dish>, DishRepos>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbCrud, DBDataOperations>();

builder.Services.AddDbContext<DAL.Entities.DinnerContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// builder.Services.AddIdentity<DAL.Entities.User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddRoles<IdentityRole>()
//     .AddEntityFrameworkStores<DAL.Entities.DinnerContext>();

// builder.Services.Configure<IdentityOptions>(options => {
//     options.Password.RequiredLength = 5;
//     options.Lockout.MaxFailedAccessAttempts = 5;
//     options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-+@_= <>#$%";
//     options.User.RequireUniqueEmail = false;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = false;
// });

var serviceProvider = builder.Services.BuildServiceProvider();
/*
async Task CreateUserRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

    /// <summary>
    /// Создание ролей пользователя/админа/повара
    /// </summary>>
    if (await roleManager.FindByNameAsync("Admin") == null)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    if (await roleManager.FindByNameAsync("User") == null)
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }
    if (await roleManager.FindByNameAsync("Cook") == null)
    {
        await roleManager.CreateAsync(new IdentityRole("Cook"));
    }

    #region Администратор
    
    string adminEmail = "admin@gmail.com";
    string adminPassword = "AdminAdmin1@";
    string adminName = "Admin1";

    if (await userManager.FindByNameAsync(adminEmail) == null)
    {
        User admin = new User
        {
            Email = adminEmail,
            UserName = adminName,
            Password = adminPassword
        };
        IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    #endregion

    #region Пользователь
    
    string userEmail = "user@gmail.com";
    string userPassword = "UserUser1@";
    string userName = "User1";

    if (await userManager.FindByNameAsync(userEmail) == null)
    {
        User user = new User
        {
            Email = userEmail,
            UserName = userName,
            Password = userPassword
        };
        IdentityResult result = await userManager.CreateAsync(user, userPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }
    }

    #endregion

    #region Повар
    
    string cookEmail = "cook@gmail.com";
    string cookPassword = "CookCook1@";
    string cookName = "Cook1";

    if (await userManager.FindByNameAsync(cookEmail) == null)
    {
        User cook = new User
        {
            Email = cookEmail,
            UserName = cookName,
            Password = cookPassword
        };
        IdentityResult result = await userManager.CreateAsync(cook, cookPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(cook, "Cook");
        }
    }

    #endregion
}
CreateUserRoles(serviceProvider).Wait(); 
*/

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run(); 
