using BAI_APP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BAI_APP.Context
{
    public class BaiContext : DbContext
    {
        public BaiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageUser> MessageUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<MessageUser>().ToTable("MessageUser");
        }
    }
}
