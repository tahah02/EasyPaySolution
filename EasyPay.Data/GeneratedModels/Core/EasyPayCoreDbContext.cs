using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EasyPay.Data.GeneratedModels.Core;

public partial class EasyPayCoreDbContext : DbContext
{
    public EasyPayCoreDbContext()
    {
    }

    public EasyPayCoreDbContext(DbContextOptions<EasyPayCoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankBranch> BankBranches { get; set; }

    public virtual DbSet<EasyPayLog> EasyPayLogs { get; set; }

    public virtual DbSet<FullAuditReport> FullAuditReports { get; set; }

    public virtual DbSet<MasterAuditReport> MasterAuditReports { get; set; }

    public virtual DbSet<PaymentRecord> PaymentRecords { get; set; }

    public virtual DbSet<ResponseBody> ResponseBodies { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-BNT57LQ\\MSSQLSERVER01;Database=EasyPayDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankBranch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__BankBran__A1682FC50B09B5B9");

            entity.Property(e => e.BranchName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EasyPayLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EasyPayLog");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.ClosingBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Controller).HasMaxLength(100);
            entity.Property(e => e.Method).HasMaxLength(10);
            entity.Property(e => e.RequestTime).HasColumnType("datetime");
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.TransferredAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<FullAuditReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FullAuditReport");

            entity.Property(e => e.ClosingBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Method).HasMaxLength(10);
            entity.Property(e => e.RequestTime).HasColumnType("datetime");
            entity.Property(e => e.TransferredAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<MasterAuditReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MasterAuditReport");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.AuditLogId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AuditLogID");
            entity.Property(e => e.ChannelId).HasMaxLength(4000);
            entity.Property(e => e.ClientIp)
                .HasMaxLength(4000)
                .HasColumnName("ClientIP");
            entity.Property(e => e.Controller).HasMaxLength(100);
            entity.Property(e => e.DeviceId).HasMaxLength(4000);
            entity.Property(e => e.RequestTime).HasColumnType("datetime");
            entity.Property(e => e.StatusMessage).HasMaxLength(4000);
            entity.Property(e => e.UserEmail).HasMaxLength(4000);
        });

        modelBuilder.Entity<PaymentRecord>(entity =>
        {
            entity.Property(e => e.ClosingBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LogId).HasDefaultValue("");
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionType).HasDefaultValue("");
            entity.Property(e => e.TransferredAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ResponseBody>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Response__3214EC075177517D");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
