using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("tbl_students");

            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.StudentId).IsRequired().HasMaxLength(50);
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.DateOfBirth).IsRequired();
            builder.Property(s => s.Address).IsRequired().HasMaxLength(150);
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Password).IsRequired().HasMaxLength(50);
            builder.Property(s => s.UserName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Role).IsRequired().HasMaxLength(50);
            builder.Property(s => s.ImagePath).HasMaxLength(255);

            builder.HasOne(s => s.Course)
                   .WithMany(c => c.Students)
                   .HasForeignKey(s => s.CourseID)
                   .IsRequired(true);
        }
    }
}