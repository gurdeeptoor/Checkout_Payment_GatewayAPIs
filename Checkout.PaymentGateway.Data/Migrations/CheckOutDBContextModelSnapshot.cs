﻿// <auto-generated />
using System;
using Checkout.PaymentGateway.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Checkout.PaymentGateway.Data.Migrations
{
    [DbContext(typeof(CheckOutDBContext))]
    partial class CheckOutDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Checkout.PaymentGateway.Data.CardDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Address2")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CardNum")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("nvarchar(19)")
                        .UseCollation("Latin1_General_BIN2");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Cvv")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("CVV")
                        .UseCollation("Latin1_General_BIN2");

                    b.Property<int>("ExpMonth")
                        .HasColumnType("int");

                    b.Property<int>("ExpYear")
                        .HasColumnType("int");

                    b.Property<string>("HolderName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .UseCollation("Latin1_General_BIN2");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CardDetail");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(1)
                        .HasColumnType("nchar(1)")
                        .IsFixedLength(true);

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.Merchant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<Guid>("MerchantRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Merchant");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.MerchantApikey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apikey")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("APIKey");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("MerchantId")
                        .HasColumnType("int")
                        .HasColumnName("MerchantID");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.ToTable("MerchantAPIKey");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TransactionID");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("AuthCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("BankRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CardDetailId")
                        .HasColumnType("int")
                        .HasColumnName("CardDetailID");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int")
                        .HasColumnName("CurrencyID");

                    b.Property<int>("MerchantId")
                        .HasColumnType("int")
                        .HasColumnName("MerchantID");

                    b.Property<string>("MerchantRef")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SourceType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TransactionStatusId")
                        .HasColumnType("int")
                        .HasColumnName("TransactionStatusID");

                    b.HasKey("TransactionId");

                    b.HasIndex("CardDetailId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("MerchantId");

                    b.HasIndex("TransactionStatusId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.TransactionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TransactionID");

                    b.Property<int>("TransactionStatusId")
                        .HasColumnType("int")
                        .HasColumnName("TransactionStatusID");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.HasIndex("TransactionStatusId");

                    b.ToTable("TransactionHistory");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.TransactionStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TransactionStatus");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.MerchantApikey", b =>
                {
                    b.HasOne("Checkout.PaymentGateway.Data.Merchant", "Merchant")
                        .WithMany("MerchantApikeys")
                        .HasForeignKey("MerchantId")
                        .HasConstraintName("FK_MerchantAPIKey_Merchant")
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.Transaction", b =>
                {
                    b.HasOne("Checkout.PaymentGateway.Data.CardDetail", "CardDetail")
                        .WithMany("Transactions")
                        .HasForeignKey("CardDetailId")
                        .HasConstraintName("FK_Transaction_CardDetail")
                        .IsRequired();

                    b.HasOne("Checkout.PaymentGateway.Data.Currency", "Currency")
                        .WithMany("Transactions")
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("FK_Transaction_Currency")
                        .IsRequired();

                    b.HasOne("Checkout.PaymentGateway.Data.Merchant", "Merchant")
                        .WithMany("Transactions")
                        .HasForeignKey("MerchantId")
                        .HasConstraintName("FK_Transaction_Merchant")
                        .IsRequired();

                    b.HasOne("Checkout.PaymentGateway.Data.TransactionStatus", "TransactionStatus")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionStatusId")
                        .HasConstraintName("FK_Transaction_TransactionStatus")
                        .IsRequired();

                    b.Navigation("CardDetail");

                    b.Navigation("Currency");

                    b.Navigation("Merchant");

                    b.Navigation("TransactionStatus");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.TransactionHistory", b =>
                {
                    b.HasOne("Checkout.PaymentGateway.Data.Transaction", "Transaction")
                        .WithMany("TransactionHistories")
                        .HasForeignKey("TransactionId")
                        .HasConstraintName("FK_TransactionHistory_Transaction")
                        .IsRequired();

                    b.HasOne("Checkout.PaymentGateway.Data.TransactionStatus", "TransactionStatus")
                        .WithMany("TransactionHistories")
                        .HasForeignKey("TransactionStatusId")
                        .HasConstraintName("FK_TransactionHistory_TransactionStatus")
                        .IsRequired();

                    b.Navigation("Transaction");

                    b.Navigation("TransactionStatus");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.CardDetail", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.Currency", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.Merchant", b =>
                {
                    b.Navigation("MerchantApikeys");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.Transaction", b =>
                {
                    b.Navigation("TransactionHistories");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Data.TransactionStatus", b =>
                {
                    b.Navigation("TransactionHistories");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
