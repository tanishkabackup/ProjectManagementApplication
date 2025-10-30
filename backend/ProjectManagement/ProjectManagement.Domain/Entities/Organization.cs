namespace ProjectManagement.Domain.Entities
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; } = null!;
        public string? Domain { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
