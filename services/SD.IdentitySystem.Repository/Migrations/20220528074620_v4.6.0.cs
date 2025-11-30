using Microsoft.EntityFrameworkCore.Migrations;

namespace SD.IdentitySystem.Repository.Migrations
{
    public partial class v460 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "LoginRecord",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "LoginRecord");
        }
    }
}
