﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SessionFinalProject;

#nullable disable

namespace SessionFinalProject.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20230427024736_userAuth")]
    partial class userAuth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("SessionFinalProject.Authorization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authorization");
                });

            modelBuilder.Entity("SessionFinalProject.Sessions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpireOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SessionFinalProject.SignupCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpiresOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SignupCodes");
                });

            modelBuilder.Entity("SessionFinalProject.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SessionFinalProject.UserAuthorization", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorizationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "AuthorizationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("UserAuthorizations");
                });

            modelBuilder.Entity("SessionFinalProject.Sessions", b =>
                {
                    b.HasOne("SessionFinalProject.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SessionFinalProject.UserAuthorization", b =>
                {
                    b.HasOne("SessionFinalProject.Authorization", "Authorization")
                        .WithMany("UserAuthorizations")
                        .HasForeignKey("AuthorizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SessionFinalProject.User", "User")
                        .WithMany("UserAuthorizations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Authorization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SessionFinalProject.Authorization", b =>
                {
                    b.Navigation("UserAuthorizations");
                });

            modelBuilder.Entity("SessionFinalProject.User", b =>
                {
                    b.Navigation("UserAuthorizations");
                });
#pragma warning restore 612, 618
        }
    }
}
