using FluentValidation;
using InventoryManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Categories.Endpoints.UpdateCategory;

public static class UpdateCategory
{
    public static void MapUpdateCategory(this IEndpointRouteBuilder group)
    {
        group
            .MapPut(
                "/{id:int}",
                async (
                    int id,
                    UpdateCategoryRequest request,
                    IValidator<UpdateCategoryRequest> validator,
                    AppDbContext db,
                    CancellationToken cancellationToken
                ) =>
                {
                    var validationResult = await validator.ValidateAsync(
                        request,
                        cancellationToken
                    );

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }

                    var existingCategory = await db.Categories.FindAsync([id], cancellationToken);

                    if (existingCategory is null)
                    {
                        return Results.NotFound($"Category with ID {id} was not found.");
                    }

                    var existingCategoryName = await db
                        .Categories.AsNoTracking()
                        .AnyAsync(
                            c => c.Id != id && EF.Functions.ILike(c.Name, request.Name),
                            cancellationToken
                        );

                    if (existingCategoryName)
                    {
                        return Results.Conflict("Category with this name already exist.");
                    }

                    existingCategory.Name = request.Name;

                    await db.SaveChangesAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status204NoContent);
    }
}
