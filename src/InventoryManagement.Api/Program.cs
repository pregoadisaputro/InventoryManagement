using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using InventoryManagement.Api.Data.Configuration;
using InventoryManagement.Api.Features.Authentication;
using InventoryManagement.Api.Features.Authentication.Services;
using InventoryManagement.Api.Features.Categories;
using InventoryManagement.Api.Features.Products;
using InventoryManagement.Api.Features.Suppliers;
using InventoryManagement.Api.Features.Transactions;
using InventoryManagement.Api.Features.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!)),
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Freego Inventory Management").WithClassicLayout().ForceDarkMode();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthentication();
app.MapUsers();
app.MapCategories();
app.MapProducts();
app.MapSuppliers();
app.MapTransactions();

app.Run();
