using JobPlatformBackend.Domain.src.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class PostConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder.ToTable("Posts");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.Body)
				.IsRequired()
				.HasMaxLength(2000);

			builder.HasOne(x => x.User)
				.WithMany(u => u.Posts)
				.HasForeignKey(x => x.UserId);
		}
	}
}
