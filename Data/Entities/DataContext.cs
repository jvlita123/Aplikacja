using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Reservation1> Reservation1 { get; set; }
        public DbSet<ReservationSlots> ReservationSlots { get; set; }
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
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Block> Blocks { get; set; }
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
/*            modelBuilder.Entity<Message>()
                .HasOne(m => m.User1)
                .WithMany(u => u.messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);*/

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Photo__3214EC07C3C1BADB");

                entity.ToTable("Photo");

                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User).WithMany(p => p.Photos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Photo__UserId__540C7B00");
            });


            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Message__3214EC073A725353");

                entity.ToTable("Message");

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User1).WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__UserId__503BEA1C");

                entity.HasOne(d => d.User2).WithMany()
                    .HasForeignKey(d => d.UserId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__UserId2__51300E55");
            });


            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Message__3214EC073A725353");

                entity.ToTable("Message");

                entity.Property(e => e.IsNew).HasDefaultValueSql("((1))");
                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC071993B37F");

                entity.ToTable("Attendance");

                entity.HasOne(d => d.Course).WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Cours__40058253");

                entity.HasOne(d => d.Cycle).WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.CycleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Cycle__40F9A68C");

                entity.HasOne(d => d.User).WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__UserI__41EDCAC5");
            });

            /*            modelBuilder.Entity<Message>()
                            .HasOne(m => m.User2)
                            .WithMany()
                            .HasForeignKey(m => m.UserId2)
                            .OnDelete(DeleteBehavior.ClientSetNull);*/

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Reservations)
                    .WithOne(r => r.User)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Reservation1>(entity =>
            {
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(r => r.ReservationSlot)
                    .WithOne(rs => rs.Reservation)
                    .HasForeignKey<ReservationSlots>(rs => rs.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ReservationSlots>(entity =>
            {
                entity.HasOne(rs => rs.Reservation)
                    .WithOne(r => r.ReservationSlot)
                    .HasForeignKey<ReservationSlots>(rs => rs.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Status__3214EC07694FE6B5");

                entity.ToTable("Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Service__3214EC0791B2BCF6");

                entity.ToTable("Service");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Cycle>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Cycles__3214EC078C29BF1D");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.SourcePath)
                    .HasMaxLength(300)
                    .IsUnicode(false);
                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course).WithMany(p => p.Cycles)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Cycles__CourseId__607251E5");
            });
            base.OnModelCreating(modelBuilder);
        }
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
