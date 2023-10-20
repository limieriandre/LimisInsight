using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LimisInsight.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_53257_time_entries_v2",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    duration_hour = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    members_user_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activity_id = table.Column<int>(type: "int", nullable: false),
                    activity_title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    time_entry_created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    time_entry_updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    members_user_id = table.Column<int>(type: "int", nullable: false),
                    activity_status_name = table.Column<string>(type: "longtext", nullable: true)
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
        }
    }
}
