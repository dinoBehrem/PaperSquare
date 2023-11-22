using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperSquare.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserPersonalInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSeriesFollower_BookSeries_BookSeriesId",
                table: "BookSeriesFollower");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSeriesFollower_Users_FollowerId",
                table: "BookSeriesFollower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookSeriesFollower",
                table: "BookSeriesFollower");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "BookSeriesFollowers",
                newName: "BookSeriesFollowers");

            migrationBuilder.RenameIndex(
                name: "IX_BookSeriesFollower_BookSeriesId",
                table: "BookSeriesFollowers",
                newName: "IX_BookSeriesFollowers_BookSeriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookSeriesFollowers",
                table: "BookSeriesFollowers",
                columns: new[] { "FollowerId", "BookSeriesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookSeriesFollowers_BookSeries_BookSeriesId",
                table: "BookSeriesFollowers",
                column: "BookSeriesId",
                principalTable: "BookSeries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSeriesFollowers_Users_FollowerId",
                table: "BookSeriesFollowers",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSeriesFollowers_BookSeries_BookSeriesId",
                table: "BookSeriesFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSeriesFollowers_Users_FollowerId",
                table: "BookSeriesFollowers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookSeriesFollowers",
                table: "BookSeriesFollowers");

            migrationBuilder.RenameTable(
                name: "BookSeriesFollowers",
                newName: "BookSeriesFollower");

            migrationBuilder.RenameIndex(
                name: "IX_BookSeriesFollowers_BookSeriesId",
                table: "BookSeriesFollower",
                newName: "IX_BookSeriesFollower_BookSeriesId");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookSeriesFollower",
                table: "BookSeriesFollower",
                columns: new[] { "FollowerId", "BookSeriesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookSeriesFollower_BookSeries_BookSeriesId",
                table: "BookSeriesFollower",
                column: "BookSeriesId",
                principalTable: "BookSeries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSeriesFollower_Users_FollowerId",
                table: "BookSeriesFollower",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
