using JobPlatformBackend.Domain.src.Common;
using JobPlatformBackend.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Domain.src.Abstractions
{
	public interface IBaseRepository<TEntity>  where TEntity : BaseEnitity
	{
		Task<IEnumerable<TEntity>> GetAllAsync(QueryOptions queryOptions);
		Task<TEntity> AddAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TEntity entity);

		Task<TEntity> DeleteAsync(TEntity entity);

		Task<TEntity> GetByIdAsync(TEntity entity);

	}
}
