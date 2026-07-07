using InventoryManagement.Api.Data.Configuration;
using InventoryManagement.Api.Features.Categories;
using InventoryManagement.Api.Features.Products;
using InventoryManagement.Api.Features.Suppliers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapCategories();
app.MapProducts();
app.MapSuppliers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
