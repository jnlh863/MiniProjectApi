using GestiondeEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestiondeEventos.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Notify> Notifications { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.organizerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notify>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.userId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notify>()
                .HasOne(n => n.Event)
                .WithMany(e => e.Notifications)
                .HasForeignKey(n => n.eventId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
