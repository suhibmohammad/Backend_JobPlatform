using JobPlatform.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
	{
		public void Configure(EntityTypeBuilder<PostLike> builder)
		{
			builder.ToTable("PostLikes");

			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Post)
				.WithMany(p => p.Likes)
				.HasForeignKey(x => x.PostId);

			builder.HasOne(x => x.User)
				.WithMany(u => u.PostLikes)
				.HasForeignKey(x => x.UserId);
		}
	}
}
