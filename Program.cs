using LimisInsight.Data;
using Microsoft.EntityFrameworkCore;
using static LimisInsight.Controllers.UsersController;

var builder = WebApplication.CreateBuilder(args);

// Register the ConnectionStrings class for DI
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddDbContext<LimisInsightContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ArtiaLocalConnection"),
    new MySqlServerVersion(new System.Version(8, 0, 21))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
