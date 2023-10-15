using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LimisInsight.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompositeKeyForTeamUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_organization_53257_teams_users_v2",
                table: "organization_53257_teams_users_v2");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "organization_53257_teams_users_v2",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "team_name",
                table: "organization_53257_teams_users_v2",
                newName: "teamName");

            migrationBuilder.RenameColumn(
                name: "team_id",
                table: "organization_53257_teams_users_v2",
                newName: "teamId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "organization_53257_teams_users_v2",
                newName: "userId");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "organization_53257_teams_users_v2",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_organization_53257_teams_users_v2",
                table: "organization_53257_teams_users_v2",
                columns: new[] { "userId", "teamId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_organization_53257_teams_users_v2",
                table: "organization_53257_teams_users_v2");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "organization_53257_teams_users_v2",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "teamName",
                table: "organization_53257_teams_users_v2",
                newName: "team_name");

            migrationBuilder.RenameColumn(
                name: "teamId",
                table: "organization_53257_teams_users_v2",
                newName: "team_id");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "organization_53257_teams_users_v2",
                newName: "user_id");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "organization_53257_teams_users_v2",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_organization_53257_teams_users_v2",
                table: "organization_53257_teams_users_v2",
                column: "user_id");
        }
    }
}
