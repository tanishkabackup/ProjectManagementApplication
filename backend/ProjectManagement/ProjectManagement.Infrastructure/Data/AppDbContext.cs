using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -- Organization--
            modelBuilder.Entity<Organization>(entity =>
            {
                
                entity.HasKey(o=>o.OrganizationId);

                entity.Property(o => o.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.Property(o => o.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.Property(o => o.Name).IsRequired().HasMaxLength(500);

                entity.Property(o=>o.Domain).IsRequired().HasMaxLength(200);

                entity.HasMany(o=>o.Users)
                      .WithOne(u=>u.Organization)
                      .HasForeignKey(u=>u.OrganizationId)
                      .OnDelete(DeleteBehavior.Cascade);


                
            });

            // --User--
            modelBuilder.Entity<User>(entity => { 

                entity.HasKey(u=>u.UserId);

                entity.Property(u=>u.FirstName).IsRequired().HasMaxLength(200);

                entity.Property(u => u.LastName).IsRequired().HasMaxLength(200);

                entity.Property(u => u.Email).IsRequired().HasMaxLength(200);

                entity.HasIndex(u=>u.Email).IsUnique();

                entity.Property(u => u.IsEmailVerified).HasDefaultValue(false);

                entity.Property(u => u.VerificationToken).HasMaxLength(100);

                entity.Property(u => u.PasswordHash).IsRequired();

                entity.Property(u => u.Role).IsRequired();

                entity.Property(o => o.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.Property(o => o.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });


        }

    }
}
