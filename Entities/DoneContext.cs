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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
