namespace JobPlatform.Domain.Entity
{
	public class Skill:BaseEnitity
	{
		public string Name { get; set; }

		public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();

		public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();

	}

}
