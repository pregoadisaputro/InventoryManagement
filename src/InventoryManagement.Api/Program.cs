using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using InventoryManagement.Api.Data.Configuration;
using InventoryManagement.Api.Data.Extensions;
using InventoryManagement.Api.Extensions;
using InventoryManagement.Api.Features.Authentication;
using InventoryManagement.Api.Features.Authentication.Services;
using InventoryManagement.Api.Features.Categories;
using InventoryManagement.Api.Features.Dashboard;
using InventoryManagement.Api.Features.Products;
using InventoryManagement.Api.Features.Suppliers;
using InventoryManagement.Api.Features.Transactions;
using InventoryManagement.Api.Features.Users;
using InventoryManagement.Api.Shared.ErrorHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails().AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddInfrastructure(builder.Configuration);

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

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Client",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Freego Inventory Management");
    });
}

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

await app.MigrateDatabaseAsync();
app.UseExceptionHandler();

app.UseCors("Client");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet(
    "/",
    () =>
    {
        return Results.Ok(new { Name = "Inventory Management API", Version = "1.0" });
    }
);

app.MapAuthentication();
app.MapUsers();
app.MapDashboard();
app.MapCategories();
app.MapProducts();
app.MapSuppliers();
app.MapTransactions();

app.Run();
