using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class ExamScoreConfiguration : IEntityTypeConfiguration<ExamScore>
    {
        public void Configure(EntityTypeBuilder<ExamScore> builder)
        {
            builder.ToTable("ExamScores");

            builder.HasKey(es => es.ScoreID);

            builder.HasOne(es => es.Exam)
                .WithMany(e => e.ExamScores)
                .HasForeignKey(es => es.ExamID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(es => es.Student)
                .WithMany(s => s.ExamScores)
                .HasForeignKey(es => es.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
