using JobPlatformBackend.Domain.src.Entity;

namespace JobPlatformBackend.Domain.src.Abstractions
{
	public interface ISkillRepository : IBaseRepository<Skill>
	{
		
		Task<Skill> GetWithDetailsAsync(int skillId); 

		Task<Skill> GetByNameAsync(string name);  //search by name
	}
}
