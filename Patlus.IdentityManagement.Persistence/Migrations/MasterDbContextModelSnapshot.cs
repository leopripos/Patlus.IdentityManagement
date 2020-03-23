﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Patlus.IdentityManagement.Persistence.Contexts;

namespace Patlus.IdentityManagement.Persistence.Migrations
{
    [DbContext(typeof(MasterDbContext))]
    partial class MasterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.2.20120.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Patlus.IdentityManagement.UseCase.Entities.HostedAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("LastModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("HostedAccount");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90fdc79d-b97a-4b62-9c04-5b2f94df2026"),
                            Archived = false,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 3, 21, 16, 51, 23, 756, DateTimeKind.Unspecified).AddTicks(9782), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = new Guid("90fdc79d-b97a-4b62-9c04-5b2f94df2026"),
                            LastModifiedTime = new DateTimeOffset(new DateTime(2020, 3, 21, 16, 51, 23, 757, DateTimeKind.Unspecified).AddTicks(258), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "root",
                            Password = "YRktoPpuxUe8JlLYP5QC1qOuC7/JoUcJ4bjZmw6cpXU="
                        });
                });

            modelBuilder.Entity("Patlus.IdentityManagement.UseCase.Entities.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<Guid>("AuthKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("LastModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("PoolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AuthKey")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PoolId");

                    b.ToTable("Identity");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90fdc79d-b97a-4b62-9c04-5b2f94df2026"),
                            Active = true,
                            Archived = false,
                            AuthKey = new Guid("1774a09a-3821-4ff4-ba16-08770c9c797d"),
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 3, 21, 16, 51, 23, 747, DateTimeKind.Unspecified).AddTicks(5389), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = new Guid("90fdc79d-b97a-4b62-9c04-5b2f94df2026"),
                            LastModifiedTime = new DateTimeOffset(new DateTime(2020, 3, 21, 16, 51, 23, 747, DateTimeKind.Unspecified).AddTicks(5779), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "root",
                            PoolId = new Guid("c73d72b1-326d-4213-ab11-ba47d83b9ccf")
                        });
                });

            modelBuilder.Entity("Patlus.IdentityManagement.UseCase.Entities.Pool", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("LastModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pool");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c73d72b1-326d-4213-ab11-ba47d83b9ccf"),
                            Active = true,
                            Archived = false,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 3, 21, 16, 51, 23, 746, DateTimeKind.Unspecified).AddTicks(1228), new TimeSpan(0, 0, 0, 0, 0)),
                            CreatorId = new Guid("90fdc79d-b97a-4b62-9c04-5b2f94df2026"),
                            Description = "Default identity pool for system administrator.",
                            LastModifiedTime = new DateTimeOffset(new DateTime(2020, 3, 21, 16, 51, 23, 746, DateTimeKind.Unspecified).AddTicks(1694), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Root Administrator"
                        });
                });

            modelBuilder.Entity("Patlus.IdentityManagement.UseCase.Entities.Identity", b =>
                {
                    b.HasOne("Patlus.IdentityManagement.UseCase.Entities.HostedAccount", "HostedAccount")
                        .WithOne("Identity")
                        .HasForeignKey("Patlus.IdentityManagement.UseCase.Entities.Identity", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Patlus.IdentityManagement.UseCase.Entities.Pool", "Pool")
                        .WithOne()
                        .HasForeignKey("Patlus.IdentityManagement.UseCase.Entities.Identity", "PoolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}