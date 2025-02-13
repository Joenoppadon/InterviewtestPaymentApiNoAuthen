﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentsApi.Data;

#nullable disable

namespace PaymentsApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaymentsApi.Data.Model.Deposit", b =>
                {
                    b.Property<string>("DepositID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DepositDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("DepositFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepositStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserIdCard")
                        .HasColumnType("bigint");

                    b.HasKey("DepositID");

                    b.HasIndex("UserIdCard");

                    b.ToTable("Deposit");
                });

            modelBuilder.Entity("PaymentsApi.Data.Model.Transfer", b =>
                {
                    b.Property<string>("TransferID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("TransferAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransferDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("TransferFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransferFlagID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserIdCard")
                        .HasColumnType("bigint");

                    b.HasKey("TransferID");

                    b.HasIndex("UserIdCard");

                    b.ToTable("Transfer");
                });

            modelBuilder.Entity("PaymentsApi.Data.Model.User", b =>
                {
                    b.Property<long>("IdCard")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCard");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PaymentsApi.Data.Model.Withdraw", b =>
                {
                    b.Property<string>("WithdrawID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("UserIdCard")
                        .HasColumnType("bigint");

                    b.Property<decimal>("WithdrawAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("WithdrawDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("WithdrawFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WithdrawStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WithdrawID");

                    b.HasIndex("UserIdCard");

                    b.ToTable("Withdraw");
                });

            modelBuilder.Entity("PaymentsApi.Data.Model.Deposit", b =>
                {
                    b.HasOne("PaymentsApi.Data.Model.User", "User")
                        .WithMany("Deposit")
                        .HasForeignKey("UserIdCard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsApi.Data.Model.Transfer", b =>
                {
                    b.HasOne("PaymentsApi.Data.Model.User", "User")
                        .WithMany("Transfer")
                        .HasForeignKey("UserIdCard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsApi.Data.Model.Withdraw", b =>
                {
                    b.HasOne("PaymentsApi.Data.Model.User", "User")
                        .WithMany("Withdraw")
                        .HasForeignKey("UserIdCard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaymentsApi.Data.Model.User", b =>
                {
                    b.Navigation("Deposit");

                    b.Navigation("Transfer");

                    b.Navigation("Withdraw");
                });
#pragma warning restore 612, 618
        }
    }
}
