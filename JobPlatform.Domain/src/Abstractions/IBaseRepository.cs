 using JobPlatformBackend.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Domain.src.Abstractions
{
	public interface IBaseRepository<TEntity> where TEntity : BaseEnitity
	{
		IQueryable<TEntity> Query(bool includeDeleted = false);

		Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

		Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task UpdateAsync(TEntity entity);

		Task DeleteAsync(TEntity entity);

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
		Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
		Task GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
	}
}
