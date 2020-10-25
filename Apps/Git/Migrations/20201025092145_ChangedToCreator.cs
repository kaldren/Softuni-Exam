using Microsoft.EntityFrameworkCore.Migrations;

namespace Git.Migrations
{
    public partial class ChangedToCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Users_userId",
                table: "Commits");

            migrationBuilder.DropIndex(
                name: "IX_Commits_userId",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Commits");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Commits",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commits_CreatorId",
                table: "Commits",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Users_CreatorId",
                table: "Commits",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Users_CreatorId",
                table: "Commits");

            migrationBuilder.DropIndex(
                name: "IX_Commits_CreatorId",
                table: "Commits");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Commits",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Commits",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commits_userId",
                table: "Commits",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Users_userId",
                table: "Commits",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
