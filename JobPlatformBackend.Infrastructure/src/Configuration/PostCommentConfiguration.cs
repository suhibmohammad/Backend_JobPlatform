using JobPlatform.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
	{
		public void Configure(EntityTypeBuilder<PostComment> builder)
		{
			builder.ToTable("PostComments");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Content)
				.IsRequired()
				.HasMaxLength(1000);

			builder.HasOne(x => x.Post)
				.WithMany(p => p.Comments)
				.HasForeignKey(x => x.PostId);

			builder.HasOne(x => x.User)
				.WithMany(u => u.PostComments)
				.HasForeignKey(x => x.UserId);
		}
	}
}
