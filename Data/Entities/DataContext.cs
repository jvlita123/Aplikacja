using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class DataContext : DbContext
    {

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Reservation1> Reservation1 { get; set; }
        public DbSet<ReservationSlots> ReservationSlots { get; set; }
        public DbSet<UserReservationSlots> UserReservationSlots { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Cycle> Cycles { get; set; }
        public DbSet<CoursesPerCycle> CoursesPerCycle { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<Message> Message { get; set; }
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseSqlServer("Server=tcp:applicationserver.database.windows.net,1433;database=ApplicationDB;User ID=jvlita123;Password=admin123.;");

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
