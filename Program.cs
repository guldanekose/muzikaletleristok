using Microsoft.AspNetCore.Authentication.Cookies;//1.adým
using Microsoft.EntityFrameworkCore;
using muzikaletleristok.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MuzikaaletleristokContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));
builder.Services.AddDbContext<MuzikaaletleristokContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Datacon"))
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
AddCookie(option =>
{
    option.LoginPath = "/Startp/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});//2.adým
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
app.UseAuthentication();//3.adým
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Startp}/{action=Login}/{id?}");//4.adým

app.Run();
