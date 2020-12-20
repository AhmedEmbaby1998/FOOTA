﻿// <auto-generated />
using System;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KhaledMohsen.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20201106180130_firs2")]
    partial class firs2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KhaledMohsen.Models.Consumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Password")
                        .HasColumnType("int");

                    b.Property<int>("UpdateCount")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Consumers");
                });

            modelBuilder.Entity("KhaledMohsen.Models.FileData", b =>
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

            modelBuilder.Entity("KhaledMohsen.Models.Line", b =>
                {
                    b.Property<int>("FileDataId")
                        .HasColumnType("int");

                    b.Property<int>("LineNum")
                        .HasColumnType("int");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FileDataId", "LineNum");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("KhaledMohsen.Models.Reading", b =>
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

            modelBuilder.Entity("KhaledMohsen.Models.Line", b =>
                {
                    b.HasOne("KhaledMohsen.Models.FileData", "FileData")
                        .WithMany("Lines")
                        .HasForeignKey("FileDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KhaledMohsen.Models.Reading", b =>
                {
                    b.HasOne("KhaledMohsen.Models.Consumer", "Consumer")
                        .WithMany("Readings")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KhaledMohsen.Models.FileData", "FileData")
                        .WithMany("Readings")
                        .HasForeignKey("FileDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
