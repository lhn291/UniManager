using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class ExamScoreConfiguration : IEntityTypeConfiguration<ExamScore>
    {
        public void Configure(EntityTypeBuilder<ExamScore> builder)
        {
            builder.ToTable("tbl_exam_scores");

            builder.HasKey(es => es.ScoreID);

            builder.Property(es => es.ScoreID).IsRequired();
            builder.Property(es => es.Score).IsRequired().HasColumnType("decimal(5, 2)");

            builder.HasOne(es => es.Exam)
                   .WithMany(e => e.ExamScores)
                   .HasForeignKey(es => es.ExamID);

            builder.HasOne(es => es.Student)
                   .WithMany(s => s.ExamScores)
                   .HasForeignKey(es => es.StudentId);
        }
    }
}