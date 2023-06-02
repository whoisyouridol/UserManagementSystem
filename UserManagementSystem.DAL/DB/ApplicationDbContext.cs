using Microsoft.EntityFrameworkCore;
using UserManagementSystem.DAL.DB.Models;

namespace UserManagementSystem.DAL.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<UserProfile>().HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<UserProfile>(x => x.UserId);

            modelBuilder.Entity<UserProfile>()
                .Property(x => x.PersonalNumber)
                .HasMaxLength(11);
        }
    }
}
