using AutoMapper;
using CatamaransRental.DAL;
using CatamaransRental.DAL.Interfaces;
using CatamaransRental.DAL.Repositories;
using CatamaransRental.Domain.Models;
using CatamaransRental.Services.AutoMapper;
using CatamaransRental.Services.Implementions;
using CatamaransRental.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBaseRepository<Catamaran>, CatamaranRepository>();
builder.Services.AddScoped<ICatamaranService, CatamaranService>();
builder.Services.AddScoped<IBaseRepository<Rental>, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IBaseRepository<Ticket>, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IBaseRepository<Purchase>, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IBaseRepository<PurchaseAttempt>, PurchaseAttemptRepository>();

builder.Services.AddAutoMapper(typeof(Catamaran), typeof(CatamaranProfile));
builder.Services.AddAutoMapper(typeof(Rental), typeof(RentalProfile));
builder.Services.AddAutoMapper(typeof(Ticket), typeof(TicketProfile));
builder.Services.AddAutoMapper(typeof(Purchase), typeof(PurchaseProfile));
builder.Services.AddAutoMapper(typeof(User), typeof(UserProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "YourAppName.CookieAuthentication";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
         options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
    });


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
