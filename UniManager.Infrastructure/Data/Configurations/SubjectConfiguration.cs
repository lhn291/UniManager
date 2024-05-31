using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniManager.Domain.Entities;

namespace UniManager.Infrastructure.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("tbl_subjects");

            builder.HasKey(s => s.SubjectID);

            builder.Property(s => s.SubjectID).IsRequired().HasMaxLength(50);
            builder.Property(s => s.SubjectName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Description).IsRequired().HasMaxLength(1000);

            builder.HasOne(s => s.Lecturer)
                   .WithMany(l => l.Subjects)
                   .HasForeignKey(s => s.LecturerId)
                   .IsRequired();

        }
    }
}