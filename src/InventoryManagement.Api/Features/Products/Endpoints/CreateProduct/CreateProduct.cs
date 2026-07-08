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
            async (
                CreateProductRequest request,
                AppDbContext db,
                CancellationToken cancellationToken
            ) =>
            {
                var existingProductName = await db
                    .Products.AsNoTracking()
                    .AnyAsync(p => EF.Functions.ILike(p.Name, request.Name), cancellationToken);

                var existingProductSku = await db
                    .Products.AsNoTracking()
                    .AnyAsync(p => EF.Functions.ILike(p.Sku, request.Sku), cancellationToken);

                if (existingProductName)
                {
                    return Results.Conflict("A product with this name already exists.");
                }

                if (existingProductSku)
                {
                    return Results.Conflict("A product with this SKU already exists.");
                }

                var existingCategory = await db
                    .Categories.AsNoTracking()
                    .AnyAsync(c => c.Id == request.CategoryId, cancellationToken);

                if (!existingCategory)
                {
                    return Results.BadRequest(
                        $"Category with ID {request.CategoryId} does not exist."
                    );
                }

                if (request.SupplierId is not null)
                {
                    var existingSupplier = await db
                        .Suppliers.AsNoTracking()
                        .AnyAsync(s => s.Id == request.SupplierId, cancellationToken);

                    if (!existingSupplier)
                    {
                        return Results.BadRequest(
                            $"Supplier with ID {request.SupplierId} does not exist."
                        );
                    }
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
                    SupplierId = request.SupplierId,
                };

                db.Products.Add(newProduct);
                await db.SaveChangesAsync(cancellationToken);

                return Results.CreatedAtRoute(
                    ProductEndpointNames.GetProduct,
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
                        newProduct.SupplierId,
                        newProduct.CreatedAt,
                        newProduct.UpdatedAt
                    )
                );
            }
        );
    }
}
