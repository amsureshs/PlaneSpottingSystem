using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Rusada.Data.DBContexts;
using Rusada.Data.Models.Entities;
using Rusada.Data.Repositories;
using Rusada.Data.Repositories.Implementations;
using Rusada.Domain.Interfaces;
using Rusada.Services;

var builder = WebApplication.CreateBuilder(args);

//Database context
if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("RusadaDb"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Identity and authentication
builder.Services
    .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentityServer().AddApiAuthorization<User, ApplicationDbContext>();
builder.Services.AddAuthentication().AddIdentityServerJwt();


//Add services to the container.
builder.Services.AddControllersWithViews();

//model validations with FluentValidation
builder.Services.AddFluentValidationAutoValidation();

//Domain servicses
builder.Services.AddTransient<IPlaneSightingsRepository, PlaneSightingsRepository>();
builder.Services.AddTransient<IPlaneSightingsService, PlaneSightingsService>();
builder.Services.AddTransient<IPlanePicturePersist, PlanePictureDiskPersist>();

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

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

