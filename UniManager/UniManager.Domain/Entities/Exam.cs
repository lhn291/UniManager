namespace UniManager.Domain.Entities
{
    // Kỳ thi (Exam)
    // Bảng này lưu trữ thông tin về các kỳ thi của các khóa học trong hệ thống.
    public class Exam
    {
        public int ExamID { get; set; }
        public string CourseID { get; set; } = null!;
        public string ExamName { get; set; } = null!;
        public DateTime ExamDate { get; set; }

        public Course? Course { get; set; }
        public ICollection<ExamScore>? ExamScores { get; set; }
    }
}
