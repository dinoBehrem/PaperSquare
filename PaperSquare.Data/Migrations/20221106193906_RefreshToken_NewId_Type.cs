using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperSquare.Data.Migrations
{
    public partial class RefreshToken_NewId_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RefreshToken");

            migrationBuilder.AddColumn<string>(
                name: "NewId",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "NewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "NewId",
                table: "RefreshToken");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RefreshToken",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");
        }
    }
}
