using JobPlatform.Domain.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Infrastructure.src.Configuration
{
	public class JobConfiguration : IEntityTypeConfiguration<Job>
	{
		public void Configure(EntityTypeBuilder<Job> builder)
		{
			builder.ToTable("Jobs");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(2000);

			builder.Property(x => x.Location)
				.HasMaxLength(200);

			builder.Property(x => x.ExperieceLevel)
				.HasMaxLength(100);

			builder.Property(x => x.Salary)
				.HasColumnType("decimal(18,2)");

			builder.HasOne(x => x.Company)
				.WithMany(c => c.Jobs)
				.HasForeignKey(x => x.CompanyId);
		}
	}
	
	 
}
