using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaperSquare.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
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
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "80f14719-acd3-487f-a17a-3e97db092127", "RegisteredUser", "REGISTEREDUSER" },
                    { "4a2b8180-45a0-4e10-89b6-3b05d0bd24d3", "0927d8c6-497b-465f-99de-ed4d9b087b6f", "Admin", "ADMIN" },
                    { "725aa7b5-55e2-43c7-b6da-d7145034fa1e", "74558331-fe58-4274-bc55-9589cba71030", "Guest", "GUEST" },
                    { "7ac91a34-fe1c-40f3-bbff-cd0398babd7e", "e06a2d1d-8234-4496-a2c0-fa5f3dc48264", "Editor", "EDITOR" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreatedBy", "CreatedOnUtc", "Email", "EmailConfirmed", "Firstname", "IsDeleted", "LastModifiedBy", "LastModifiedOnUtc", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00be9f04-6a57-4660-a5fc-0fa38bc29a25", 0, new DateTime(1986, 11, 2, 12, 15, 21, 69, DateTimeKind.Unspecified).AddTicks(2931), "df8952c3-a89f-4b60-81b5-c172089eab43", "00be9f04-6a57-4660-a5fc-0fa38bc29a25", new DateTime(2019, 8, 24, 11, 28, 3, 681, DateTimeKind.Unspecified).AddTicks(1923), "Quinn_Muller30@yahoo.com", true, "Quinn", false, null, null, "Muller", false, null, "QUINN_MULLER30@YAHOO.COM", "QUINN_MULLER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "(892) 995-3907", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Quinn_Muller" },
                    { "06eae39b-2ea3-49e1-8146-994494849ba4", 0, new DateTime(1995, 5, 23, 19, 21, 19, 884, DateTimeKind.Unspecified).AddTicks(186), "37e2317f-f8dc-4b5c-8bfe-75117d362820", "06eae39b-2ea3-49e1-8146-994494849ba4", new DateTime(2012, 9, 27, 14, 47, 4, 185, DateTimeKind.Unspecified).AddTicks(6192), "Geovanni_Champlin@hotmail.com", false, "Geovanni", true, null, null, "Champlin", false, null, "GEOVANNI_CHAMPLIN@HOTMAIL.COM", "GEOVANNI_CHAMPLIN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "801-687-1293", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Geovanni_Champlin" },
                    { "18676753-b673-40ae-ac7f-6a6ddd5613d3", 0, new DateTime(1980, 1, 5, 0, 39, 58, 328, DateTimeKind.Unspecified).AddTicks(6962), "819675bb-aee5-4e97-b221-b0daf17c9992", "18676753-b673-40ae-ac7f-6a6ddd5613d3", new DateTime(2015, 4, 8, 21, 44, 49, 889, DateTimeKind.Unspecified).AddTicks(1860), "Tressie.Koelpin@yahoo.com", true, "Tressie", false, null, null, "Koelpin", false, null, "TRESSIE.KOELPIN@YAHOO.COM", "TRESSIE_KOELPIN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "451-908-1307 x62474", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Tressie_Koelpin" },
                    { "202d843a-6a9a-4c96-893e-762a11756399", 0, new DateTime(1972, 2, 29, 7, 36, 37, 248, DateTimeKind.Unspecified).AddTicks(770), "84626275-0eaf-4018-b012-40aa6b3a0b75", "202d843a-6a9a-4c96-893e-762a11756399", new DateTime(2012, 8, 7, 9, 51, 27, 428, DateTimeKind.Unspecified).AddTicks(6222), "Leda.Hettinger67@gmail.com", false, "Leda", false, null, null, "Hettinger", false, null, "LEDA.HETTINGER67@GMAIL.COM", "LEDA_HETTINGER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "924-498-7012 x827", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Leda_Hettinger" },
                    { "2afa61a5-be9e-4990-a674-7f43e3086205", 0, new DateTime(1999, 5, 9, 8, 47, 0, 429, DateTimeKind.Unspecified).AddTicks(4748), "7b17067c-dd61-43ec-ae38-d712641ca617", "2afa61a5-be9e-4990-a674-7f43e3086205", new DateTime(2010, 2, 21, 19, 48, 58, 595, DateTimeKind.Unspecified).AddTicks(6249), "Lorenz_Jast32@hotmail.com", false, "Lorenz", false, null, null, "Jast", false, null, "LORENZ_JAST32@HOTMAIL.COM", "LORENZ_JAST", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "347.739.9578 x2930", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Lorenz_Jast" },
                    { "2f83e937-b2a6-4825-9797-67bc19eccf2f", 0, new DateTime(1980, 3, 29, 4, 17, 3, 612, DateTimeKind.Unspecified).AddTicks(4331), "7caa62fa-54a3-4727-9ff0-1464a3bc76c2", "2f83e937-b2a6-4825-9797-67bc19eccf2f", new DateTime(2012, 1, 7, 23, 2, 17, 127, DateTimeKind.Unspecified).AddTicks(7511), "Vicente70@hotmail.com", false, "Vicente", false, null, null, "Hermann", false, null, "VICENTE70@HOTMAIL.COM", "VICENTE_HERMANN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "789.873.3543 x07703", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Vicente_Hermann" },
                    { "52dfcc62-0bc5-4ec6-b3a3-06b6447d6c6e", 0, new DateTime(1961, 10, 22, 12, 53, 59, 850, DateTimeKind.Unspecified).AddTicks(9066), "faad7601-960c-4c55-985b-87015d47cc41", "52dfcc62-0bc5-4ec6-b3a3-06b6447d6c6e", new DateTime(2015, 3, 6, 11, 59, 26, 698, DateTimeKind.Unspecified).AddTicks(6345), "Laverne9@gmail.com", true, "Laverne", false, null, null, "Lang", false, null, "LAVERNE9@GMAIL.COM", "LAVERNE_LANG", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "528.695.1084 x53621", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Laverne_Lang" },
                    { "64d3a57c-5406-4c97-8879-33106d72c252", 0, new DateTime(1979, 4, 26, 23, 22, 6, 337, DateTimeKind.Unspecified).AddTicks(2684), "617bda41-a64e-4f87-871b-371243cd6ecf", "64d3a57c-5406-4c97-8879-33106d72c252", new DateTime(2017, 4, 22, 16, 29, 24, 924, DateTimeKind.Unspecified).AddTicks(4986), "Kirk.Kozey@gmail.com", false, "Kirk", true, null, null, "Kozey", false, null, "KIRK.KOZEY@GMAIL.COM", "KIRK_KOZEY", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "(923) 734-7096 x794", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Kirk_Kozey" },
                    { "761d6858-ea3b-420f-bbc5-cd72c60d7319", 0, new DateTime(1982, 11, 21, 18, 40, 36, 407, DateTimeKind.Unspecified).AddTicks(8046), "4bc760d2-09d4-42db-a656-4381a965f666", "761d6858-ea3b-420f-bbc5-cd72c60d7319", new DateTime(2013, 1, 24, 5, 26, 20, 103, DateTimeKind.Unspecified).AddTicks(9236), "Godfrey_Ryan21@gmail.com", true, "Godfrey", false, null, null, "Ryan", false, null, "GODFREY_RYAN21@GMAIL.COM", "GODFREY_RYAN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "1-743-894-2795", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Godfrey_Ryan" },
                    { "84b81c0d-16cd-4d89-8d2d-dd2b10a5fac5", 0, new DateTime(1980, 3, 21, 23, 10, 40, 634, DateTimeKind.Unspecified).AddTicks(731), "de37e744-62e0-4517-8914-a34a6f1855ac", "84b81c0d-16cd-4d89-8d2d-dd2b10a5fac5", new DateTime(2016, 4, 29, 20, 39, 21, 567, DateTimeKind.Unspecified).AddTicks(693), "Laurie.Stokes@gmail.com", false, "Laurie", false, null, null, "Stokes", false, null, "LAURIE.STOKES@GMAIL.COM", "LAURIE_STOKES", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "(475) 464-3766", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Laurie_Stokes" },
                    { "866f9e09-c4d5-4dd1-b463-1af21a96d2ff", 0, new DateTime(1979, 8, 22, 18, 9, 27, 106, DateTimeKind.Unspecified).AddTicks(7970), "77a2fdb6-6ec2-471d-bb31-35919f93951a", "866f9e09-c4d5-4dd1-b463-1af21a96d2ff", new DateTime(2014, 10, 16, 4, 8, 57, 118, DateTimeKind.Unspecified).AddTicks(185), "Gregory_Russel3@yahoo.com", true, "Gregory", false, null, null, "Russel", false, null, "GREGORY_RUSSEL3@YAHOO.COM", "GREGORY_RUSSEL", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "1-216-599-3125", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Gregory_Russel" },
                    { "8f6d985b-f0e2-42ec-b9f1-3e7b58ae89fb", 0, new DateTime(1982, 9, 3, 9, 31, 25, 771, DateTimeKind.Unspecified).AddTicks(4212), "03b3b0d1-2565-4879-9170-1b757a09c109", "8f6d985b-f0e2-42ec-b9f1-3e7b58ae89fb", new DateTime(2017, 9, 22, 14, 41, 47, 550, DateTimeKind.Unspecified).AddTicks(6258), "Seamus.Tillman@gmail.com", false, "Seamus", true, null, null, "Tillman", false, null, "SEAMUS.TILLMAN@GMAIL.COM", "SEAMUS_TILLMAN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "755-932-3708 x219", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Seamus_Tillman" },
                    { "98f3cdca-4230-4e42-a014-2b29091d5b68", 0, new DateTime(1991, 4, 14, 13, 44, 13, 674, DateTimeKind.Unspecified).AddTicks(6764), "261f29d5-763c-49b6-a4a7-32aaf414bb03", "98f3cdca-4230-4e42-a014-2b29091d5b68", new DateTime(2018, 1, 2, 23, 6, 51, 418, DateTimeKind.Unspecified).AddTicks(256), "Narciso.Greenholt14@hotmail.com", true, "Narciso", false, null, null, "Greenholt", false, null, "NARCISO.GREENHOLT14@HOTMAIL.COM", "NARCISO_GREENHOLT", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "200.390.8109", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Narciso_Greenholt" },
                    { "a4ab6114-115a-4c08-9672-950b8d7fd91d", 0, new DateTime(1976, 12, 17, 14, 19, 24, 779, DateTimeKind.Unspecified).AddTicks(5153), "1df66b31-be98-4eb5-8928-b2ec7a722853", "a4ab6114-115a-4c08-9672-950b8d7fd91d", new DateTime(2012, 3, 16, 18, 50, 11, 608, DateTimeKind.Unspecified).AddTicks(8171), "Thomas.Casper31@hotmail.com", true, "Thomas", true, null, null, "Casper", false, null, "THOMAS.CASPER31@HOTMAIL.COM", "THOMAS_CASPER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "1-740-252-6607 x63056", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Thomas_Casper" },
                    { "b31ce142-2ace-489e-a39a-360f193ee8da", 0, new DateTime(1964, 5, 4, 7, 2, 5, 82, DateTimeKind.Unspecified).AddTicks(9749), "d20951f5-70d0-4b7c-bb10-a1fc98fd22d1", "b31ce142-2ace-489e-a39a-360f193ee8da", new DateTime(2016, 4, 25, 7, 44, 43, 584, DateTimeKind.Unspecified).AddTicks(9476), "Danika25@hotmail.com", false, "Danika", true, null, null, "Parker", false, null, "DANIKA25@HOTMAIL.COM", "DANIKA_PARKER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "810.756.4778 x963", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Danika_Parker" },
                    { "c0d55b02-9c55-46ba-afd8-3a54a997a8eb", 0, new DateTime(1989, 11, 23, 6, 28, 0, 865, DateTimeKind.Unspecified).AddTicks(1418), "3d8eb46e-5750-4a28-8eb8-b6b8d922d920", "c0d55b02-9c55-46ba-afd8-3a54a997a8eb", new DateTime(2015, 9, 26, 21, 20, 5, 95, DateTimeKind.Unspecified).AddTicks(5965), "Tillman48@hotmail.com", false, "Tillman", false, null, null, "Bednar", false, null, "TILLMAN48@HOTMAIL.COM", "TILLMAN_BEDNAR", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "301-842-9546", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Tillman_Bednar" },
                    { "c850f80a-9226-44ed-8880-a8bace6c43ab", 0, new DateTime(1979, 7, 22, 20, 48, 36, 969, DateTimeKind.Unspecified).AddTicks(2761), "f1d51ff1-be5a-422c-8dc0-ab3f616632a8", "c850f80a-9226-44ed-8880-a8bace6c43ab", new DateTime(2018, 8, 10, 21, 40, 51, 795, DateTimeKind.Unspecified).AddTicks(4098), "Zane_Schulist@yahoo.com", false, "Zane", false, null, null, "Schulist", false, null, "ZANE_SCHULIST@YAHOO.COM", "ZANE_SCHULIST", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "(332) 478-6802 x2135", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Zane_Schulist" },
                    { "e09844ec-0d11-441e-8021-d3cbe4d21bbc", 0, new DateTime(1986, 1, 9, 3, 7, 33, 863, DateTimeKind.Unspecified).AddTicks(8434), "2b1ffe61-361f-418c-91eb-c28120ecb1f5", "e09844ec-0d11-441e-8021-d3cbe4d21bbc", new DateTime(2018, 11, 2, 23, 39, 59, 170, DateTimeKind.Unspecified).AddTicks(2258), "Raphaelle_Blick@gmail.com", false, "Raphaelle", true, null, null, "Blick", false, null, "RAPHAELLE_BLICK@GMAIL.COM", "RAPHAELLE_BLICK", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "864.571.4257", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Raphaelle_Blick" },
                    { "e5ef12ab-1f71-44a1-873a-6646b9c13091", 0, new DateTime(1962, 7, 14, 23, 43, 50, 794, DateTimeKind.Unspecified).AddTicks(4390), "84f07b8d-25cd-47c5-af06-8e6ffeee5ca7", "e5ef12ab-1f71-44a1-873a-6646b9c13091", new DateTime(2015, 8, 12, 22, 27, 10, 376, DateTimeKind.Unspecified).AddTicks(2270), "Art74@yahoo.com", true, "Art", false, null, null, "Kilback", false, null, "ART74@YAHOO.COM", "ART_KILBACK", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "1-761-555-7579 x0384", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Art_Kilback" },
                    { "fa8f4aab-b715-46d2-9c17-882616769b46", 0, new DateTime(1960, 6, 1, 14, 34, 7, 929, DateTimeKind.Unspecified).AddTicks(1229), "7b16026c-d329-4efe-b2f7-5e5f84945ec4", "fa8f4aab-b715-46d2-9c17-882616769b46", new DateTime(2012, 2, 13, 2, 40, 3, 555, DateTimeKind.Unspecified).AddTicks(2609), "Alda.Dooley@yahoo.com", false, "Alda", true, null, null, "Dooley", false, null, "ALDA.DOOLEY@YAHOO.COM", "ALDA_DOOLEY", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "(416) 587-3585 x7825", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Alda_Dooley" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7ac91a34-fe1c-40f3-bbff-cd0398babd7e", "00be9f04-6a57-4660-a5fc-0fa38bc29a25" },
                    { "7ac91a34-fe1c-40f3-bbff-cd0398babd7e", "06eae39b-2ea3-49e1-8146-994494849ba4" },
                    { "725aa7b5-55e2-43c7-b6da-d7145034fa1e", "18676753-b673-40ae-ac7f-6a6ddd5613d3" },
                    { "725aa7b5-55e2-43c7-b6da-d7145034fa1e", "202d843a-6a9a-4c96-893e-762a11756399" },
                    { "4a2b8180-45a0-4e10-89b6-3b05d0bd24d3", "2afa61a5-be9e-4990-a674-7f43e3086205" },
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "2f83e937-b2a6-4825-9797-67bc19eccf2f" },
                    { "7ac91a34-fe1c-40f3-bbff-cd0398babd7e", "52dfcc62-0bc5-4ec6-b3a3-06b6447d6c6e" },
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "64d3a57c-5406-4c97-8879-33106d72c252" },
                    { "4a2b8180-45a0-4e10-89b6-3b05d0bd24d3", "761d6858-ea3b-420f-bbc5-cd72c60d7319" },
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "84b81c0d-16cd-4d89-8d2d-dd2b10a5fac5" },
                    { "4a2b8180-45a0-4e10-89b6-3b05d0bd24d3", "866f9e09-c4d5-4dd1-b463-1af21a96d2ff" },
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "8f6d985b-f0e2-42ec-b9f1-3e7b58ae89fb" },
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "98f3cdca-4230-4e42-a014-2b29091d5b68" },
                    { "4a2b8180-45a0-4e10-89b6-3b05d0bd24d3", "a4ab6114-115a-4c08-9672-950b8d7fd91d" },
                    { "4a2b8180-45a0-4e10-89b6-3b05d0bd24d3", "b31ce142-2ace-489e-a39a-360f193ee8da" },
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "c0d55b02-9c55-46ba-afd8-3a54a997a8eb" },
                    { "7ac91a34-fe1c-40f3-bbff-cd0398babd7e", "c850f80a-9226-44ed-8880-a8bace6c43ab" },
                    { "185bee48-25e7-49a8-8e4d-9764d76b2347", "e09844ec-0d11-441e-8021-d3cbe4d21bbc" },
                    { "7ac91a34-fe1c-40f3-bbff-cd0398babd7e", "e5ef12ab-1f71-44a1-873a-6646b9c13091" },
                    { "725aa7b5-55e2-43c7-b6da-d7145034fa1e", "fa8f4aab-b715-46d2-9c17-882616769b46" }
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
