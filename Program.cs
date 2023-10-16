using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register the ConnectionStrings class for DI
builder.Services.AddDbContext<OrigemDbContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("Origem"), new MySqlServerVersion(new Version(5, 7, 38))));

builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Local"), new MySqlServerVersion(new Version(8, 0, 21))));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
