using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProductApplication.APIController;
using ProductApplication.APIController.APIServices;
using ProductApplication.Data;
using ProductApplication.Service.IService;
using ProductApplication.Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;

builder.Services.Configure<ApiSetting>(configuration);
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ApiSetting>>().Value);

builder.Services.AddHttpClient<CategoryApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7247/"); // Replace with your actual API base URL
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProductApp")));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAppSettingsService, AppSettingsService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
