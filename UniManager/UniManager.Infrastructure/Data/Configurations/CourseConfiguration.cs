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

            builder.HasIndex(c => c.CourseID);

            builder.Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(c => c.Description)
                .IsRequired()
                .IsUnicode(true);

            builder.HasOne(c => c.Lecturer)
                .WithMany(l => l.Courses)
                .HasForeignKey(c => c.LecturerId)
                .OnDelete(DeleteBehavior.Restrict); 
            // Đặt tùy chọn OnDelete thành Restrict để tắt xóa đệ quy
        }
    }
}
