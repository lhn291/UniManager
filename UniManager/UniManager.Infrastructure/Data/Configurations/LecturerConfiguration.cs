using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.ToTable("tbl_lecturers");

            builder.HasKey(l => l.LecturerId);

            builder.Property(l => l.FullName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(l => l.DateOfBirth)
                .IsRequired();

            builder.Property(l => l.Address)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(true);

            builder.Property(l => l.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(l => l.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(l => l.Email)
                .IsUnique();

            builder.Property(l => l.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.Role)
                .HasDefaultValue("Lecturer");

            builder.HasMany(l => l.Courses)
                .WithOne(c => c.Lecturer)
                .HasForeignKey(c => c.LecturerId);
        }
    }
}
