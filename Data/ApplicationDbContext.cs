using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Models;

namespace StudentEventManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.StudentId, r.EventId })
                .IsUnique();

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Feedbacks)
                .WithOne(f => f.Event)
                .HasForeignKey(f => f.EventId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Registrations)
                .WithOne(r => r.Student)
                .HasForeignKey(r => r.StudentId);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Registrations)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId);

            modelBuilder.Entity<Event>().HasData(
         new Event
         {
             Id = 1,
             Name = "Web Tech Seminar",
             // Use a specific, fixed date. Make it a UTC date for consistency if using UtcNow elsewhere
             Date = new DateTime(2025, 7, 15, 10, 0, 0, DateTimeKind.Utc), // Example: July 15, 2025, 10:00 AM UTC
             Venue = "Auditorium A",
             Description = "Intro to modern web technologies"
         },
         new Event
         {
             Id = 2,
             Name = "AI Workshop",
             // Use another specific, fixed date
             Date = new DateTime(2025, 8, 1, 14, 0, 0, DateTimeKind.Utc), // Example: August 1, 2025, 2:00 PM UTC
             Venue = "Lab C",
             Description = "Hands-on AI workshop"
         }
     );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Alice Wonderland", Email = "alice@example.com" },
                new Student { Id = 2, Name = "Bob The Builder", Email = "bob@example.com" }
            );

            // ... any other HasData calls should also use static values ...
        }
    }
}