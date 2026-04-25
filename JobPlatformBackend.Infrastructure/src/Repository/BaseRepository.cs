using JobPlatformBackend.Domain.src.Abstractions;
 using JobPlatformBackend.Domain.src.Entity;
using JobPlatformBackend.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace JobPlatformBackend.Infrastructure.src.Repository
{


	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEnitity
	{
		private readonly AppDbContext _context;
		private readonly DbSet<TEntity> _dbSet;
		private readonly ILogger<BaseRepository<TEntity>> _logger;

		public BaseRepository(AppDbContext context, ILogger<BaseRepository<TEntity>> logger)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
			_logger = logger;
		}

		// 🔹 Generic Queryable (for filtering, sorting, projection in Application Layer)
		public IQueryable<TEntity> Query(bool includeDeleted = false)
		{
			if (typeof(TEntity).GetProperty("IsDeleted") != null && !includeDeleted)
			{
				return _dbSet.AsNoTracking().Where(e => !EF.Property<bool>(e, "IsDeleted"));
			}
			return _dbSet.AsNoTracking();
		}
		public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			if (predicate != null)
			{
				return await _dbSet.CountAsync(predicate, cancellationToken);
			}
			return await _dbSet.CountAsync(cancellationToken);
		}

		// 🔹 Get by Id
		public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _dbSet.FirstOrDefaultAsync(x=>x.Id==id,cancellationToken);
		}

		// 🔹 Add
		public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			try
			{
				var entry = await _dbSet.AddAsync(entity, cancellationToken);
				return entry.Entity;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Error adding entity {Entity}", typeof(TEntity).Name);
				throw;
			}
		}

		// 🔹 Update
		public   Task UpdateAsync(TEntity entity)
		{
			try { 
			 _dbSet.Update(entity);
			return Task.CompletedTask;
			}catch (Exception ex)
			{
				throw new Exception("Error in update data");
			}
		}

		// 🔹 Delete (Hard Delete by default, Soft Delete if property exists)
		public Task DeleteAsync(TEntity entity)
		{
			var isSoftDelete = typeof(TEntity).GetProperty("IsDeleted") != null;
			if (isSoftDelete)
			{
				_dbSet.Attach(entity);
				typeof(TEntity).GetProperty("IsDeleted")!.SetValue(entity, true);
				_dbSet.Update(entity);
			}
			else
			{
				_dbSet.Remove(entity);
			}
			return Task.CompletedTask;
		}

	 public Task GetCountAsync (Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
		{
			if (predicate != null)
			{
				return _dbSet.CountAsync(predicate, cancellationToken);
			}
			return _dbSet.CountAsync(cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync();
		}
	}
}