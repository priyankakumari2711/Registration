using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoginForm.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBLoginFormContextConnection") ?? throw new InvalidOperationException("Connection string 'DBLoginFormContextConnection' not found.");

builder.Services.AddDbContext<DBLoginFormContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<LoginFormUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DBLoginFormContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
