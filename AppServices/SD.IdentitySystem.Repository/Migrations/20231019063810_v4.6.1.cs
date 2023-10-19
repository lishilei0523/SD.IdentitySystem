using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SD.IdentitySystem.Repository.Migrations
{
    public partial class v461 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_PrivateKey",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_PrivateKey",
                table: "User",
                column: "PrivateKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_PrivateKey",
                table: "User");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_PrivateKey",
                table: "User",
                column: "PrivateKey");
        }
    }
}
