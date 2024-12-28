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
    public virtual DbSet<TaskItem> Tasks { get; set; }
    public virtual DbSet<DailyTaskPerformance> TaskPerformances { get; set; }
    public virtual DbSet<SecureData> SecureDatas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskItem>()
            .Property(x => x.Priority)
            .HasConversion<string>()
            .HasMaxLength(50);
        modelBuilder.Entity<TaskItem>()
            .Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50);
        modelBuilder.Entity<User>()
            .Property(x => x.UserType)
            .HasConversion<string>()
            .HasMaxLength(50);
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Personal", ColorCode = "#FF5733", UserId = null }, // Orange
            new Category { CategoryId = 2, Name = "Home", ColorCode = "#33FF57", UserId = null },     // Green
            new Category { CategoryId = 3, Name = "Work", ColorCode = "#3357FF", UserId = null },     // Blue
            new Category { CategoryId = 4, Name = "Sport", ColorCode = "#FFC300", UserId = null },    // Yellow
            new Category { CategoryId = 5, Name = "Music", ColorCode = "#C70039", UserId = null },    // Red
            new Category { CategoryId = 6, Name = "Movie", ColorCode = "#900C3F", UserId = null },    // Maroon
            new Category { CategoryId = 7, Name = "Grocery", ColorCode = "#581845", UserId = null },  // Purple
            new Category { CategoryId = 8, Name = "Education", ColorCode = "#1ABC9C", UserId = null }, // Teal
            new Category { CategoryId = 9, Name = "Others", ColorCode = "#BDC3C7", UserId = null }     // Grey
        );
        //Test Data for Check and Delete after use
        //026ff00a - 4f70 - 474d - b118 - df00c609f620
        modelBuilder.Entity<User>().HasData(
            new User { UserId = "1c63b500-e915-4430-8eaf-68a2983d92e7", Email="admin@gmail.com",Password="Admin@123",UserType=UserType.Admin,CreatedAt=DateTime.UtcNow, },
            new User { UserId = "026ff00a-4f70-474d-b118-df00c609f620", Email = "test@gmail.com", Password = "12345", UserType = UserType.User, CreatedAt = DateTime.UtcNow, }
        );
        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem { TaskId = 1, Title = "Math Homework", Description = "", DueTime = DateTime.Now.AddDays(1), Priority = TodoPriority.Medium, Status = TodoStatus.Pending, IsDailyTask = false, CreatedAt = DateTime.UtcNow, CategoryId = 1, UserId = "026ff00a-4f70-474d-b118-df00c609f620" },
            new TaskItem { TaskId = 2, Title = "Gym Training", Description = "6 AM To 7 AM", DueTime = DateTime.Now.AddDays(1), Priority = TodoPriority.High, Status = TodoStatus.Pending, IsDailyTask = true, CreatedAt = DateTime.UtcNow, CategoryId = 4, UserId = "026ff00a-4f70-474d-b118-df00c609f620" },
            new TaskItem { TaskId = 3, Title = "Morning Walk", Description = "5 AM to 5:30 AM", DueTime = DateTime.Now.AddDays(1), Priority = TodoPriority.High, Status = TodoStatus.Pending, IsDailyTask = true, CreatedAt = DateTime.UtcNow, CategoryId = 4, UserId = "026ff00a-4f70-474d-b118-df00c609f620" },
            new TaskItem { TaskId = 4, Title = "Client Meeting", Description = "7 PM To 8 PM", DueTime = DateTime.Now.AddDays(1), Priority = TodoPriority.High, Status = TodoStatus.Pending, IsDailyTask = true, CreatedAt = DateTime.UtcNow, CategoryId = 3, UserId = "026ff00a-4f70-474d-b118-df00c609f620" },
            new TaskItem { TaskId = 5, Title = "Leet Code", Description = "", DueTime = DateTime.Now.AddDays(1), Priority = TodoPriority.High, Status = TodoStatus.Pending, IsDailyTask = true, CreatedAt = DateTime.UtcNow, CategoryId = 8, UserId = "026ff00a-4f70-474d-b118-df00c609f620" }
            );
    }
}