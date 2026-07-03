using InventoryManagement.Api.Data;
using InventoryManagement.Api.Data.Entity;
using InventoryManagement.Api.Features.Products.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Products.Endpoints.CreateProduct;

public static class CreateProduct
{
    public static void MapCreateProduct(this IEndpointRouteBuilder group)
    {
        group.MapPost(
            "/",
            async (CreateProductRequest request, AppDbContext db) =>
            {
                var existingProductName = await db
                    .Products.AsNoTracking()
                    .AnyAsync(p => EF.Functions.ILike(p.Name, request.Name));

                var existingProductSku = await db
                    .Products.AsNoTracking()
                    .AnyAsync(p => EF.Functions.ILike(p.Sku, request.Sku));

                if (existingProductName)
                {
                    return Results.BadRequest("A product with this name already exists.");
                }

                if (existingProductSku)
                {
                    return Results.BadRequest("A product with this SKU already exists.");
                }

                var existingCategory = await db
                    .Categories.AsNoTracking()
                    .AnyAsync(c => c.Id == request.CategoryId);

                if (!existingCategory)
                {
                    return Results.BadRequest(
                        $"Category with ID {request.CategoryId} does not exist."
                    );
                }

                var newProduct = new Product
                {
                    Name = request.Name,
                    Sku = request.Sku,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock,
                    MinimumStock = request.MinimumStock,
                    CategoryId = request.CategoryId,
                };

                db.Products.Add(newProduct);
                await db.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    ProductEndpointNames.GetProductById,
                    new { id = newProduct.Id },
                    new CreateProductResponse(
                        newProduct.Id,
                        newProduct.Name,
                        newProduct.Sku,
                        newProduct.Description,
                        newProduct.Price,
                        newProduct.Stock,
                        newProduct.MinimumStock,
                        newProduct.CategoryId,
                        newProduct.CreatedAt,
                        newProduct.UpdatedAt
                    )
                );
            }
        );
    }
}
