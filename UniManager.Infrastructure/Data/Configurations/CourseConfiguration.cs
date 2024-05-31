using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("tbl_courses");

            builder.HasKey(c => c.CourseID);

            builder.Property(c => c.CourseID).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CourseName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(1000);

            builder.HasMany(c => c.Students)
                   .WithOne(s => s.Course)
                   .HasForeignKey(s => s.CourseID)
                   .IsRequired(false);

        }
    }
}