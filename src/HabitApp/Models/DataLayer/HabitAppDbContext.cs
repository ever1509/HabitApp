using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitApp.Models.DataLayer
{
    public class HabitAppDbContext : DbContext
    {
        public HabitAppDbContext()
        {

        }
        public HabitAppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        #region DbSets
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ErrorLog> Errors { get; set; }
        #endregion

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB; Initial Catalog=HabitAppDb; Integrated Security=SSPI;");
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Map(modelBuilder.Entity<Question>());
            Map(modelBuilder.Entity<Answer>());
            Map(modelBuilder.Entity<Habit>());
            Map(modelBuilder.Entity<User>());
            Map(modelBuilder.Entity<Role>());
            Map(modelBuilder.Entity<UserRole>());
            Map(modelBuilder.Entity<ErrorLog>());
            base.OnModelCreating(modelBuilder);
        }

        #region Mappings
        private static void Map(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.QuestionId);
            builder.Property(q => q.QuestionId).UseSqlServerIdentityColumn();
            builder.Property(q => q.QuestionId).HasColumnType("int").IsRequired();
            builder.Property(q => q.HabitId).HasColumnType("int").IsRequired(false);
            builder.Property(q => q.QuestionDescription).HasColumnType("varchar(150)").IsRequired(false);
            builder.Property(q => q.QuestionDate).HasColumnType("datetime").IsRequired(false);

            builder.HasOne(x => x.FkHabit).WithMany(x => x.Questions).HasForeignKey(x => x.HabitId)
                .HasConstraintName("FK_Question_Habits").OnDelete(DeleteBehavior.ClientSetNull);

        }
        private static void Map(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(a => a.AnswerId);
            builder.Property(a => a.AnswerId).UseSqlServerIdentityColumn();
            builder.Property(a => a.AnswerId).HasColumnType("int").IsRequired();
            builder.Property(a => a.QuestionId).HasColumnType("int").IsRequired(false);
            builder.Property(a => a.AnswerDescription).HasColumnType("varchar(150)").IsRequired(false);

            builder.HasOne(a => a.FkQuestion).WithMany(a => a.Answers).HasForeignKey(a => a.QuestionId)
                .HasConstraintName("FK_Answers_Questions").OnDelete(DeleteBehavior.ClientSetNull);
        }

        private static void Map(EntityTypeBuilder<Habit> builder)
        {
            builder.ToTable("Habits");
            builder.HasKey(h => h.HabitId);
            builder.Property(h => h.HabitId).UseSqlServerIdentityColumn();
            builder.Property(h => h.HabitId).HasColumnType("int").IsRequired();
            builder.Property(h => h.HabitDate).HasColumnType("datetime").IsRequired(false);
            builder.Property(h => h.HabitDescription).HasColumnType("varchar(150)").IsRequired(false);
        }

        private static void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId).UseSqlServerIdentityColumn();
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Salt).IsRequired().HasMaxLength(200);
            builder.Property(u => u.IsLocked).IsRequired();
            builder.Property(u => u.DateCreated);
        }

        private static void Map(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(r => r.RoleId);
            builder.Property(r => r.RoleId).UseSqlServerIdentityColumn();
            builder.Property(r => r.RoleId).IsRequired();
            builder.Property(r => r.RoleName).HasMaxLength(50);

        }

        private static void Map(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(ur => ur.UserRoleId);
            builder.Property(ur => ur.UserRoleId).UseSqlServerIdentityColumn();
            builder.Property(ur => ur.UserRoleId).IsRequired();
            builder.Property(ur => ur.UserId).IsRequired();
            builder.Property(ur => ur.RoleId).IsRequired();

        }
        private static void Map(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.ToTable("Errors");
            builder.HasKey(e => e.ErrorId);
            builder.Property(e => e.ErrorId).UseSqlServerIdentityColumn();


        }
        #endregion
    }
}
