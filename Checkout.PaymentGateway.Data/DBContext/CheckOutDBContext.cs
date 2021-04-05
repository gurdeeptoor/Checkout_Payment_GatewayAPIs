using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class CheckOutDBContext : DbContext
    {
        public CheckOutDBContext()
        {
        }

        public CheckOutDBContext(DbContextOptions<CheckOutDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<CardDetail> CardDetails { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<MerchantApikey> MerchantApikeys { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CardDetail>(entity =>
            {
                entity.ToTable("CardDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.CardNum)
                    .IsRequired()
                    .HasMaxLength(19)
                    .UseCollation("Latin1_General_BIN2");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CountryCode).HasMaxLength(3);

                entity.Property(e => e.Cvv)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("CVV")
                    .UseCollation("Latin1_General_BIN2");

                entity.Property(e => e.HolderName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .UseCollation("Latin1_General_BIN2");

                entity.Property(e => e.State).HasMaxLength(50);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Symbol)
                    .HasMaxLength(1)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.ToTable("Merchant");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<MerchantApikey>(entity =>
            {
                entity.ToTable("MerchantAPIKey");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apikey)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("APIKey");

                entity.Property(e => e.MerchantId).HasColumnName("MerchantID");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.MerchantApikeys)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchantAPIKey_Merchant");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("TransactionID");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.AuthCode).HasMaxLength(20);

                entity.Property(e => e.CardDetailId).HasColumnName("CardDetailID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.MerchantId).HasColumnName("MerchantID");

                entity.Property(e => e.MerchantRef).HasMaxLength(20);

                entity.Property(e => e.SourceType).HasMaxLength(50);

                entity.Property(e => e.TransactionStatusId).HasColumnName("TransactionStatusID");

                entity.HasOne(d => d.CardDetail)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CardDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_CardDetail");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Currency");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Merchant");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_TransactionStatus");
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.ToTable("TransactionHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.TransactionStatusId).HasColumnName("TransactionStatusID");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionHistory_Transaction");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.TransactionHistories)
                    .HasForeignKey(d => d.TransactionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionHistory_TransactionStatus");
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.ToTable("TransactionStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
