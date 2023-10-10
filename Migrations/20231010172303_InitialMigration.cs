using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LimisInsight.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "organization_53257_teams_users_v2");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "organization_53257_teams_users_v2",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "TeamName",
                table: "organization_53257_teams_users_v2",
                newName: "team_name");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "organization_53257_teams_users_v2",
                newName: "team_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "organization_53257_teams_users_v2",
                newName: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_organization_53257_teams_users_v2",
                table: "organization_53257_teams_users_v2",
                column: "user_id");

            migrationBuilder.CreateTable(
                name: "organization_53257_time_entries_v2",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    duration_hour = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    members_user_id = table.Column<int>(type: "int", nullable: false),
                    members_user_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_53257_time_entries_v2", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_53257_time_entries_v2");

            migrationBuilder.DropPrimaryKey(
                name: "PK_organization_53257_teams_users_v2",
                table: "organization_53257_teams_users_v2");

            migrationBuilder.RenameTable(
                name: "organization_53257_teams_users_v2",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "team_name",
                table: "Users",
                newName: "TeamName");

            migrationBuilder.RenameColumn(
                name: "team_id",
                table: "Users",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<float>(type: "float", nullable: false),
                    DurationHour = table.Column<float>(type: "float", nullable: false),
                    MembersUserId = table.Column<int>(type: "int", nullable: false),
                    MembersUserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
