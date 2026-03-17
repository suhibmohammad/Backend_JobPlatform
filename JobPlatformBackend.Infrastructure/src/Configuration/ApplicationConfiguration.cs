using JobPlatformBackend.Domain.src.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
	{
		public void Configure(EntityTypeBuilder<Application> builder)
		{
			builder.ToTable("Applications");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.CvUrl)
				.IsRequired()
				.HasMaxLength(500);

			builder.HasOne(a => a.Job)
			.WithMany(j => j.Applications)
			.HasForeignKey(a => a.JobId)
			.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Applications)
				.HasForeignKey(x => x.UserId);
		}
	}
}
