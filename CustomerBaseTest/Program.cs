using CustomerBaseTest.Data;
using CustomerBaseTest.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowCredentials()
        .WithOrigins("https://localhost:7143")
        .AllowAnyHeader()
        .WithMethods("GET","POST")
        .AllowAnyMethod()
       
        .SetIsOriginAllowed(x => true)));




builder.Services.AddDbContext<CustomerIdentiyContext>();
builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 5;
        //options.Password.RequiredUniqueChars = 1;



        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        //options.User.AllowedUserNameCharacters =
        //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;

    })
    .AddEntityFrameworkStores<CustomerIdentiyContext>()
    .AddDefaultTokenProviders().Services.AddMvc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");


app.Run();
