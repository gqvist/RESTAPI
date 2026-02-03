using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Data
{
    public class RestApiDBContext : DbContext
    {
        public RestApiDBContext(DbContextOptions<RestApiDBContext> options) : base(options){ }

        public DbSet<Person> People => Set<Person>();
        public DbSet<Interest> Interests => Set<Interest>();
        public DbSet<PersonInterest> PersonInterests => Set<PersonInterest>();
        public DbSet<PersonInterestLink> PersonInterestLinks => Set<PersonInterestLink>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite primary key for PersonInterest
            modelBuilder.Entity<PersonInterest>()
            .HasKey(pi => new { pi.PersonId, pi.InterestId });

            // Configure relationships
            modelBuilder.Entity<PersonInterest>()
                .HasOne(pi => pi.Person)
                .WithMany(p => p.PersonInterests)
                .HasForeignKey(pi => pi.PersonId);

            // Configure relationships
            modelBuilder.Entity<PersonInterest>()
                .HasOne(pi => pi.Interest)
                .WithMany(i => i.PersonInterests)
                .HasForeignKey(pi => pi.InterestId);

            // Configure relationship for PersonInterestLink to PersonInterest
            modelBuilder.Entity<PersonInterestLink>()
                .HasOne(l => l.PersonInterest)
                .WithMany(pi => pi.Links)
                .HasForeignKey(l => new { l.PersonId, l.InterestId });
        }
    }
}
