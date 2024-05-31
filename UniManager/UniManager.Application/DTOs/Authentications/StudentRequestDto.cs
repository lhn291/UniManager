﻿namespace UniManager.Application.DTOs.Authentications
{
    public class StudentRequestDto
    {
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = "Student";
        public string? ImagePath { get; set; }
    }
}
