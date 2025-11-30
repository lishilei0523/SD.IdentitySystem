using Microsoft.EntityFrameworkCore.Migrations;

namespace SD.IdentitySystem.Repository.Migrations
{
    public partial class v450 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssemblyName",
                table: "Authority");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Authority");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Authority");

            migrationBuilder.DropColumn(
                name: "MethodName",
                table: "Authority");

            migrationBuilder.DropColumn(
                name: "Namespace",
                table: "Authority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyName",
                table: "Authority",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Authority",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Authority",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MethodName",
                table: "Authority",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Namespace",
                table: "Authority",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
