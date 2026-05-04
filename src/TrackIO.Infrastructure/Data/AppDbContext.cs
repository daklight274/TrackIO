using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrackIO.Domain.Entities;

namespace TrackIO.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
        public DbSet<Sprint> Sprints => Set<Sprint>();
        public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Email).IsRequired().HasMaxLength(100);
                e.Property(x => x.Username).IsRequired().HasMaxLength(100);
                e.Property(x => x.PasswordHash).IsRequired().HasMaxLength(2000);
            });

            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.Description).HasMaxLength(2000);

                e.HasOne(x => x.Owner)
                 .WithMany()
                 .HasForeignKey(x => x.OwnerId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProjectMember>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Role).HasConversion<string>();

                e.HasOne(x => x.User)
                 .WithMany()
                 .HasForeignKey(x => x.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Project)
                 .WithMany()
                 .HasForeignKey(x => x.ProjectId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => new { x.ProjectId, x.UserId }).IsUnique();
            });

            modelBuilder.Entity<ProjectTask>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).HasMaxLength(500).IsRequired();
                e.Property(x => x.Description).HasMaxLength(1000);
                e.Property(x => x.BlockedReason).HasMaxLength(1000);
                e.Property(x => x.Status).HasConversion<string>();
                e.Property(x => x.Priority).HasConversion<string>();

                e.HasOne(x => x.Assignee)
                 .WithMany()
                 .HasForeignKey(x => x.AssigneeId)
                 .OnDelete(DeleteBehavior.SetNull);

                e.HasOne(x => x.Project)
                 .WithMany()
                 .HasForeignKey(x => x.ProjectId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Sprint)
                 .WithMany()
                 .HasForeignKey(x => x.SprintId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Comment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Content).HasMaxLength(1000);

                e.HasOne(x => x.Author)
                 .WithMany()
                 .HasForeignKey(x => x.AuthorId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Task)
                 .WithMany()
                 .HasForeignKey(x => x.TaskId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Attachment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.FileName).HasMaxLength(1000);
                e.Property(x => x.FilePath).HasMaxLength(1000);
            });

            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Token).HasMaxLength(100).IsRequired();

                e.HasOne(x => x.User)
                 .WithMany()
                 .HasForeignKey(x => x.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
