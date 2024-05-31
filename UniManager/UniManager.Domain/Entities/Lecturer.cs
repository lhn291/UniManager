using System.ComponentModel.DataAnnotations;

namespace UniManager.Domain.Entities
{
    // Giảng viên (Lecturer)
    // Bảng này lưu trữ thông tin về các giảng viên trong hệ thống.
    public class Lecturer
    {
        public string LecturerId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = "Lecturer";

        public ICollection<Course>? Courses { get; set; }
    }
}
