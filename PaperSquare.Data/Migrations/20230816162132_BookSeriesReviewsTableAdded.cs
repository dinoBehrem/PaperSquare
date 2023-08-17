using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperSquare.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookSeriesReviewsTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReviews_User_UserId",
                table: "BookReviews");

            migrationBuilder.CreateTable(
                name: "BookSeriesReviews",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BookSeriesId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSeriesReviews", x => new { x.UserId, x.BookSeriesId });
                    table.ForeignKey(
                        name: "FK_BookSeriesReviews_BookSeries_BookSeriesId",
                        column: x => x.BookSeriesId,
                        principalTable: "BookSeries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookSeriesReviews_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookSeriesReviews_BookSeriesId",
                table: "BookSeriesReviews",
                column: "BookSeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReviews_User_UserId",
                table: "BookReviews",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReviews_User_UserId",
                table: "BookReviews");

            migrationBuilder.DropTable(
                name: "BookSeriesReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReviews_User_UserId",
                table: "BookReviews",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
