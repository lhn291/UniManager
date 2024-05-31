namespace UniManager.Domain.Entities
{
    public class Subject
    {
        public string SubjectID { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string LecturerId { get; set; } = null!;

        public Lecturer Lecturer { get; set; } = null!;
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new HashSet<CourseSubject>();
        public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
    }
}