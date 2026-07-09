using InventoryManagement.Api.Data;
using InventoryManagement.Api.Features.Products.Constant;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Features.Products.Endpoints.GetProducts;

public static class GetProducts
{
    public static void MapGetProducts(this IEndpointRouteBuilder group)
    {
        group
            .MapGet(
                "/",
                async (
                    [AsParameters] GetProductsRequest request,
                    AppDbContext db,
                    CancellationToken cancellationToken
                ) =>
                {
                    var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
                    var pageSize = request.PageSize switch
                    {
                        < 1 => 5,
                        > 100 => 100,
                        _ => request.PageSize,
                    };
                    var skip = (pageNumber - 1) * pageSize;

                    var query = db.Products.AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        query = query.Where(p => EF.Functions.ILike(p.Name, $"%{request.Name}%"));
                    }

                    var totalProducts = await query.CountAsync(cancellationToken);
                    var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

                    var response = await query
                        .OrderBy(p => p.Id)
                        .Skip(skip)
                        .Take(pageSize)
                        .Select(p => new GetProductsResponse(
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
                        .ToListAsync(cancellationToken);

                    return Results.Ok(
                        new GetProductsPage(
                            pageNumber,
                            pageSize,
                            totalProducts,
                            totalPages,
                            response
                        )
                    );
                }
            )
            .WithName(ProductEndpointNames.GetProducts)
            .Produces<GetProductsPage>(StatusCodes.Status200OK);
    }
}
