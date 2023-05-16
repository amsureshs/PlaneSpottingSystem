using FluentValidation;
using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Rusada.Data.DBContexts;
using Rusada.Data.Models.Entities;
using Rusada.Data.Repositories;
using Rusada.Data.Repositories.Implementations;
using Rusada.Domain.Interfaces;
using Rusada.Services;
using Rusada.Web.Models.Validators;
using Rusada.Web.Models.APIViewModels;
using Rusada.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Database context
if (builder.Configuration.GetValue<bool>("UseInSQLiteDatabase"))
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    var dbPath = System.IO.Path.Join(path, "RusadaDb_2.db");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite($"Data Source={dbPath}"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<ApplicationDbContextInitialiser>();

//Identity and authentication
builder.Services
    .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Add services to the container.
builder.Services.AddControllersWithViews();
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.Converters.Add(new DateTimeUTCJsonConverter());
    //});

//model validations with FluentValidation
builder.Services.AddScoped<IValidator<PlaneSightingCreateVM>, PlaneSightingCreateVMValidator>();
builder.Services.AddScoped<IValidator<PlaneSightingEditVM>, PlaneSightingEditVMValidator>();


//Domain servicses
builder.Services.AddTransient<IPlaneSightingsRepository, PlaneSightingsRepository>();
builder.Services.AddTransient<IPlaneSightingsService, PlaneSightingsService>();
builder.Services.AddTransient<IPlanePicturePersist, PlanePictureDiskPersist>();

//TODO Db initate and ceeding

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    // Initialise the database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

