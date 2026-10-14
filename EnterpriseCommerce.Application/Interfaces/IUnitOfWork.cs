using EnterpriseCommerce.Domain.Common;

namespace EnterpriseCommerce.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}