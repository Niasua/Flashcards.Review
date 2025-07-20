using Microsoft.EntityFrameworkCore;
using Flashcards.Niasua.Models;

namespace Flashcards.Niasua.Data;

public class AppDbContext : DbContext
{
    public DbSet<Stack> Stacks { get; set; }
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FlashcardsDB;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stack>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<Stack>()
            .HasMany(s => s.Flashcards)
            .WithOne(f => f.Stack)
            .HasForeignKey(f => f.StackId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Stack>()
            .HasMany(s => s.StudySessions)
            .WithOne(st => st.Stack)
            .HasForeignKey(st => st.StackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
