﻿// <auto-generated />
using System;
using LimisInsight.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LimisInsight.Migrations
{
    [DbContext(typeof(LimisInsightContext))]
    [Migration("20231011235207_UpdateCompositeKeyForTeamUsers")]
    partial class UpdateCompositeKeyForTeamUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LimisInsight.Models.TimeEntries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_at");

                    b.Property<float>("Duration")
                        .HasColumnType("float")
                        .HasColumnName("duration");

                    b.Property<float>("DurationHour")
                        .HasColumnType("float")
                        .HasColumnName("duration_hour");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("members_user_id");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext")
                        .HasColumnName("members_user_name");

                    b.HasKey("Id");

                    b.ToTable("organization_53257_time_entries_v2");
                });

            modelBuilder.Entity("LimisInsight.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<int>("teamId")
                        .HasColumnType("int");

                    b.Property<string>("teamName")
                        .HasColumnType("longtext");

                    b.Property<string>("userName")
                        .HasColumnType("longtext");

                    b.HasKey("userId", "teamId");

                    b.ToTable("organization_53257_teams_users_v2");
                });
#pragma warning restore 612, 618
        }
    }
}
