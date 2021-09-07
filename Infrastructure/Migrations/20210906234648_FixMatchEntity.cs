using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FixMatchEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Teams_TeamId",
                table: "Matchs");

            migrationBuilder.DropIndex(
                name: "IX_Matchs_TeamId",
                table: "Matchs");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Matchs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Matchs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_TeamId",
                table: "Matchs",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Teams_TeamId",
                table: "Matchs",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
