using FluentValidation;
using InventoryManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Products.Endpoints.UpdateProduct;

public static class UpdateProduct
{
    public static void MapUpdateProduct(this IEndpointRouteBuilder group)
    {
        group
            .MapPut(
                "/{id:int}",
                async (
                    int id,
                    UpdateProductRequest request,
                    IValidator<UpdateProductRequest> validator,
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

                    var existingProduct = await db.Products.FindAsync([id], cancellationToken);

                    if (existingProduct is null)
                    {
                        return Results.NotFound($"Product with ID {id} was not found.");
                    }

                    var existingProductName = await db
                        .Products.AsNoTracking()
                        .AnyAsync(
                            p => p.Id != id && EF.Functions.ILike(p.Name, request.Name),
                            cancellationToken
                        );

                    var existingProductSku = await db
                        .Products.AsNoTracking()
                        .AnyAsync(
                            p => p.Id != id && EF.Functions.ILike(p.Sku, request.Sku),
                            cancellationToken
                        );

                    if (existingProductName)
                    {
                        return Results.Conflict($"Product with this name already exist.");
                    }

                    if (existingProductSku)
                    {
                        return Results.Conflict($"Product with this SKU already exist.");
                    }

                    var existingCategory = await db.Categories.FindAsync(
                        [request.CategoryId],
                        cancellationToken
                    );

                    if (existingCategory is null)
                    {
                        return Results.NotFound(
                            $"Category with ID {request.CategoryId} was not found."
                        );
                    }

                    if (request.SupplierId is not null)
                    {
                        var existingSupplier = await db.Suppliers.FindAsync(
                            [request.SupplierId],
                            cancellationToken
                        );

                        if (existingSupplier is null)
                        {
                            return Results.NotFound(
                                $"Supplier with ID {request.SupplierId} was not found."
                            );
                        }
                    }

                    existingProduct.Name = request.Name;
                    existingProduct.Sku = request.Sku;
                    existingProduct.Description = request.Description;
                    existingProduct.Price = request.Price;
                    existingProduct.MinimumStock = request.MinimumStock;
                    existingProduct.CategoryId = request.CategoryId;
                    existingProduct.SupplierId = request.SupplierId;
                    existingProduct.UpdatedAt = DateTimeOffset.UtcNow;

                    await db.SaveChangesAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status204NoContent);
    }
}
