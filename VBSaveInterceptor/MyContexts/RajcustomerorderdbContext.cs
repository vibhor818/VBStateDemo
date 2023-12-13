using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VBSaveInterceptor.VBModels;

namespace VBSaveInterceptor.MyContexts;

public partial class RajcustomerorderdbContext : DbContext
{
    public RajcustomerorderdbContext()
    {
    }

    public RajcustomerorderdbContext(DbContextOptions<RajcustomerorderdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\mssqllocaldb;database=rajcustomerorderdb;trusted_connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomersHistory", "dbo");
                        ttb
                            .HasPeriodStart("PeriodStart")
                            .HasColumnName("PeriodStart");
                        ttb
                            .HasPeriodEnd("PeriodEnd")
                            .HasColumnName("PeriodEnd");
                    }));
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries=ChangeTracker.Entries<BaseDomainModel>().Where(q=>q.State==EntityState.Modified || q.State==EntityState.Added);

        foreach (var entry in entries)
        {
            entry.Entity.ModifiedDate = DateTime.Now;
            entry.Entity.ModifiedBy = "Admin User 1";
            if (entry.State==EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.Now;
                entry.Entity.CreatedBy = "Admin User1";
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
