﻿// <auto-generated />
using System;
using LimisInsight.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LimisInsight.Migrations
{
    [DbContext(typeof(LimisInsightContext))]
    partial class LimisInsightContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LimisInsight.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
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

                    b.HasKey("UserId");

                    b.ToTable("organization_53257_teams_users_v2");
                });

            modelBuilder.Entity("TimeEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_at");

                    b.Property<int>("Duration")
                        .HasColumnType("int")
                        .HasColumnName("duration");

                    b.Property<decimal>("DurationHour")
                        .HasColumnType("decimal(65,30)")
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
#pragma warning restore 612, 618
        }
    }
}
