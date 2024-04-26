using DataAccess.Enums;

namespace Domain.Models;

public class User
{
    public int? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? PasswordHash { get; set; }
    public UserRole? Role { get; set; }
}