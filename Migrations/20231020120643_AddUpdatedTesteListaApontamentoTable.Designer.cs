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
    [Migration("20231020120643_AddUpdatedTesteListaApontamentoTable")]
    partial class AddUpdatedTesteListaApontamentoTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LimisInsight.Models.DataJustificada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("data");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("justificativa");

                    b.Property<int>("MembersUserId")
                        .HasColumnType("int")
                        .HasColumnName("members_user_id");

                    b.HasKey("Id");

                    b.HasIndex("MembersUserId");

                    b.ToTable("data_justificada");
                });

            modelBuilder.Entity("LimisInsight.Models.ListaApontamento", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<DateTime>("Data")
                        .HasColumnType("date")
                        .HasColumnName("data");

                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext")
                        .HasColumnName("nome");

                    b.Property<float>("Soma")
                        .HasColumnType("float")
                        .HasColumnName("soma");

                    b.Property<string>("Status")
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.Property<string>("Time")
                        .HasColumnType("longtext")
                        .HasColumnName("time");

                    b.HasKey("IdUser", "Data");

                    b.ToTable("lista_apontamentos");
                });

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

            modelBuilder.Entity("LimisInsight.Models.ParametrosConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("ParametroNome")
                        .HasColumnType("longtext");

                    b.Property<string>("ParametroTipo")
                        .HasColumnType("longtext");

                    b.Property<string>("ParametroValor")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ParametrosConfigs");
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

            modelBuilder.Entity("LimisInsight.Models.DataJustificada", b =>
                {
                    b.HasOne("LimisInsight.Models.OrganizationUser", "OrganizationUser")
                        .WithMany("DatasJustificadas")
                        .HasForeignKey("MembersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationUser");
                });

            modelBuilder.Entity("LimisInsight.Models.OrganizationUser", b =>
                {
                    b.Navigation("DatasJustificadas");
                });
#pragma warning restore 612, 618
        }
    }
}
