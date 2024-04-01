using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoDrive.Migrations
{
    /// <inheritdoc />
    public partial class addDBIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_UserId",
                table: "Image",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drive_UserId",
                table: "Drive",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drive_User_UserId",
                table: "Drive",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_User_UserId",
                table: "Image",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_UserId",
                table: "Token",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drive_User_UserId",
                table: "Drive");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_User_UserId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Token_User_UserId",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Token_UserId",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Image_UserId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Drive_UserId",
                table: "Drive");
        }
    }
}
