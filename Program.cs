using System.Reflection;
using Microsoft.Extensions.Options;
using MongoDbECommerce.Services;
using MongoDbECommerce.Services.CategoryServices;
using MongoDbECommerce.Services.CustomerServices;
using MongoDbECommerce.Services.OrderLineServices;
using MongoDbECommerce.Services.OrderServices;
using MongoDbECommerce.Services.ProductServices;
using MongoDbECommerce.Settings;

var builder = WebApplication.CreateBuilder(args);

    // LOCALSTORAGE START
    builder.Services.Configure<GCSConfigOptions>(builder.Configuration);
    builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();
    // LOCALSTORAGE FINISH

    // REGISTIRATION İŞLEMLERİ START -----------------------------------------------------------------------
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IOrderLineService, OrderLineService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    // REGISTIRATION İŞLEMLERİ FINISH -----------------------------------------------------------------------

    // AUTOMAPPER İŞLEMLERİ START -----------------------------------------------------------------------
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    // AUTOMAPPER İŞLEMLERİ FINISH -----------------------------------------------------------------------

    // MONGO DATABASE İŞLEMLERİ START -----------------------------------------------------------------------
    builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
    builder.Services.AddScoped<IDatabaseSettings>(sp =>
    {
        return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    });
    // MONGO DATABASE İŞLEMLERİ FINISH -----------------------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();

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
