﻿// <auto-generated />
using System;
using DogT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DogT.Migrations
{
    [DbContext(typeof(DogTContext))]
    [Migration("20210221200216_ProposalModel")]
    partial class ProposalModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DogT.Models.Dog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DogHandlerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DogHandlerId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Dogs");
                });

            modelBuilder.Entity("DogT.Models.DogHandler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("DogHandlers");
                });

            modelBuilder.Entity("DogT.Models.Proposal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Context")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("DogHandlerId")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DogHandlerId");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("DogT.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Адміністратор"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Кінолог"
                        });
                });

            modelBuilder.Entity("DogT.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("DogT.Models.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Context")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DogHandlerId")
                        .HasColumnType("int");

                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<string>("Estimate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DogHandlerId");

                    b.HasIndex("DogId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("DogT.Models.TrainingComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CommentContext")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DogHandlerId")
                        .HasColumnType("int");

                    b.Property<int>("TrainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DogHandlerId");

                    b.HasIndex("TrainingId");

                    b.ToTable("TrainingComments");
                });

            modelBuilder.Entity("DogT.Models.TrainingTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Context")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<int>("DogHandlerId")
                        .HasColumnType("int");

                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DogHandlerId");

                    b.HasIndex("DogId");

                    b.ToTable("TrainingTasks");
                });

            modelBuilder.Entity("DogT.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@mail",
                            Password = "admin",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("DogT.Models.Dog", b =>
                {
                    b.HasOne("DogT.Models.DogHandler", "DogHandler")
                        .WithMany("Dogs")
                        .HasForeignKey("DogHandlerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DogT.Models.Specialization", "Specialization")
                        .WithMany("Dogs")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DogHandler");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("DogT.Models.DogHandler", b =>
                {
                    b.HasOne("DogT.Models.User", "User")
                        .WithOne("DogHandler")
                        .HasForeignKey("DogT.Models.DogHandler", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DogT.Models.Proposal", b =>
                {
                    b.HasOne("DogT.Models.DogHandler", "DogHandler")
                        .WithMany("Proposals")
                        .HasForeignKey("DogHandlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DogHandler");
                });

            modelBuilder.Entity("DogT.Models.Training", b =>
                {
                    b.HasOne("DogT.Models.DogHandler", "DogHandler")
                        .WithMany("Trainings")
                        .HasForeignKey("DogHandlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DogT.Models.Dog", "Dog")
                        .WithMany("Trainings")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DogT.Models.Specialization", "Specialization")
                        .WithMany("Trainings")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("DogHandler");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("DogT.Models.TrainingComment", b =>
                {
                    b.HasOne("DogT.Models.DogHandler", "DogHandler")
                        .WithMany("TrainingComments")
                        .HasForeignKey("DogHandlerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DogT.Models.Training", "Training")
                        .WithMany("Comments")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DogHandler");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("DogT.Models.TrainingTask", b =>
                {
                    b.HasOne("DogT.Models.DogHandler", "DogHandler")
                        .WithMany("TrainingTasks")
                        .HasForeignKey("DogHandlerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogT.Models.Dog", "Dog")
                        .WithMany("TrainingTasks")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("DogHandler");
                });

            modelBuilder.Entity("DogT.Models.User", b =>
                {
                    b.HasOne("DogT.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DogT.Models.Dog", b =>
                {
                    b.Navigation("Trainings");

                    b.Navigation("TrainingTasks");
                });

            modelBuilder.Entity("DogT.Models.DogHandler", b =>
                {
                    b.Navigation("Dogs");

                    b.Navigation("Proposals");

                    b.Navigation("TrainingComments");

                    b.Navigation("Trainings");

                    b.Navigation("TrainingTasks");
                });

            modelBuilder.Entity("DogT.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DogT.Models.Specialization", b =>
                {
                    b.Navigation("Dogs");

                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("DogT.Models.Training", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DogT.Models.User", b =>
                {
                    b.Navigation("DogHandler");
                });
#pragma warning restore 612, 618
        }
    }
}
