using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class CourseSubjectConfiguration : IEntityTypeConfiguration<CourseSubject>
    {
        public void Configure(EntityTypeBuilder<CourseSubject> builder)
        {
            builder.ToTable("tbl_course_subjects");

            builder.HasKey(cs => new { cs.CourseID, cs.SubjectID });

            builder.HasOne(cs => cs.Course)
                   .WithMany(c => c.CourseSubjects)
                   .HasForeignKey(cs => cs.CourseID);

            builder.HasOne(cs => cs.Subject)
                   .WithMany(s => s.CourseSubjects)
                   .HasForeignKey(cs => cs.SubjectID);
        }
    }
}