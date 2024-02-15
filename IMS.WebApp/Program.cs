using IMS.Plugins.InMemory;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products;
using IMS.UseCases.Products.interfaces;
using IMS.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();// instance created once and stored entire lifetime
builder.Services.AddSingleton<IProductRepository, ProductRepository>();


builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();//framework doesn't store instance anywhere. It creates whenever needed.

//builder.Services.AddScoped<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();// this instance will be stored but It will only be stored for the lifeti//me of the signalR channel. Disposed on refresh. 
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();// It's a usecase so we add a transient

builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>();
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
