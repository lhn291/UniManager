namespace UniManager.Domain.Entities
{
    public class Student
    {
        public string StudentId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? ImagePath { get; set; }

        public string CourseID { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public ICollection<ExamScore> ExamScores { get; set; } = new HashSet<ExamScore>();
    }
}