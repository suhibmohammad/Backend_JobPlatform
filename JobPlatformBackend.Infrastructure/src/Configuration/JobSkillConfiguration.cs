using JobPlatform.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class JobSkillConfiguration : IEntityTypeConfiguration<JobSkill>
	{
		public void Configure(EntityTypeBuilder<JobSkill> builder)
		{
			builder.ToTable("JobSkills");

			builder.HasKey(x => new { x.JobId, x.SkillId });

			builder.HasOne(x => x.Job)
				.WithMany(j => j.JobSkills)
				.HasForeignKey(x => x.JobId);

			builder.HasOne(x => x.Skill)
				.WithMany()
				.HasForeignKey(x => x.SkillId);
		}
	}
}
