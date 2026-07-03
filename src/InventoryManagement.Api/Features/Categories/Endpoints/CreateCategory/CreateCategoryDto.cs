namespace InventoryManagement.Api.Features.Categories.Endpoints.CreateCategory;

public record CreateCategoryRequest(string Name);

public record CreateCategoryResponse(int Id, string Name);
