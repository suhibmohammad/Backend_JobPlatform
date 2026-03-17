using JobPlatformBackend.Domain.src.Entity;
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

			builder.HasOne(pl => pl.User)
	.WithMany(u => u.PostLikes)
	.HasForeignKey(pl => pl.UserId)
	.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
