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

    public virtual DbSet<Assigment> Assigments { get; set; }

    public virtual DbSet<GroupChat> GroupChats { get; set; }

    public virtual DbSet<GroupMessage> GroupMessages { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectsUser> ProjectsUsers { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("server=127.0.0.1;uid=postgres;pwd=2004Pawel!;database=done;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assigment>(entity =>
        {
            entity.HasKey(e => e.AssigmentId).HasName("assigments_pkey");

            entity.ToTable("assigments");

            entity.Property(e => e.AssigmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("assigment_id");
            entity.Property(e => e.AssigmentJobId).HasColumnName("assigment_job_id");
            entity.Property(e => e.AssigmentUserId).HasColumnName("assigment_user_id");

            entity.HasOne(d => d.AssigmentJob).WithMany(p => p.Assigments)
                .HasForeignKey(d => d.AssigmentJobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_assigment_id");

            entity.HasOne(d => d.AssigmentUser).WithMany(p => p.Assigments)
                .HasForeignKey(d => d.AssigmentUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_assigment_user_id");
        });

        modelBuilder.Entity<GroupChat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("group_chats_pkey");

            entity.ToTable("group_chats");

            entity.Property(e => e.ChatId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("chat_id");
            entity.Property(e => e.ChatProjectId).HasColumnName("chat_project_id");

            entity.HasOne(d => d.ChatProject).WithMany(p => p.GroupChats)
                .HasForeignKey(d => d.ChatProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_project_id");
        });

        modelBuilder.Entity<GroupMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("group_messages_pkey");

            entity.ToTable("group_messages");

            entity.Property(e => e.MessageId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("message_id");
            entity.Property(e => e.MessageChatId).HasColumnName("message_chat_id");
            entity.Property(e => e.MessageContent)
                .HasMaxLength(255)
                .HasColumnName("message_content");
            entity.Property(e => e.MessageUserId).HasColumnName("message_user_id");

            entity.HasOne(d => d.MessageChat).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.MessageChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_meessage_chat_id");

            entity.HasOne(d => d.MessageUser).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.MessageUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_meessage_user_id");
        });

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
            entity.Property(e => e.JobPriorityId).HasColumnName("job_priority_id");
            entity.Property(e => e.JobProjectId).HasColumnName("job_project_id");
            entity.Property(e => e.JobStartdate).HasColumnName("job_startdate");
            entity.Property(e => e.JobStatusId).HasColumnName("job_status_id");
            entity.Property(e => e.JobStatusPriorityId).HasColumnName("job_status_priority_id");

            entity.HasOne(d => d.JobPriority).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobPriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_job_priority_id");

            entity.HasOne(d => d.JobProject).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_id");

            entity.HasOne(d => d.JobStatus).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jobs_status_id");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("message_id");
            entity.Property(e => e.MessageContent)
                .HasMaxLength(255)
                .HasColumnName("message_content");
            entity.Property(e => e.MessageCreatorId).HasColumnName("message_creator_id");
            entity.Property(e => e.MessageRecipientId).HasColumnName("message_recipient_id");

            entity.HasOne(d => d.MessageCreator).WithMany(p => p.MessageMessageCreators)
                .HasForeignKey(d => d.MessageCreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message_creator_id");

            entity.HasOne(d => d.MessageRecipient).WithMany(p => p.MessageMessageRecipients)
                .HasForeignKey(d => d.MessageRecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_message_recipe_id");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("priorities_pkey");

            entity.ToTable("priorities");

            entity.Property(e => e.PriorityId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("priority_id");
            entity.Property(e => e.PriorityName)
                .HasMaxLength(40)
                .HasColumnName("priority_name");
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
        });

        modelBuilder.Entity<ProjectsUser>(entity =>
        {
            entity.HasKey(e => e.ProjectUserId).HasName("projects_users_pkey");

            entity.ToTable("projects_users");

            entity.Property(e => e.ProjectUserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("project_user_id");
            entity.Property(e => e.ProjectAdmin).HasColumnName("project_admin");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectsUsers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_projects_user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectsUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_projects_users_user_id");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("statuses_pkey");

            entity.ToTable("statuses");

            entity.Property(e => e.StatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
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
