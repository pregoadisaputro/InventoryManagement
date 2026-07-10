using System.Text.Json.Serialization;
using FluentValidation;
using InventoryManagement.Api.Data.Configuration;
using InventoryManagement.Api.Features.Categories;
using InventoryManagement.Api.Features.Products;
using InventoryManagement.Api.Features.Suppliers;
using InventoryManagement.Api.Features.Transactions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.MapCategories();
app.MapProducts();
app.MapSuppliers();
app.MapTransactions();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Freego Inventory Management").WithClassicLayout().ForceDarkMode();
    });
}

app.UseHttpsRedirection();

app.Run();
