using Microsoft.EntityFrameworkCore;
using Eventify.Backend.UserService.Infrastructure.Entities;

namespace Eventify.Backend.UserService.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
     

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "eventify-db.carou00acerl.us-east-1.rds.amazonaws.com",
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3, 
                        maxRetryDelay: TimeSpan.FromSeconds(30), 
                        errorNumbersToAdd: null)
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
