﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoteIt.Persistence.EF;

#nullable disable

namespace NoteIt.Persistence.EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220402120737_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NoteIt.Domain.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("StorageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("NoteIt.Domain.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("Location")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset?>("ReminderDate")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<Guid>("StorageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("NoteIt.Domain.Entities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<bool>("IsImportant")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("StorageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("NoteIt.Domain.Entities.Storage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("NoteIt.Domain.Entities.Contact", b =>
                {
                    b.HasOne("NoteIt.Domain.Entities.Storage", "Storage")
                        .WithMany("Contacts")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("NoteIt.Domain.Entities.Event", b =>
                {
                    b.HasOne("NoteIt.Domain.Entities.Storage", "Storage")
                        .WithMany("Events")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("NoteIt.Domain.Entities.Note", b =>
                {
                    b.HasOne("NoteIt.Domain.Entities.Storage", "Storage")
                        .WithMany("Notes")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("NoteIt.Domain.Entities.Storage", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Events");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}