using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Nocturne.DAL;

namespace Nocturne.DAL.Migrations
{
    [DbContext(typeof(NocturneContext))]
    [Migration("20160122114955_Discount")]
    partial class Discount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Nocturne.DAL.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardType");

                    b.Property<int?>("ClientId");

                    b.Property<string>("Firstname")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Lastname")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("RegCard")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<ulong>("Uid");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Nocturne.DAL.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IdCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Nocturne.DAL.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountPercent");

                    b.Property<int>("DiscountTypeId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Nocturne.DAL.DiscountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Nocturne.DAL.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<byte[]>("DisplayImage");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Nocturne.DAL.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Nocturne.DAL.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("RegCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Nocturne.DAL.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");
                });

            modelBuilder.Entity("Nocturne.DAL.Card", b =>
                {
                    b.HasOne("Nocturne.DAL.Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Nocturne.DAL.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Nocturne.DAL.Discount", b =>
                {
                    b.HasOne("Nocturne.DAL.DiscountType")
                        .WithMany()
                        .HasForeignKey("DiscountTypeId");

                    b.HasOne("Nocturne.DAL.Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Nocturne.DAL.UserRole", b =>
                {
                    b.HasOne("Nocturne.DAL.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Nocturne.DAL.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
