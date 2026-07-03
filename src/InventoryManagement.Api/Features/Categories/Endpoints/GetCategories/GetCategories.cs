using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Categories.Constant;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Categories.Endpoints.GetCategories;

public static class GetCategories
{
    public static void MapGetCategories(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/",
                async Task<Ok<List<GetCategoriesResponse>>> (
                    AppDbContext db,
                    CancellationToken ct
                ) =>
                {
                    var categories = await db
                        .Categories.AsNoTracking()
                        .Select(c => new GetCategoriesResponse(c.Id, c.Name))
                        .ToListAsync(ct);

                    return TypedResults.Ok(categories);
                }
            )
            .WithName(CategoryEndpointNames.GetCategory)
            .Produces<List<GetCategoriesResponse>>(200);
    }
}
