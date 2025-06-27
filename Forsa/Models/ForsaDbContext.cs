using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Forsa.Models;

public partial class ForsaDbContext : DbContext
{
    public ForsaDbContext()
    {
    }

    public ForsaDbContext(DbContextOptions<ForsaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<DonorOrganization> DonorOrganizations { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<FundingProgram> FundingPrograms { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KCF2RKO;Database=ForsaDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC07580DD5C6");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityL__UserI__47DBAE45");
        });

        modelBuilder.Entity<DonorOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DonorOrg__3214EC07F90BDFC7");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC07CBEFBC3D");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.FundingProgram).WithMany(p => p.Favorites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorites__Fundi__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorites__UserI__5DCAEF64");
        });

        modelBuilder.Entity<FundingProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FundingP__3214EC077B0E86C6");

            entity.HasOne(d => d.DonorOrganization).WithMany(p => p.FundingPrograms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FundingPr__Donor__403A8C7D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC073084BF99");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRead).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__6383C8BA");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07B96C75F2");

            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__UserId__440B1D61");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07D86E0EF1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0758FCEC13");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.Users).HasConstraintName("FK__Users__Subscript__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
