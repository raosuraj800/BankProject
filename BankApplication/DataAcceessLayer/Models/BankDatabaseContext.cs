using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBLayer.Models
{
    public partial class BankDatabaseContext : DbContext
    {
        public BankDatabaseContext()
        {
        }

        public BankDatabaseContext(DbContextOptions<BankDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountInfo> AccountInfos { get; set; } = null!;
        public virtual DbSet<Bank> Banks { get; set; } = null!;
        public virtual DbSet<CurrencyChart> CurrencyCharts { get; set; } = null!;
        public virtual DbSet<TransactionInfo> TransactionInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=TL397;Database=BankDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountInfo>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("AccountInfo");

                entity.Property(e => e.AccountId).HasMaxLength(50);

                entity.Property(e => e.AccountEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountRole)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.BankId).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.AccountInfos)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountInfo_Bank");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.Property(e => e.BankId).HasMaxLength(50);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CurrencyChart>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CurrencyChart");

                entity.Property(e => e.CurrencyType)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("money");
            });

            modelBuilder.Entity<TransactionInfo>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("TransactionInfo");

                entity.Property(e => e.TransactionId).HasMaxLength(50);

                entity.Property(e => e.AccountId).HasMaxLength(50);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.BankId).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsRtgs).HasColumnName("IsRTGS");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TransactionInfos)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_TransactionInfo_AccountInfo");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.TransactionInfos)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_TransactionInfo_Bank");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == (EntityState)EntityState.Added || e.State == (EntityState)EntityState.Modified))

            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("CreatedDate").CurrentValue == null || entry.Property("CreatedDate").CurrentValue.ToString() == default(DateTime).ToString())
                        entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    //if (entry.Property("CreatedBy").CurrentValue == null)
                    //    entry.Property("CreatedBy").CurrentValue = "Suraj";
                }
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Property("UpdatedDate").CurrentValue == null)
                        entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                    //if (entry.Property("UpdatedBy").CurrentValue == null)
                    //    entry.Property("UpdatedBy").CurrentValue = "Suraj";
                }
            }
            //await ApplyAuditInformationAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {

            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == (EntityState)EntityState.Added || e.State == (EntityState)EntityState.Modified))

            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("CreatedDate").CurrentValue == null || entry.Property("CreatedDate").CurrentValue.ToString() == default(DateTime).ToString())
                        entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    //if (entry.Property("CreatedBy").CurrentValue == null)
                    //    entry.Property("CreatedBy").CurrentValue = "Suraj";
                }
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Property("UpdatedDate").CurrentValue == null)
                        entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                    //if (entry.Property("UpdatedBy").CurrentValue == null)
                    //    entry.Property("UpdatedBy").CurrentValue = "Suraj";
                }
            }
            return base.SaveChanges();
        }
    }
}
