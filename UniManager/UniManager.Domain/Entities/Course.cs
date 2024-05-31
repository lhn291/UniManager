namespace UniManager.Domain.Entities
{
    // Khóa học (Course)
    // Bảng này lưu trữ thông tin về các khóa học trong hệ thống.
    public class Course
    {
        public string CourseID { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string? LecturerId { get; set; } 
        public Lecturer? Lecturer { get; set; }

        public ICollection<Exam>? Exams { get; set; }
        public ICollection<CourseStudent>? CourseStudents { get; set; }
    }
}
