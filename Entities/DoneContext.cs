using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Done.Entities;

public partial class DoneContext : DbContext
{
    public DoneContext()
    {
    }

    public DoneContext(DbContextOptions<DoneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("jobs_pkey");

            entity.ToTable("jobs");

            entity.Property(e => e.JobId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("job_id");
            entity.Property(e => e.JobDescription)
                .HasMaxLength(50)
                .HasColumnName("job_description");
            entity.Property(e => e.JobEnddate).HasColumnName("job_enddate");
            entity.Property(e => e.JobName)
                .HasMaxLength(50)
                .HasColumnName("job_name");
            entity.Property(e => e.JobStartdate).HasColumnName("job_startdate");
            entity.Property(e => e.JobStatusId).HasColumnName("job_status_id");
            entity.Property(e => e.JobStatusPriorityId).HasColumnName("job_status_priority_id");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.Property(e => e.ProjectId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("project_id");
            entity.Property(e => e.ProjectCreatedDate).HasColumnName("project_created_date");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(50)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectPinned).HasColumnName("project_pinned");
            entity.Property(e => e.ProjectUserId).HasColumnName("project_user_id");

            entity.HasOne(d => d.ProjectUser).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.UserAdmin).HasColumnName("user_admin");
            entity.Property(e => e.UserAvatar).HasColumnName("user_avatar");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword).HasColumnName("user_password");
            entity.Property(e => e.UserSurname)
                .HasMaxLength(50)
                .HasColumnName("user_surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
