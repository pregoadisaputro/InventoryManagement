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

                    var query = db.Products.AsNoTracking().AsQueryable();

                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        query = query.Where(p => EF.Functions.ILike(p.Name, $"%{request.Name}%"));
                    }

                    if (request.CategoryId.HasValue)
                    {
                        query = query.Where(p => p.CategoryId == request.CategoryId);
                    }

                    if (request.SupplierId.HasValue)
                    {
                        query = query.Where(p => p.SupplierId == request.SupplierId);
                    }

                    if (request.LowStock == true)
                    {
                        query = query.Where(p => p.Stock > 0 && p.Stock <= p.MinimumStock);
                    }

                    if (request.OutOfStock == true)
                    {
                        query = query.Where(p => p.Stock == 0);
                    }

                    if (request.MinPrice.HasValue)
                    {
                        query = query.Where(p => p.Price >= request.MinPrice);
                    }

                    if (request.MaxPrice.HasValue)
                    {
                        query = query.Where(p => p.Price <= request.MaxPrice);
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
                            p.ImgUrl,
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
