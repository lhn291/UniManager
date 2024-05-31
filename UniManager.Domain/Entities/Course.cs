namespace UniManager.Domain.Entities
{
    public class Course
    {
        public string CourseID { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<CourseSubject> CourseSubjects { get; set; } = new HashSet<CourseSubject>();
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
