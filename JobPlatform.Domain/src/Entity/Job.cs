namespace JobPlatformBackend.Domain.src.Entity
{
	public class Job:SharedEnitity
	{
		public required string Title { get; set; }	

		public string? Description { get; set; }

		public decimal? Salary { get; set; }

		public string? Location { get; set; }

		public TypeJob TypeJop { get; set; }

		public string? ExperieceLevel { get; set; }

		public int CompanyId { get; set; }
		
		public Company Company { get; set; }

		public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
		public ICollection<Application> Applications { get; set; } = new List<Application>();


	}

}
