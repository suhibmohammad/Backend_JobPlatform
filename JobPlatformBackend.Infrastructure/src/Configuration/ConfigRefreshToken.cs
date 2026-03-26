using JobPlatformBackend.Domain.src.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class ConfigRefreshToken : IEntityTypeConfiguration<UserRefreshToken>
	{
		public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				;
			builder.Property(x => x.UserId).IsRequired();
			builder.HasOne(x => x.User).WithMany(x => x.RefreshTokens)
				.HasForeignKey(fk => fk.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
