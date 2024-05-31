namespace UniManager.Application.DTOs.Courses
{
    public class CourseDto
    {
        public string CourseID { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? LecturerId { get; set; }
    }
}
