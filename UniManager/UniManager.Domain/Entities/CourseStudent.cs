namespace UniManager.Domain.Entities
{
    public class CourseStudent
    {
        public string? StudentId { get; set; } 
        public Student? Student { get; set; }

        public string? CourseId { get; set; } 
        public Course? Course { get; set; }
    }
}
