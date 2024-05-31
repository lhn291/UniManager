namespace UniManager.Domain.Entities
{
    public class CourseSubject
    {
        public string CourseID { get; set; } = null!;
        public string SubjectID { get; set; } = null!;

        public Course Course { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
    }
}