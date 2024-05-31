namespace UniManager.Application.DTOs.Students
{
    public class StudentScoreDto
    {
        public string StudentId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Score { get; set; }
    }
}
