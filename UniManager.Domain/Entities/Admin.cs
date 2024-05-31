namespace UniManager.Domain.Entities
{
    public class Admin
    {
        public string AdminId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "Admin";
    }
}