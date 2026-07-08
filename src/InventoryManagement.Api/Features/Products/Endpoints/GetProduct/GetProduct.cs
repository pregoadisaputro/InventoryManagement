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
                    var response = await db
                        .Products.AsNoTracking()
                        .Where(p => p.Id == id)
                        .Select(p => new GetProductResponse(
                            p.Id,
                            p.Name,
                            p.Sku,
                            p.Description,
                            p.Price,
                            p.Stock,
                            p.MinimumStock,
                            p.CategoryId,
                            p.Category.Name,
                            p.SupplierId,
                            p.Supplier != null ? p.Supplier.Name : null,
                            p.CreatedAt,
                            p.UpdatedAt
                        ))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (response is null)
                    {
                        return Results.NotFound($"Product with ID {id} was not found.");
                    }

                    return Results.Ok(response);
                }
            )
            .WithName(ProductEndpointNames.GetProduct)
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
