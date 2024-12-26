using Microsoft.EntityFrameworkCore;
using ToDo.Shared.Models;

namespace ToDo.Shared.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    // DbSets for models
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Exam> Exams { get; set; }
    public virtual DbSet<TodoItem> Todos { get; set; }
    public virtual DbSet<DailyTaskPerformance> TaskPerformances { get; set; }
    public virtual DbSet<DailyTask> DailyTasks { get; set; }
    public virtual DbSet<SecureData> SecureDatas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>()
            .Property(x => x.Priority)
            .HasConversion<string>()
            .HasMaxLength(50);
        modelBuilder.Entity<TodoItem>()
            .Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50);
        modelBuilder.Entity<User>()
            .Property(x => x.UserType)
            .HasConversion<string>()
            .HasMaxLength(50);
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Personal", UserId = null },
            new Category { CategoryId = 2, Name = "Home", UserId = null },
            new Category { CategoryId = 3, Name = "Work", UserId = null },
            new Category { CategoryId = 4, Name = "Sport", UserId = null },
            new Category { CategoryId = 5, Name = "Music", UserId = null },
            new Category { CategoryId = 6, Name = "Movie", UserId = null },
            new Category { CategoryId = 7, Name = "Grocery", UserId = null },
            new Category { CategoryId = 8, Name = "Education", UserId = null },
            new Category { CategoryId = 9, Name = "Others", UserId = null }
            );
    }
}
