using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoDrive.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Token_UserId",
                table: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Token_UserId",
                table: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId",
                unique: true);
        }
    }
}
