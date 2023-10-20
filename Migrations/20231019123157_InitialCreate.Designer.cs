﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LimisInsight.Migrations
{
    [DbContext(typeof(LocalDbContext))]
    [Migration("20231019123157_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LimisInsight.Models.OrganizationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("OrganizationUserState")
                        .HasColumnType("longtext")
                        .HasColumnName("organization_user_state");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext")
                        .HasColumnName("role_name");

                    b.HasKey("Id");

                    b.ToTable("organization_53257_organization_users");
                });

            modelBuilder.Entity("LimisInsight.Models.TeamUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("TeamId")
                        .HasColumnType("int")
                        .HasColumnName("team_id");

                    b.Property<string>("TeamName")
                        .HasColumnType("longtext")
                        .HasColumnName("team_name");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext")
                        .HasColumnName("user_name");

                    b.HasKey("UserId", "TeamId");

                    b.ToTable("organization_53257_teams_users");
                });

            modelBuilder.Entity("LimisInsight.Models.TimeEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int")
                        .HasColumnName("activity_id");

                    b.Property<string>("ActivityStatusName")
                        .HasColumnType("longtext")
                        .HasColumnName("activity_status_name");

                    b.Property<string>("ActivityTitle")
                        .HasColumnType("longtext")
                        .HasColumnName("activity_title");

                    b.Property<DateTime>("DateAt")
                        .HasColumnType("date")
                        .HasColumnName("date_at");

                    b.Property<float>("DurationHour")
                        .HasColumnType("float")
                        .HasColumnName("duration_hour");

                    b.Property<int>("MembersUserId")
                        .HasColumnType("int")
                        .HasColumnName("members_user_id");

                    b.Property<string>("MembersUserName")
                        .HasColumnType("longtext")
                        .HasColumnName("members_user_name");

                    b.Property<DateTime>("TimeEntryCreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("time_entry_created_at");

                    b.Property<DateTime>("TimeEntryUpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("time_entry_updated_at");

                    b.HasKey("Id");

                    b.ToTable("organization_53257_time_entries_v2");
                });
#pragma warning restore 612, 618
        }
    }
}