using JobPlatformBackend.Domain.src.Entity;

namespace JobPlatformBackend.Domain.src.Abstractions
{
	public interface IJobRepository : IBaseRepository<Job>
	{
		//get all jobs for a specific company
		Task<IEnumerable<Job>> GetByCompanyIdAsync(int companyId);
		//get job with its related company, applicatoins , and jobskills
		Task<IEnumerable<Job>> GetWithDetailsAsync(int jobId);
		//serch jobs by title or location
		Task<IEnumerable<Job>> SearchAsync(string? title = null, string? location = null);
	}
}
