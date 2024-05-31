using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("tbl_admins");

            builder.HasKey(a => a.AdminId);

            builder.Property(a => a.AdminId)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Role)
                .HasDefaultValue("Admin");
        }
    }
}
