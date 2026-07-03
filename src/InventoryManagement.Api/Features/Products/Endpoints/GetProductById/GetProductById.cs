using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Products.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Products.Endpoints.GetProductById;

public static class GetProductById
{
    public static void MapGetProductById(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    var existingProduct = await db
                        .Products.AsNoTracking()
                        .Include(p => p.Category)
                        .OrderBy(p => p.Id)
                        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                    if (existingProduct is null)
                    {
                        return Results.NotFound($"Product with ID {id} not exist.");
                    }

                    return Results.Ok(
                        new GetProductByIdResponse(
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
            .WithName(ProductEndpointNames.GetProductById);
    }
}
