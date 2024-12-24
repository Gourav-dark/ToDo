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
    }
}
