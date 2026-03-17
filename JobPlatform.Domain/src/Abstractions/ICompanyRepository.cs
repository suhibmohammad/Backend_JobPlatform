using JobPlatformBackend.Domain.src.Entity;

namespace JobPlatformBackend.Domain.src.Abstractions
{
	public interface ICompanyRepository : IBaseRepository<Company> { 
	Task<Company> GetWithDetailsAsync(int id);
		Task<Job> AddJobToCompanyAsync(int companyId, Job job);
		
	}
}
