using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeleProgram.Models;
using Microsoft.AspNetCore.Identity;
using TeleProgram.Interfaces;
using TeleProgram.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Injectiong the DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IPrograms, ProgramsRepo>();
builder.Services.AddScoped<IBills, BillsRepo>();




var app = builder.Build();
// Enable serving static files from wwwroot
app.UseStaticFiles(); 

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
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//This is for seeding the Roles from the SeedingClass 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedingClass.Seed(services);  
}

app.Run();
