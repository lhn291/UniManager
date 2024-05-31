using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("tbl_exams");

            builder.HasKey(e => e.ExamID);
            builder.Property(e => e.ExamID).IsRequired().HasMaxLength(50);
            builder.Property(e => e.ExamName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.ExamDate).IsRequired();

            builder.HasOne(e => e.Subject)
                   .WithMany(s => s.Exams)
                   .HasForeignKey(e => e.SubjectID);
        }
    }
}