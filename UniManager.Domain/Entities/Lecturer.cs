namespace UniManager.Domain.Entities
{
    public class Lecturer
    {
        public string LecturerId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;

        public ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
    }
}