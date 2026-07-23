using FluentValidation;
using InventoryManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Suppliers.Endpoints.UpdateSupplier;

public static class UpdateSupplier
{
    public static void MapUpdateSupplier(this IEndpointRouteBuilder group)
    {
        group
            .MapPut(
                "/{id:int}",
                async (
                    int id,
                    UpdateSupplierRequest request,
                    IValidator<UpdateSupplierRequest> validator,
                    AppDbContext db,
                    ILogger<Program> logger,
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

                    var existingSupplier = await db.Suppliers.FindAsync([id], cancellationToken);

                    if (existingSupplier is null)
                    {
                        return Results.NotFound($"Supplier with ID {id} was not found.");
                    }

                    var existingSupplierName = await db
                        .Suppliers.AsNoTracking()
                        .AnyAsync(
                            s => s.Id != id && EF.Functions.ILike(s.Name, request.Name),
                            cancellationToken
                        );

                    if (existingSupplierName)
                    {
                        return Results.Conflict("Supplier with this name already exist.");
                    }

                    existingSupplier.Name = request.Name;
                    existingSupplier.PhoneNumber = request.PhoneNumber;
                    existingSupplier.Email = request.Email;
                    existingSupplier.Address = request.Address;

                    await db.SaveChangesAsync(cancellationToken);

                    logger.LogInformation(
                        "Supplier updated for ID {SupplierId}",
                        existingSupplier.Id
                    );

                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status204NoContent);
    }
}
