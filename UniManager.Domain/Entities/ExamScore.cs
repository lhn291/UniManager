namespace UniManager.Domain.Entities
{
    public class ExamScore
    {
        public long ScoreID { get; set; }
        public string ExamID { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public decimal Score { get; set; }

        public Exam Exam { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}