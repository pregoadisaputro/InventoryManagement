using FluentValidation;
using InventoryManagement.Api.Data;
using InventoryManagement.Api.Data.Entity;
using InventoryManagement.Api.Features.Categories.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Categories.Endpoints.CreateCategory;

public static class CreateCategory
{
    public static void MapCreateCategory(this IEndpointRouteBuilder group)
    {
        group
            .MapPost(
                "/",
                async (
                    CreateCategoryRequest request,
                    IValidator<CreateCategoryRequest> validator,
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

                    var existingCategory = await db
                        .Categories.AsNoTracking()
                        .AnyAsync(c => EF.Functions.ILike(c.Name, request.Name), cancellationToken);

                    if (existingCategory)
                    {
                        return Results.Conflict("A category with this name already exist.");
                    }

                    var newCategory = new Category { Name = request.Name };

                    db.Categories.Add(newCategory);
                    await db.SaveChangesAsync(cancellationToken);

                    return Results.CreatedAtRoute(
                        CategoryEndpointNames.GetCategory,
                        new { id = newCategory.Id },
                        new CreateCategoryResponse(newCategory.Id, newCategory.Name)
                    );
                }
            )
            .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict);
    }
}
