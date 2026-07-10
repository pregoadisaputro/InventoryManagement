using InventoryManagement.Api.Data.Entity;

namespace InventoryManagement.Api.Features.Authentication.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    DateTimeOffset ExpiresAt { get; }
}
