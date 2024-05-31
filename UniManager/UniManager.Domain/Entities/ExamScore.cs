namespace UniManager.Domain.Entities
{
    // Điểm kỳ thi (ExamScore)
    // Bảng này lưu trữ điểm của các học sinh trong các kỳ thi.
    public class ExamScore
    {
        public int ScoreID { get; set; }
        public int ExamID { get; set; }
        public string StudentId { get; set; } = null!;
        public decimal Score { get; set; }

        public Exam? Exam { get; set; }
        public Student? Student { get; set; }
    }
}
