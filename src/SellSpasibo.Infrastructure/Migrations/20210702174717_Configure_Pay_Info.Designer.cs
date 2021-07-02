﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SellSpasibo.Infrastructure;

namespace SellSpasibo.API.Infrastructure.Migrations
{
    [DbContext(typeof(SellSpasiboDbContext))]
    [Migration("20210702174717_Configure_Pay_Info")]
    partial class Configure_Pay_Info
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("SellSpasibo.Domain.Entities.BankEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("MemberId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("IX_BANK");

                    b.HasIndex("MemberId")
                        .IsUnique();

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("SellSpasibo.Domain.Entities.PayInfoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Number")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("IX_PAY_INFO");

                    b.HasIndex("Status");

                    b.ToTable("PayInfo");
                });

            modelBuilder.Entity("SellSpasibo.Domain.Entities.TinkoffAccountEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("IX_TINKOFF_ACCOUNT");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("TinkoffAccounts");
                });

            modelBuilder.Entity("SellSpasibo.Domain.Entities.TransactionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Hash")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("TransactionType")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("IX_TRANSACTION");

                    b.HasIndex("Time", "TransactionType")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("SellSpasibo.Domain.Entities.TransactionHistoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<double>("Commission")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("NumberFrom")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("NumberTo")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<Guid>("TransactionEntityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("TransactionTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("IX_TRANSACTION_HISTORY");

                    b.HasIndex("TransactionEntityId");

                    b.HasIndex("NumberFrom", "NumberTo");

                    b.ToTable("TransactionHistories");
                });

            modelBuilder.Entity("SellSpasibo.Domain.Entities.UserInfoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<bool>("PhoneIsValid")
                        .HasColumnType("boolean");

                    b.Property<string>("PhoneLinkId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("IX_USER_INFO");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("SellSpasibo.Domain.Entities.TransactionHistoryEntity", b =>
                {
                    b.HasOne("SellSpasibo.Domain.Entities.TransactionEntity", "TransactionEntity")
                        .WithMany()
                        .HasForeignKey("TransactionEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
