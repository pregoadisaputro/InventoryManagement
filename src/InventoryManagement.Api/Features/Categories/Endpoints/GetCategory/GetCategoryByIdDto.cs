namespace InventoryManagement.Api.Features.Categories.Endpoints.GetCategory;

public record GetCategoryByIdRequest(int Id);

public record GetCategoryByIdResponse(int Id, string Name);
