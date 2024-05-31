using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class CourseStudentConfiguration : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            builder.ToTable("tbl_course_students");

            builder.HasKey(cs => new { cs.StudentId, cs.CourseId });

            builder.HasOne(cs => cs.Student)
                .WithMany(s => s!.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);

            builder.HasOne(cs => cs.Course)
                .WithMany(c => c!.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);
        }
    }
}
