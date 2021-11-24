﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment.Infra.DataBase.Context;

namespace Payment.Infra.DataBase.Migrations
{
    [DbContext(typeof(PaymentContext))]
    partial class PaymentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payment.Domain.Models.Antecipations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AnalysisEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AnalysisResult")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AnalysisStartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("GrantedAmount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RequestedAmount")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id")
                        .HasName("IX_Antecipations_Id");

                    b.ToTable("Antecipations");
                });

            modelBuilder.Entity("Payment.Domain.Models.Installments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime?>("AntecipationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("AntecipationValue")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime>("ExpectedPaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("IX_Installments_Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("Installments");
                });

            modelBuilder.Entity("Payment.Domain.Models.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int?>("AntecipationId")
                        .HasColumnType("int");

                    b.Property<int?>("AntecipationStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("BankConfirmation")
                        .HasColumnType("bit");

                    b.Property<decimal>("FlatRate")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("FourLastCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstallmentsNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime?>("NotApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .HasName("IX_Transactions_Id");

                    b.HasIndex("AntecipationId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Payment.Domain.Models.Installments", b =>
                {
                    b.HasOne("Payment.Domain.Models.Transactions", "Payment")
                        .WithMany("Installments")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Payment.Domain.Models.Transactions", b =>
                {
                    b.HasOne("Payment.Domain.Models.Antecipations", "Antecipation")
                        .WithMany("RequestedTransactions")
                        .HasForeignKey("AntecipationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
