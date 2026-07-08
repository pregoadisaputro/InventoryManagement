using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Products.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Products.Endpoints.GetProduct;

public static class GetProduct
{
    public static void MapGetProduct(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var existingProduct = await db
                        .Products.AsNoTracking()
                        .Include(p => p.Category)
                        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                    if (existingProduct is null)
                        return Results.NotFound($"Product with ID {id} not exist.");

                    return Results.Ok(
                        new GetProductResponse(
                            existingProduct.Id,
                            existingProduct.Name,
                            existingProduct.Sku,
                            existingProduct.Description,
                            existingProduct.Price,
                            existingProduct.Stock,
                            existingProduct.MinimumStock,
                            existingProduct.CategoryId,
                            existingProduct.Category.Name,
                            existingProduct.CreatedAt,
                            existingProduct.UpdatedAt
                        )
                    );
                }
            )
            .WithName(ProductEndpointNames.GetProduct);
    }
}
