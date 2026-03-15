using JobPlatform.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
	{
		public void Configure(EntityTypeBuilder<UserSkill> builder)
		{

			builder.ToTable("UserSkills");
			builder.Property(x=>x.Id).ValueGeneratedOnAdd();

			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.User)
				.WithMany(u => u.UserSkills)
				.HasForeignKey(x => x.UserID);

			builder.HasOne(x => x.Skill)
				.WithMany(s => s.UserSkills)
				.HasForeignKey(x => x.SkillId);
		}
	}
}
