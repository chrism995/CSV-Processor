﻿// <auto-generated />
using System;
using EnergyCustomerAccountProcessorApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnergyCustomerAccountProcessorApi.Migrations
{
    [DbContext(typeof(EnergyContext))]
    [Migration("20241119142127_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("EnergyCustomerAccountProcessorApi.Models.MeterReading", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("MeterReadingDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MeterReadingValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserAccountID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserAccountID");

                    b.ToTable("MeterReadings");
                });

            modelBuilder.Entity("EnergyCustomerAccountProcessorApi.Models.User", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AccountID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EnergyCustomerAccountProcessorApi.Models.MeterReading", b =>
                {
                    b.HasOne("EnergyCustomerAccountProcessorApi.Models.User", "User")
                        .WithMany("MeterReadings")
                        .HasForeignKey("UserAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnergyCustomerAccountProcessorApi.Models.User", b =>
                {
                    b.Navigation("MeterReadings");
                });
#pragma warning restore 612, 618
        }
    }
}