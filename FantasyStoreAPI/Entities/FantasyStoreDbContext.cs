using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FantasyStoreAPI.Entities;

public partial class FantasyStoreDbContext : DbContext
{
    public FantasyStoreDbContext()
    {
    }

    public FantasyStoreDbContext(DbContextOptions<FantasyStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC07CD64DE38");

            entity.ToTable("Inventory");

            entity.Property(e => e.ItemName).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07FD2E3D82");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Orders__ItemId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
