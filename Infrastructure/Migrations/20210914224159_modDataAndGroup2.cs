using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class modDataAndGroup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Groups_GroupId",
                table: "GroupDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupDetails_Teams_TeamId",
                table: "GroupDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupDetails",
                table: "GroupDetails");

            migrationBuilder.RenameTable(
                name: "GroupDetails",
                newName: "GroupTeams");

            migrationBuilder.RenameIndex(
                name: "IX_GroupDetails_TeamId",
                table: "GroupTeams",
                newName: "IX_GroupTeams_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupDetails_GroupId",
                table: "GroupTeams",
                newName: "IX_GroupTeams_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTeams",
                table: "GroupTeams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeams_Groups_GroupId",
                table: "GroupTeams",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeams_Teams_TeamId",
                table: "GroupTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeams_Groups_GroupId",
                table: "GroupTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeams_Teams_TeamId",
                table: "GroupTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTeams",
                table: "GroupTeams");

            migrationBuilder.RenameTable(
                name: "GroupTeams",
                newName: "GroupDetails");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTeams_TeamId",
                table: "GroupDetails",
                newName: "IX_GroupDetails_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTeams_GroupId",
                table: "GroupDetails",
                newName: "IX_GroupDetails_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupDetails",
                table: "GroupDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Groups_GroupId",
                table: "GroupDetails",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDetails_Teams_TeamId",
                table: "GroupDetails",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
