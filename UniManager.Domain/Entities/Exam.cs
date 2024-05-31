namespace UniManager.Domain.Entities
{
    public class Exam
    {
        public string ExamID { get; set; } = null!;
        public string ExamName { get; set; } = null!;
        public DateTime ExamDate { get; set; }
        public string SubjectID { get; set; } = null!;

        public Subject Subject { get; set; } = null!;
        public ICollection<ExamScore> ExamScores { get; set; } = new HashSet<ExamScore>();
    }
}