using BillingAndPayments.BusinessLogic.Interfaces;
using BillingAndPayments.BusinessLogic.Services;
using BillingAndPayments.Models; // You might not need this here anymore as BillingContext is in the Repository
using BillingAndPayments.Repository;
using BillingAndPayments.Repository.Interfaces;
using BillingAndPayments.Repository.Models;
using BillingAndPayments.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Test committ from Gokul
// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure your database context - THIS IS CRUCIAL
builder.Services.AddDbContext<BillingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))); // Replace "DefaultConnection" with your actual connection string

// Register your repository and service
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillService, BillService>(); // This line was missing

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();