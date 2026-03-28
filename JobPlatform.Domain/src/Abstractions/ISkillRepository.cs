using JobPlatformBackend.Domain.src.Entity;

namespace JobPlatformBackend.Domain.src.Abstractions
{
	public interface ISkillRepository : IBaseRepository<Skill>
	{

		Task<Skill?> GetByIdAsync(int id);
		Task<Skill?> GetByNameAsync(string name);
		Task<IEnumerable<Skill>> GetAllAsync();
		Task AddAsync(Skill skill);
		Task DeleteAsync(Skill skill);  //search by name
	}
}
