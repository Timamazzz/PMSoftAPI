using DataAccess.Enums;

namespace PMSoftAPI.Models;

    public class UserCreate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole? Role { get; set; } = UserRole.User;
    }

    public class UserGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
    }
