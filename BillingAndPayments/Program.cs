using BillingAndPayments.BusinessLogic.Interfaces;
using BillingAndPayments.BusinessLogic.Services;
using BillingAndPayments.Repository.Interfaces;
using BillingAndPayments.Repository.Models;
using BillingAndPayments.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure your database context - THIS IS CRUCIAL
builder.Services.AddDbContext<BillingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Replace "DefaultConnection" with your actual connection string

// Register your repository and service
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillService, BillService>(); // This line was missing
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddHttpContextAccessor();

// 2. Configure Cookie Policy

builder.Services.Configure<CookiePolicyOptions>(options =>

{

    options.CheckConsentNeeded = context => false; // Optional: true if you want consent banner

    options.MinimumSameSitePolicy = SameSiteMode.Lax; // Change to None if needed

    options.Secure = CookieSecurePolicy.Always; // Enforce HTTPS

});

// 3. Configure Session properly

builder.Services.AddSession(options =>

{

    options.Cookie.Name = ".DoctorMgmt.Session";

    options.IdleTimeout = TimeSpan.FromMinutes(30); // adjust as needed

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;

    options.Cookie.SameSite = SameSiteMode.Lax; // Set to None if needed with HTTPS

    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

});

builder.Services.AddControllersWithViews()

    .AddSessionStateTempDataProvider(); // Support TempData with Session

var app = builder.Build(); // Ensure this is within a method or local scope

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(

    name: "default",

    pattern: "{controller=Doctor}/{action=Login}/{id?}");

app.Run();