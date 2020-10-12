using BAI_APP.Models;

using Microsoft.EntityFrameworkCore;

namespace BAI_APP.Context
{
    public class BaiContext : DbContext
    {
        public BaiContext()
        {

        }

        public BaiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageModerator> MessageUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<MessageModerator>().ToTable("MessageUser");
        }
    }
}
