using System.ComponentModel.DataAnnotations;

namespace UniManager.Application.DTOs.Students
{
    public class CreateStudentDto
    {
        public string FullName { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? ImagePath { get; set; }
    }
}
