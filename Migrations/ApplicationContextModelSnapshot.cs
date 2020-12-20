﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication6.Models;

namespace KhaledMohsen.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication6.Models.Consumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActiveTill")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpdateCount")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Consumers");
                });

            modelBuilder.Entity("WebApplication6.Models.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LineCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UploadingDate")
                        .IsUnique();

                    b.ToTable("FileDatas");
                });

            modelBuilder.Entity("WebApplication6.Models.Line", b =>
                {
                    b.Property<int>("FileDataId")
                        .HasColumnType("int");

                    b.Property<int>("LineNum")
                        .HasColumnType("int");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("FileDataId", "LineNum");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("WebApplication6.Models.Reading", b =>
                {
                    b.Property<int>("FileDataId")
                        .HasColumnType("int");

                    b.Property<int>("ConsumerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReadingDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("FileDataId", "ConsumerId");

                    b.HasIndex("ConsumerId");

                    b.ToTable("Readings");
                });

            modelBuilder.Entity("WebApplication6.Models.Line", b =>
                {
                    b.HasOne("WebApplication6.Models.FileData", "FileData")
                        .WithMany("Lines")
                        .HasForeignKey("FileDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication6.Models.Reading", b =>
                {
                    b.HasOne("WebApplication6.Models.Consumer", "Consumer")
                        .WithMany("Readings")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication6.Models.FileData", "FileData")
                        .WithMany("Readings")
                        .HasForeignKey("FileDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
