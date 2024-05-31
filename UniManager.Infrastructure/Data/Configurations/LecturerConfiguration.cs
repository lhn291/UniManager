using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.ToTable("tbl_lecturers");
 
            builder.HasKey(l => l.LecturerId);

            builder.Property(l => l.LecturerId).IsRequired().HasMaxLength(50);
            builder.Property(l => l.FullName).IsRequired().HasMaxLength(100);
            builder.Property(l => l.DateOfBirth).IsRequired();
            builder.Property(l => l.Address).IsRequired().HasMaxLength(150);
            builder.Property(l => l.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(l => l.Email).IsRequired().HasMaxLength(50);
            builder.Property(l => l.Password).IsRequired().HasMaxLength(50);
            builder.Property(l => l.UserName).IsRequired().HasMaxLength(50);
            builder.Property(l => l.Role).IsRequired().HasMaxLength(50);

            builder.HasMany(l => l.Subjects)
                   .WithOne(s => s.Lecturer)
                   .HasForeignKey(s => s.LecturerId);
        }
    }
}