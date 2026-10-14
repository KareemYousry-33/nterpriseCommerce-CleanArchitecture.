using EnterpriseCommerce.Domain.Entities;

namespace EnterpriseCommerce.Application.Interfaces;

public interface ICartRepository
{
    Task<CustomerCart?> GetCartAsync(string userId, CancellationToken cancellationToken = default);
    Task<CustomerCart?> UpdateCartAsync(CustomerCart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteCartAsync(string userId, CancellationToken cancellationToken = default);
}