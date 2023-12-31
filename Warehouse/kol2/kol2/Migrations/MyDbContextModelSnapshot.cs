﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarehousesAPI.Models;

#nullable disable

namespace WarehousesAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("kol2.Models.Object", b =>
                {
                    b.Property<int>("IdObject")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdObject"));

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int>("IdObjectType")
                        .HasColumnType("int");

                    b.Property<int>("IdWarehouse")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("IdObject");

                    b.HasIndex("IdObjectType");

                    b.HasIndex("IdWarehouse");

                    b.ToTable("Objects");

                    b.HasData(
                        new
                        {
                            IdObject = 1,
                            Height = 10.0,
                            IdObjectType = 1,
                            IdWarehouse = 1,
                            Width = 20.0
                        },
                        new
                        {
                            IdObject = 2,
                            Height = 15.0,
                            IdObjectType = 2,
                            IdWarehouse = 1,
                            Width = 25.0
                        });
                });

            modelBuilder.Entity("kol2.Models.ObjectOwner", b =>
                {
                    b.Property<int>("IdObject")
                        .HasColumnType("int");

                    b.Property<int>("IdOwner")
                        .HasColumnType("int");

                    b.HasKey("IdObject", "IdOwner");

                    b.HasIndex("IdOwner");

                    b.ToTable("ObjectOwners");

                    b.HasData(
                        new
                        {
                            IdObject = 1,
                            IdOwner = 1
                        },
                        new
                        {
                            IdObject = 2,
                            IdOwner = 2
                        });
                });

            modelBuilder.Entity("kol2.Models.ObjectType", b =>
                {
                    b.Property<int>("IdObjectType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdObjectType"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdObjectType");

                    b.ToTable("ObjectTypes");

                    b.HasData(
                        new
                        {
                            IdObjectType = 1,
                            Name = "Obiekt A"
                        },
                        new
                        {
                            IdObjectType = 2,
                            Name = "Obiekt B"
                        });
                });

            modelBuilder.Entity("kol2.Models.Owner", b =>
                {
                    b.Property<int>("IdOwner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOwner"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("IdOwner");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            IdOwner = 1,
                            FirstName = "Jan",
                            LastName = "Kowalski",
                            PhoneNumber = "123456789"
                        },
                        new
                        {
                            IdOwner = 2,
                            FirstName = "Anna",
                            LastName = "Nowak",
                            PhoneNumber = "987654321"
                        });
                });

            modelBuilder.Entity("kol2.Models.Warehouse", b =>
                {
                    b.Property<int>("IdWarehouse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdWarehouse"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdWarehouse");

                    b.ToTable("Warehouses");

                    b.HasData(
                        new
                        {
                            IdWarehouse = 1,
                            Name = "Magazyn A"
                        },
                        new
                        {
                            IdWarehouse = 2,
                            Name = "Magazyn B"
                        });
                });

            modelBuilder.Entity("kol2.Models.Object", b =>
                {
                    b.HasOne("kol2.Models.ObjectType", "ObjectType")
                        .WithMany("Objects")
                        .HasForeignKey("IdObjectType")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("kol2.Models.Warehouse", "Warehouse")
                        .WithMany("Objects")
                        .HasForeignKey("IdWarehouse")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("ObjectType");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("kol2.Models.ObjectOwner", b =>
                {
                    b.HasOne("kol2.Models.Object", "Object")
                        .WithMany("ObjectOwners")
                        .HasForeignKey("IdObject")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("kol2.Models.Owner", "Owner")
                        .WithMany("ObjectOwners")
                        .HasForeignKey("IdOwner")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Object");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("kol2.Models.Object", b =>
                {
                    b.Navigation("ObjectOwners");
                });

            modelBuilder.Entity("kol2.Models.ObjectType", b =>
                {
                    b.Navigation("Objects");
                });

            modelBuilder.Entity("kol2.Models.Owner", b =>
                {
                    b.Navigation("ObjectOwners");
                });

            modelBuilder.Entity("kol2.Models.Warehouse", b =>
                {
                    b.Navigation("Objects");
                });
#pragma warning restore 612, 618
        }
    }
}
