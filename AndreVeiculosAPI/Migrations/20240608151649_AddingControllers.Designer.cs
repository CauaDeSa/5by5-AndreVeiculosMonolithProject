﻿// <auto-generated />
using System;
using AndreVeiculosAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AndreVeiculosAPI.Migrations
{
    [DbContext(typeof(AndreVeiculosAPIContext))]
    [Migration("20240608151649_AddingControllers")]
    partial class AddingControllers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Models.Cars.Car", b =>
                {
                    b.Property<string>("Plate")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufactureYear")
                        .HasColumnType("int");

                    b.Property<int>("ModelYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sold")
                        .HasColumnType("bit");

                    b.HasKey("Plate");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("Models.Cars.CarOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CarPlate")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.Property<bool>("OperationStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CarPlate");

                    b.HasIndex("OperationId");

                    b.ToTable("CarOperation");
                });

            modelBuilder.Entity("Models.Cars.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("Models.People.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("PublicPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicPlaceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Models.People.Customer", b =>
                {
                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentPDF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Income")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Document");

                    b.HasIndex("AddressId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Models.People.Employee", b =>
                {
                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Comission")
                        .HasColumnType("float");

                    b.Property<double>("ComissionAmount")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FunctionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Document");

                    b.HasIndex("AddressId");

                    b.HasIndex("FunctionId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Models.People.JobTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JobTitle");
                });

            modelBuilder.Entity("Models.Profitable.Card", b =>
                {
                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DueDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardNumber");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Models.Profitable.Fetlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Fetlock");
                });

            modelBuilder.Entity("Models.Profitable.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FetlockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PixId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardNumber");

                    b.HasIndex("FetlockId");

                    b.HasIndex("PixId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Models.Profitable.Pix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PixKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Pix");
                });

            modelBuilder.Entity("Models.Profitable.PixType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PixType");
                });

            modelBuilder.Entity("Models.Profitable.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CarPlate")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarPlate");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("Models.Profitable.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CarPlate")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerDocument")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeDocument")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CarPlate");

                    b.HasIndex("CustomerDocument");

                    b.HasIndex("EmployeeDocument");

                    b.HasIndex("PaymentId");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("Models.Cars.CarOperation", b =>
                {
                    b.HasOne("Models.Cars.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarPlate");

                    b.HasOne("Models.Cars.Operation", "Operation")
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Operation");
                });

            modelBuilder.Entity("Models.People.Customer", b =>
                {
                    b.HasOne("Models.People.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Models.People.Employee", b =>
                {
                    b.HasOne("Models.People.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.People.JobTitle", "Function")
                        .WithMany()
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Function");
                });

            modelBuilder.Entity("Models.Profitable.Payment", b =>
                {
                    b.HasOne("Models.Profitable.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Profitable.Fetlock", "Fetlock")
                        .WithMany()
                        .HasForeignKey("FetlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Profitable.Pix", "Pix")
                        .WithMany()
                        .HasForeignKey("PixId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Fetlock");

                    b.Navigation("Pix");
                });

            modelBuilder.Entity("Models.Profitable.Pix", b =>
                {
                    b.HasOne("Models.Profitable.PixType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Models.Profitable.Purchase", b =>
                {
                    b.HasOne("Models.Cars.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarPlate");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Models.Profitable.Sale", b =>
                {
                    b.HasOne("Models.Cars.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarPlate");

                    b.HasOne("Models.People.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerDocument");

                    b.HasOne("Models.People.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeDocument");

                    b.HasOne("Models.Profitable.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("Payment");
                });
#pragma warning restore 612, 618
        }
    }
}
