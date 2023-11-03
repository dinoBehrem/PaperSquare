using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperSquare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuditProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookSeriesReviews");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "RefreshToken",
                newName: "CreatedOnUtc");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "UserGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "UserGroups",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserGroups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "UserGenres",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "UserGenres",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserGenres",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserGenres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "User",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Quotes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Quotes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "QuoteCollections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "QuoteCollections",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuoteCollections",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Publishers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Publishers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Publishers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PublisherFollowers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "PublisherFollowers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PublisherFollowers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PublisherFollowers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "GroupMembershipsRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "GroupMembershipsRequests",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "GroupMembershipsRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GroupMembershipsRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "GroupMemberships",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "GroupMemberships",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "GroupMemberships",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GroupMemberships",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Genres",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Genres",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Genres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookShelves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookShelves",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookShelves",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookSeriesFollowers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookSeriesFollowers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "BookSeriesFollowers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookSeriesFollowers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookSeries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookSeries",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookSeries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Books",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookReviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookReviews",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "BookReviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookReviews",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookPublishers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookPublishers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookPublishers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookInShelves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookInShelves",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "BookInShelves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookInShelves",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookGenres",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookGenres",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "BookGenres",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookGenres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookAuthors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "BookAuthors",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "BookAuthors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookAuthors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Authors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Authors",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Authors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BookSeriesReview",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BookSeriesId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSeriesReview", x => new { x.UserId, x.BookSeriesId });
                    table.ForeignKey(
                        name: "FK_BookSeriesReview_BookSeries_BookSeriesId",
                        column: x => x.BookSeriesId,
                        principalTable: "BookSeries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookSeriesReview_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookSeriesReview_BookSeriesId",
                table: "BookSeriesReview",
                column: "BookSeriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookSeriesReview");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "UserGenres");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "UserGenres");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserGenres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserGenres");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "QuoteCollections");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "QuoteCollections");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuoteCollections");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PublisherFollowers");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "PublisherFollowers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PublisherFollowers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PublisherFollowers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "GroupMembershipsRequests");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "GroupMembershipsRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupMembershipsRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GroupMembershipsRequests");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "GroupMemberships");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "GroupMemberships");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupMemberships");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GroupMemberships");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookShelves");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookShelves");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookShelves");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookSeriesFollowers");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookSeriesFollowers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookSeriesFollowers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookSeriesFollowers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookSeries");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookSeries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookSeries");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookReviews");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookReviews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookReviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookReviews");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookPublishers");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookPublishers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookPublishers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookInShelves");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookInShelves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookInShelves");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookInShelves");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "RefreshToken",
                newName: "Created");

            migrationBuilder.CreateTable(
                name: "BookSeriesReviews",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BookSeriesId = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false)
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
        }
    }
}
