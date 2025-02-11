using Blog.Data;
using Blog.Interfaces;
using Blog.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Blog.Utility;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnction")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
// Register a preconfigured instance of the MailSettings class
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IBlogEmailSender, EmailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// Register our Image Service
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IFileManager, FileManager>();
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
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=UI}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
