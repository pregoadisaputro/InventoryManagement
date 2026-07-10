using InventoryManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Products.Endpoints.DeleteProduct;

public static class DeleteProduct
{
    public static void MapDeleteProduct(this IEndpointRouteBuilder group)
    {
        group
            .MapDelete(
                "/{id:int}",
                async (int id, AppDbContext db, CancellationToken cancellationToken) =>
                {
                    await db.Products.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status204NoContent);
    }
}
