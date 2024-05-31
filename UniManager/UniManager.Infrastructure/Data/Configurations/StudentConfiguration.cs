using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("tbl_students");

            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(s => s.Email)
                .IsUnique();

            builder.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(s => s.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.DateOfBirth)
                .IsRequired();

            builder.Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(true);

            builder.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(s => s.PhoneNumber)
                .IsUnique();

            builder.Property(s => s.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ImagePath)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(s => s.Role)
                .HasDefaultValue("Student");
        }
    }
}
