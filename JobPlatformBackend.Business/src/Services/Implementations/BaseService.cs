using JobPlatformBackend.Contracts.Contracts.Shared;
using JobPlatformBackend.Domain.src.Abstractions;
using JobPlatformBackend.Domain.src.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace JobPlatformBackend.Business.src.Services.Implementations
{
	public class BaseService<TEntity, TDto> where TEntity : BaseEnitity
	{
		private readonly IBaseRepository<TEntity> _repo;

 		private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _stringPropsCache = new();

		public BaseService(IBaseRepository<TEntity> repo)
		{
			_repo = repo;
		}

		public async Task<PagedResponseDto<TDto>> GetAll(QueryOptions options, Expression<Func<TEntity, TDto>> mapper)
		{
			var query = _repo.Query().AsNoTracking();

 			if (!string.IsNullOrEmpty(options.SearchKeyword))
			{
 				var stringProps = _stringPropsCache.GetOrAdd(typeof(TEntity), t =>
					t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
					 .Where(p => p.PropertyType == typeof(string) &&
								 !p.Name.Contains("Password", StringComparison.OrdinalIgnoreCase))  
					 .ToArray());

				if (stringProps.Any())
				{
					var param = Expression.Parameter(typeof(TEntity), "x");
					var searchConstant = Expression.Constant(options.SearchKeyword);
					var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

					Expression? combined = null;

					foreach (var prop in stringProps)
					{
						var propAccess = Expression.Property(param, prop);
 
						var notNull = Expression.NotEqual(propAccess, Expression.Constant(null));
						var callContains = Expression.Call(propAccess, containsMethod, searchConstant);
						var filter = Expression.AndAlso(notNull, callContains);

						combined = combined == null ? filter : Expression.OrElse(combined, filter);
					}

					if (combined != null)
						query = query.Where(Expression.Lambda<Func<TEntity, bool>>(combined, param));
				}
			}
			int totalCount = await query.CountAsync();
			if (!string.IsNullOrEmpty(options.SortBy))
			{
 				var prop = typeof(TEntity).GetProperty(options.SortBy,
					BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

				if (prop != null)
				{
					var param = Expression.Parameter(typeof(TEntity), "x");
					var propAccess = Expression.Property(param, prop);
					var lambda = Expression.Lambda(propAccess, param);

					string methodName = options.SortDescending ? "OrderByDescending" : "OrderBy";

 					var resultExp = Expression.Call(
						typeof(Queryable),
						methodName,
						new Type[] { typeof(TEntity), prop.PropertyType },
						query.Expression,
						Expression.Quote(lambda)
					);
					query = query.Provider.CreateQuery<TEntity>(resultExp);
				}
				else
				{
					query=query.OrderBy(e => e.Id);
				}
			}

			// --- 3. الترقيم (Pagination) ---
			int page = Math.Max(1, options.PageNumber);
			int size = Math.Clamp(options.PageSize, 1, 100); // استخدام Math.Clamp كطريقة أنظف

			// --- 4. التحويل والتنفيذ (Final Execution) ---
			var items= await query
				.Skip((page - 1) * size)
				.Take(size)
				.Select(mapper)
				.ToListAsync();

			return new PagedResponseDto<TDto>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = page,
				PageSize = size
			};
		}
	}
}