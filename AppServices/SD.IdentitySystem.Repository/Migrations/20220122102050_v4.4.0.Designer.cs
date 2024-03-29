﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SD.IdentitySystem.Repository.Base;

namespace SD.IdentitySystem.Repository.Migrations
{
    [DbContext(typeof(DbSession))]
    [Migration("20220122102050_v4.4.0")]
    partial class v440
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Menu_Authority", b =>
                {
                    b.Property<Guid>("Authority_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Menu_Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Authority_Id", "Menu_Id");

                    b.HasIndex("Menu_Id");

                    b.ToTable("Menu_Authority");
                });

            modelBuilder.Entity("Role_Authority", b =>
                {
                    b.Property<Guid>("Authority_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Role_Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Authority_Id", "Role_Id");

                    b.HasIndex("Role_Id");

                    b.ToTable("Role_Authority");
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.Authority", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApplicationType")
                        .HasColumnType("int");

                    b.Property<string>("AssemblyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorityPath")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InfoSystemNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Namespace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SavedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex("AddedTime")
                        .IsClustered();

                    b.HasIndex("InfoSystemNo");

                    b.ToTable("Authority");
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.InfoSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdminLoginId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ApplicationType")
                        .HasColumnType("int");

                    b.Property<string>("CreatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Host")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Index")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("OperatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Port")
                        .HasColumnType("int");

                    b.Property<DateTime>("SavedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex("AddedTime")
                        .IsClustered();

                    b.ToTable("InfoSystem");
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.LoginRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LoginId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartitionIndex")
                        .HasColumnType("int");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RealName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SavedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex("AddedTime")
                        .IsClustered();

                    b.ToTable("LoginRecord");
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApplicationType")
                        .HasColumnType("int");

                    b.Property<string>("CreatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InfoSystemNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<bool>("IsRoot")
                        .HasColumnType("bit");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("OperatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentNode_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SavedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex("InfoSystemNo");

                    b.HasIndex("ParentNode_Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InfoSystemNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SavedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasIndex("AddedTime")
                        .IsClustered();

                    b.HasIndex("InfoSystemNo");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("OperatorAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PrivateKey")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("SavedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .IsClustered(false);

                    b.HasAlternateKey("Number");

                    b.HasAlternateKey("PrivateKey");

                    b.HasIndex("AddedTime")
                        .IsClustered();

                    b.ToTable("User");
                });

            modelBuilder.Entity("User_Role", b =>
                {
                    b.Property<Guid>("Role_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("User_Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Role_Id", "User_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("User_Role");
                });

            modelBuilder.Entity("Menu_Authority", b =>
                {
                    b.HasOne("SD.IdentitySystem.Domain.Entities.Authority", null)
                        .WithMany()
                        .HasForeignKey("Authority_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SD.IdentitySystem.Domain.Entities.Menu", null)
                        .WithMany()
                        .HasForeignKey("Menu_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Role_Authority", b =>
                {
                    b.HasOne("SD.IdentitySystem.Domain.Entities.Authority", null)
                        .WithMany()
                        .HasForeignKey("Authority_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SD.IdentitySystem.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.Authority", b =>
                {
                    b.HasOne("SD.IdentitySystem.Domain.Entities.InfoSystem", null)
                        .WithMany()
                        .HasForeignKey("InfoSystemNo")
                        .HasPrincipalKey("Number")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.Menu", b =>
                {
                    b.HasOne("SD.IdentitySystem.Domain.Entities.InfoSystem", null)
                        .WithMany()
                        .HasForeignKey("InfoSystemNo")
                        .HasPrincipalKey("Number")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SD.IdentitySystem.Domain.Entities.Menu", "ParentNode")
                        .WithMany("SubNodes")
                        .HasForeignKey("ParentNode_Id");

                    b.Navigation("ParentNode");
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.Role", b =>
                {
                    b.HasOne("SD.IdentitySystem.Domain.Entities.InfoSystem", null)
                        .WithMany()
                        .HasForeignKey("InfoSystemNo")
                        .HasPrincipalKey("Number")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("User_Role", b =>
                {
                    b.HasOne("SD.IdentitySystem.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SD.IdentitySystem.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SD.IdentitySystem.Domain.Entities.Menu", b =>
                {
                    b.Navigation("SubNodes");
                });
#pragma warning restore 612, 618
        }
    }
}
