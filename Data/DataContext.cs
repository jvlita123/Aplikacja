using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Entities.Service> Service { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Cycle> Cycles { get; set; }
        public DbSet<CoursesPerCycle> CoursesPerCycle { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=applicationserver.database.windows.net,1433;database=ApplicationDB;Persist Security Info=True;User ID=jvlita123;password=admin123.;");

        public void AddEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Add(entity);
        }

        public void AddEntityAndSaveChanges<TEntity>(TEntity entity) where TEntity : class, new()
        {
            AddEntity(entity);
            SaveChanges();
        }

        public void AddEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().AddRange(entity);
        }

        public void AddEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            AddEntitiesRange(entity);
            SaveChanges();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void UpdateEntitiesRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }

        public void UpdateEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            UpdateEntitiesRange(entities);
            SaveChanges();
        }

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Remove(entity);
        }

        public void RemoveEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().RemoveRange(entity);
        }

        public void RemoveEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            RemoveEntitiesRange(entity);
            SaveChanges();
        }
    }
}
