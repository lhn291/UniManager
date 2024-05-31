namespace UniManager.Application.DTOs.Courses
{
    public class CreateCourseDto
    {
        public string CourseID { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
