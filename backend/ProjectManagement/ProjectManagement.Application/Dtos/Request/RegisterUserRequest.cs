using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Application.Dtos.Request
{
    public class RegisterUserRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? OrganizationName { get; set; }
        public Role Role { get; set; }
        public string Domain { get; set; } = null!;
    }
}
