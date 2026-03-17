using JobPlatformBackend.Domain.src.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class JobSkillConfiguration : IEntityTypeConfiguration<JobSkill>
	{
		public void Configure(EntityTypeBuilder<JobSkill> builder)
		{
			builder.ToTable("JobSkills");

			builder.HasKey(x => x.Id);
			builder.HasOne(x => x.Job)
				.WithMany(j => j.JobSkills)
				.HasForeignKey(x => x.JobId);

			builder.HasOne(js => js.Skill)
		.WithMany(s => s.JobSkills)
		.HasForeignKey(js => js.SkillId);
			builder.HasIndex(x => new { x.JobId, x.SkillId }).IsUnique();

		}
	}
}
