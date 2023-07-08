using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperSquare.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookShelf_BookInShelf_TablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookShelves",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShelves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookShelves_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookInShelves",
                columns: table => new
                {
                    BookShelfId = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<string>(type: "text", nullable: false),
                    Progress = table.Column<decimal>(type: "numeric", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInShelves", x => new { x.BookId, x.BookShelfId });
                    table.ForeignKey(
                        name: "FK_BookInShelves_BookShelves_BookShelfId",
                        column: x => x.BookShelfId,
                        principalTable: "BookShelves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookInShelves_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookInShelves_BookShelfId",
                table: "BookInShelves",
                column: "BookShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_BookShelves_UserId",
                table: "BookShelves",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookInShelves");

            migrationBuilder.DropTable(
                name: "BookShelves");
        }
    }
}
