using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaperSquare.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9709d04b-71b0-4874-9ca1-bc4e70e64298", "df7e8015-80c9-4428-8372-74ce0e6e16e8", "Editor", "EDITOR" },
                    { "9813ad52-51e2-431b-8116-2e77fdf79b33", "c413c738-8402-49ea-a52e-0db396a612a5", "Admin", "ADMIN" },
                    { "c2444b71-c794-4e42-8ec9-25a65ff3abeb", "2cb2edf4-0574-4285-b7f6-c827d2906d6b", "Guest", "GUEST" },
                    { "f6b5c6c9-3f5c-4a91-bc4d-a91825243fba", "cbe1b56d-c28d-4023-95ff-ed835870fbc4", "RegisteredUser", "REGISTEREDUSER" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "Firstname", "IsDeleted", "LastUpdated", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0eee7038-6751-45d5-a434-3223ac0546f1", 0, new DateTime(1988, 6, 6, 23, 18, 46, 180, DateTimeKind.Unspecified).AddTicks(3380), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2016, 11, 24, 23, 6, 55, 512, DateTimeKind.Unspecified).AddTicks(4438), "Jaron.Abshire80@gmail.com", true, "Jaron", false, null, "Abshire", false, null, "JARON.ABSHIRE80@GMAIL.COM", "JARON_ABSHIRE", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Jaron_Abshire" },
                    { "1a7726f8-de89-465e-a761-6f7597a8d6bd", 0, new DateTime(1991, 7, 6, 19, 11, 8, 899, DateTimeKind.Unspecified).AddTicks(6738), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2017, 4, 22, 23, 41, 11, 275, DateTimeKind.Unspecified).AddTicks(1470), "Matt_Kemmer@yahoo.com", false, "Matt", false, null, "Kemmer", false, null, "MATT_KEMMER@YAHOO.COM", "MATT_KEMMER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Matt_Kemmer" },
                    { "1f9ff0c1-72d5-45b7-9150-b9bf5e4f7c12", 0, new DateTime(1970, 3, 21, 13, 31, 27, 233, DateTimeKind.Unspecified).AddTicks(3292), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2018, 7, 26, 22, 49, 17, 335, DateTimeKind.Unspecified).AddTicks(578), "Chanel_Carroll41@hotmail.com", true, "Chanel", true, null, "Carroll", false, null, "CHANEL_CARROLL41@HOTMAIL.COM", "CHANEL_CARROLL", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Chanel_Carroll" },
                    { "3bf4e986-0502-49f8-84d9-d841697995e5", 0, new DateTime(1967, 10, 25, 15, 53, 18, 0, DateTimeKind.Unspecified).AddTicks(8330), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2016, 11, 16, 11, 6, 51, 130, DateTimeKind.Unspecified).AddTicks(9206), "Fritz30@hotmail.com", true, "Fritz", true, null, "Kessler", false, null, "FRITZ30@HOTMAIL.COM", "FRITZ_KESSLER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Fritz_Kessler" },
                    { "646cb87b-3078-40b7-9910-6f042aea6390", 0, new DateTime(1973, 7, 11, 20, 51, 2, 496, DateTimeKind.Unspecified).AddTicks(7749), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2017, 7, 30, 1, 13, 56, 633, DateTimeKind.Unspecified).AddTicks(6500), "Felicity.Durgan84@hotmail.com", true, "Felicity", true, null, "Durgan", false, null, "FELICITY.DURGAN84@HOTMAIL.COM", "FELICITY_DURGAN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Felicity_Durgan" },
                    { "76c92c3a-11df-4b2f-8e6a-0b3c88844b16", 0, new DateTime(1964, 2, 7, 1, 17, 9, 207, DateTimeKind.Unspecified).AddTicks(3460), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2010, 3, 20, 4, 46, 43, 42, DateTimeKind.Unspecified).AddTicks(8824), "Angeline2@gmail.com", false, "Angeline", false, null, "Wiegand", false, null, "ANGELINE2@GMAIL.COM", "ANGELINE_WIEGAND", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Angeline_Wiegand" },
                    { "7a085da8-d9da-48e1-ad98-c04afd1c52af", 0, new DateTime(2001, 8, 9, 11, 49, 44, 655, DateTimeKind.Unspecified).AddTicks(7244), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2014, 10, 10, 16, 10, 24, 309, DateTimeKind.Unspecified).AddTicks(1198), "Reece11@hotmail.com", true, "Reece", true, null, "Effertz", false, null, "REECE11@HOTMAIL.COM", "REECE_EFFERTZ", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Reece_Effertz" },
                    { "7fab3eab-31f1-46f4-9726-644c9b44f992", 0, new DateTime(1982, 3, 7, 18, 29, 55, 414, DateTimeKind.Unspecified).AddTicks(1450), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2017, 8, 6, 4, 9, 52, 581, DateTimeKind.Unspecified).AddTicks(7388), "Mallory.Nolan37@gmail.com", true, "Mallory", true, null, "Nolan", false, null, "MALLORY.NOLAN37@GMAIL.COM", "MALLORY_NOLAN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Mallory_Nolan" },
                    { "9536000e-3ddc-452f-8d28-fb784442fdc8", 0, new DateTime(1972, 8, 16, 23, 40, 9, 163, DateTimeKind.Unspecified).AddTicks(3928), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 8, 25, 11, 41, 28, 273, DateTimeKind.Unspecified).AddTicks(5264), "Rosalee0@gmail.com", true, "Rosalee", true, null, "Muller", false, null, "ROSALEE0@GMAIL.COM", "ROSALEE_MULLER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Rosalee_Muller" },
                    { "a895c874-59d1-40ff-868c-9d9a4a7a1957", 0, new DateTime(1975, 11, 29, 5, 52, 13, 484, DateTimeKind.Unspecified).AddTicks(2035), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2018, 6, 3, 19, 14, 22, 407, DateTimeKind.Unspecified).AddTicks(5900), "Garry_Ryan@hotmail.com", false, "Garry", true, null, "Ryan", false, null, "GARRY_RYAN@HOTMAIL.COM", "GARRY_RYAN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Garry_Ryan" },
                    { "b5a7f7b0-a408-4bc2-b7a3-4ef6a04a30cd", 0, new DateTime(1988, 4, 5, 2, 54, 32, 425, DateTimeKind.Unspecified).AddTicks(3585), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2019, 8, 9, 9, 50, 14, 223, DateTimeKind.Unspecified).AddTicks(4152), "Sarina_Olson@gmail.com", true, "Sarina", false, null, "Olson", false, null, "SARINA_OLSON@GMAIL.COM", "SARINA_OLSON", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Sarina_Olson" },
                    { "de37d74b-d253-43da-b3b8-4330f6252df0", 0, new DateTime(1986, 12, 14, 1, 43, 41, 162, DateTimeKind.Unspecified).AddTicks(856), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2013, 5, 1, 20, 8, 56, 661, DateTimeKind.Unspecified).AddTicks(7879), "Meredith_Gleichner@gmail.com", true, "Meredith", false, null, "Gleichner", false, null, "MEREDITH_GLEICHNER@GMAIL.COM", "MEREDITH_GLEICHNER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Meredith_Gleichner" },
                    { "e411687a-67b5-47b7-bac5-443d814341f4", 0, new DateTime(1989, 7, 22, 10, 4, 24, 926, DateTimeKind.Unspecified).AddTicks(7982), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2010, 7, 14, 19, 45, 16, 505, DateTimeKind.Unspecified).AddTicks(155), "Austin_Brekke93@gmail.com", false, "Austin", true, null, "Brekke", false, null, "AUSTIN_BREKKE93@GMAIL.COM", "AUSTIN_BREKKE", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Austin_Brekke" },
                    { "e6bbcaf1-b10d-4635-8c31-0d68bb3736f3", 0, new DateTime(1999, 4, 21, 21, 10, 37, 989, DateTimeKind.Unspecified).AddTicks(9590), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2014, 10, 23, 5, 17, 10, 238, DateTimeKind.Unspecified).AddTicks(9798), "Orion_Sporer@yahoo.com", true, "Orion", false, null, "Sporer", false, null, "ORION_SPORER@YAHOO.COM", "ORION_SPORER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Orion_Sporer" },
                    { "e7c4be97-1121-481a-a6ca-c4a502a745c6", 0, new DateTime(1978, 1, 10, 12, 58, 41, 764, DateTimeKind.Unspecified).AddTicks(1551), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 6, 9, 5, 27, 40, 654, DateTimeKind.Unspecified).AddTicks(9094), "Viviane67@yahoo.com", false, "Viviane", false, null, "Fadel", false, null, "VIVIANE67@YAHOO.COM", "VIVIANE_FADEL", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Viviane_Fadel" },
                    { "e943557a-81af-457b-b9b6-9cd8fc273b28", 0, new DateTime(1990, 5, 14, 19, 42, 37, 595, DateTimeKind.Unspecified).AddTicks(5170), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2014, 2, 8, 16, 54, 35, 358, DateTimeKind.Unspecified).AddTicks(5384), "Laurence29@yahoo.com", false, "Laurence", true, null, "Abernathy", false, null, "LAURENCE29@YAHOO.COM", "LAURENCE_ABERNATHY", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Laurence_Abernathy" },
                    { "ecc02ce1-8fe0-41dc-8daa-52e6f6c8ccb3", 0, new DateTime(2000, 10, 30, 1, 44, 29, 435, DateTimeKind.Unspecified).AddTicks(968), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2014, 2, 7, 20, 7, 11, 161, DateTimeKind.Unspecified).AddTicks(9676), "Sunny22@hotmail.com", false, "Sunny", false, null, "Waters", false, null, "SUNNY22@HOTMAIL.COM", "SUNNY_WATERS", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Sunny_Waters" },
                    { "fba78b4e-ff35-4a74-b5e6-769a51948b5d", 0, new DateTime(1964, 6, 28, 9, 54, 41, 767, DateTimeKind.Unspecified).AddTicks(9018), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2011, 7, 19, 8, 42, 46, 888, DateTimeKind.Unspecified).AddTicks(6936), "Benton83@gmail.com", true, "Benton", true, null, "Osinski", false, null, "BENTON83@GMAIL.COM", "BENTON_OSINSKI", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Benton_Osinski" },
                    { "fdb2d0e6-60a1-47b1-b661-d68442ea4ad2", 0, new DateTime(1967, 1, 28, 7, 47, 45, 593, DateTimeKind.Unspecified).AddTicks(677), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2013, 9, 10, 2, 11, 23, 967, DateTimeKind.Unspecified).AddTicks(5331), "Jayne_Williamson@yahoo.com", true, "Jayne", false, null, "Williamson", false, null, "JAYNE_WILLIAMSON@YAHOO.COM", "JAYNE_WILLIAMSON", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Jayne_Williamson" },
                    { "ffba4dae-5045-4cfc-b6e9-8c2e4cd1e8f9", 0, new DateTime(1978, 12, 23, 6, 28, 34, 346, DateTimeKind.Unspecified).AddTicks(6695), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2018, 12, 6, 10, 30, 25, 864, DateTimeKind.Unspecified).AddTicks(5599), "Stephany92@yahoo.com", true, "Stephany", false, null, "Frami", false, null, "STEPHANY92@YAHOO.COM", "STEPHANY_FRAMI", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Stephany_Frami" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9813ad52-51e2-431b-8116-2e77fdf79b33", "0eee7038-6751-45d5-a434-3223ac0546f1" },
                    { "c2444b71-c794-4e42-8ec9-25a65ff3abeb", "1a7726f8-de89-465e-a761-6f7597a8d6bd" },
                    { "f6b5c6c9-3f5c-4a91-bc4d-a91825243fba", "1f9ff0c1-72d5-45b7-9150-b9bf5e4f7c12" },
                    { "f6b5c6c9-3f5c-4a91-bc4d-a91825243fba", "3bf4e986-0502-49f8-84d9-d841697995e5" },
                    { "c2444b71-c794-4e42-8ec9-25a65ff3abeb", "646cb87b-3078-40b7-9910-6f042aea6390" },
                    { "9813ad52-51e2-431b-8116-2e77fdf79b33", "76c92c3a-11df-4b2f-8e6a-0b3c88844b16" },
                    { "c2444b71-c794-4e42-8ec9-25a65ff3abeb", "7a085da8-d9da-48e1-ad98-c04afd1c52af" },
                    { "9709d04b-71b0-4874-9ca1-bc4e70e64298", "7fab3eab-31f1-46f4-9726-644c9b44f992" },
                    { "c2444b71-c794-4e42-8ec9-25a65ff3abeb", "9536000e-3ddc-452f-8d28-fb784442fdc8" },
                    { "f6b5c6c9-3f5c-4a91-bc4d-a91825243fba", "a895c874-59d1-40ff-868c-9d9a4a7a1957" },
                    { "9709d04b-71b0-4874-9ca1-bc4e70e64298", "b5a7f7b0-a408-4bc2-b7a3-4ef6a04a30cd" },
                    { "9709d04b-71b0-4874-9ca1-bc4e70e64298", "de37d74b-d253-43da-b3b8-4330f6252df0" },
                    { "9709d04b-71b0-4874-9ca1-bc4e70e64298", "e411687a-67b5-47b7-bac5-443d814341f4" },
                    { "f6b5c6c9-3f5c-4a91-bc4d-a91825243fba", "e6bbcaf1-b10d-4635-8c31-0d68bb3736f3" },
                    { "c2444b71-c794-4e42-8ec9-25a65ff3abeb", "e7c4be97-1121-481a-a6ca-c4a502a745c6" },
                    { "9709d04b-71b0-4874-9ca1-bc4e70e64298", "e943557a-81af-457b-b9b6-9cd8fc273b28" },
                    { "c2444b71-c794-4e42-8ec9-25a65ff3abeb", "ecc02ce1-8fe0-41dc-8daa-52e6f6c8ccb3" },
                    { "9813ad52-51e2-431b-8116-2e77fdf79b33", "fba78b4e-ff35-4a74-b5e6-769a51948b5d" },
                    { "9813ad52-51e2-431b-8116-2e77fdf79b33", "fdb2d0e6-60a1-47b1-b661-d68442ea4ad2" },
                    { "9709d04b-71b0-4874-9ca1-bc4e70e64298", "ffba4dae-5045-4cfc-b6e9-8c2e4cd1e8f9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
