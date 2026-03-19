using JobPlatformBackend.Domain.src.Abstractions;
using JobPlatformBackend.Domain.src.Common;
using JobPlatformBackend.Domain.src.Entity;
using JobPlatformBackend.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Infrastructure.src.Repository
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEnitity
	{

		private readonly AppDbContext _applicatoinDbContext;

		private readonly DbSet<TEntity> _dbset;

		private readonly ILogger <BaseRepository<TEntity>> _logger;

		public BaseRepository(AppDbContext applicationDbContext,ILogger<BaseRepository<TEntity>> logger)
		{
		_applicatoinDbContext = applicationDbContext;
		_dbset = _applicatoinDbContext.Set<TEntity>();
			_logger = logger;
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			try {
			var entry=await _dbset.AddAsync(entity);
			await _applicatoinDbContext.SaveChangesAsync();
				return entry.Entity;
			} 
			catch (DbUpdateException ex) {
				_logger.LogError(ex, "Error adding {Entity}", typeof(TEntity).Name);

				throw;
			}
		}

		public Task<TEntity> DeleteAsync(TEntity entity)
		{
			try {
				_dbset.Remove(entity);
				return Task.FromResult(entity);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error deleting {Entity}", typeof(TEntity).Name); throw;
			}
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(QueryOptions queryOptions)
		{
			
				IQueryable<TEntity> query = _dbset;

				if (!string.IsNullOrEmpty(queryOptions.SearchKeyword)) {
				var searchableProperties= typeof(TEntity).GetProperties().Where(p=>p.PropertyType==typeof(string)).ToList();

				var parameter=Expression.Parameter(typeof(TEntity),"entity");
				var orConditions=new List<Expression>();

					foreach (var property in searchableProperties) {
						var propertyAccess = Expression.Property(parameter, property);

					}
				}
			return await query.AsNoTracking().ToListAsync();



		}

		public Task<TEntity> GetByIdAsync(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity> UpdateAsync(TEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
