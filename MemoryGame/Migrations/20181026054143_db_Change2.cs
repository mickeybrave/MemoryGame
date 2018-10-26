using Microsoft.EntityFrameworkCore.Migrations;

namespace MemoryGame.Migrations
{
    public partial class db_Change2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Config_UserId",
                table: "Config");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Config",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Config_UserId",
                table: "Config",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Config_UserId",
                table: "Config");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Config",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Config_UserId",
                table: "Config",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}
