namespace UniManager.Domain.Entities
{
    // Thực thể Admin
    // Bảng này lưu trữ thông tin về các quản trị viên có quyền hạn cao nhất trong hệ thống.
    public class Admin
    {
        public string AdminId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "Admin";
    }
}
