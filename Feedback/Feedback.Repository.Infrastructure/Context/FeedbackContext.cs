using System;
using Feedback.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Feedback.Repository.Infrastructure.Context
{
    public partial class FeedbackContext : DbContext
    {
        public FeedbackContext()
        {
        }

        public FeedbackContext(DbContextOptions<FeedbackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserFeedback> UserFeedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Ramiro.allen\\source\\repos\\Feedback\\Feedback.API\\Data\\Feedback.mdf;Integrated Security=True
                optionsBuilder.UseSqlServer("Name=Feedback");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<UserFeedback>(entity =>
            {
                entity.ToTable("UserFeedback");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
