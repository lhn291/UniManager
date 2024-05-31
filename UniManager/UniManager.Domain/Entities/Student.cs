using System.ComponentModel.DataAnnotations;

namespace UniManager.Domain.Entities
{
    // Sinh viên (Student)
    // Bảng này lưu trữ thông tin về các sinh viên trong hệ thống.

    public class Student
    {
        public string StudentId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = "Student";
        public string? ImagePath { get; set; }

        public ICollection<ExamScore>? ExamScores { get; set; }
        public ICollection<CourseStudent>? CourseStudents { get; set; }
    }
}
