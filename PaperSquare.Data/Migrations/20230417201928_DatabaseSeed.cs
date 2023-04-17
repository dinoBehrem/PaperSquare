using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperSquare.Data.Migrations
{
    public partial class DatabaseSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3589c061-1c60-43ff-ae06-5da838058dee", "41126c3d-4044-4f0c-bc81-9d7f60389ac8", "RegisteredUser", "REGISTEREDUSER" },
                    { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "3f3c5af8-e9c3-45ba-87bd-9fc3e2b3f134", "Guest", "GUEST" },
                    { "4ded5da6-4674-41cf-a83c-74fe04d869bb", "a51dee12-0345-402c-8c9b-7b81147f6605", "Editor", "EDITOR" },
                    { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "75fabf07-07a8-4ed0-b480-bfb7953b5fd6", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "Firstname", "IsDeleted", "LastUpdated", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "085f3d5f-7d3e-4500-8709-400c19fd5810", 0, new DateTime(1971, 5, 24, 5, 17, 50, 710, DateTimeKind.Unspecified).AddTicks(5228), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2014, 9, 15, 17, 49, 4, 714, DateTimeKind.Unspecified).AddTicks(435), "Caleb76@yahoo.com", false, "Caleb", false, null, "Parisian", false, null, "CALEB76@YAHOO.COM", "CALEB_PARISIAN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Caleb_Parisian" },
                    { "1c06c32f-9e53-4144-a286-13ddc0ae70e4", 0, new DateTime(1993, 10, 19, 14, 38, 13, 769, DateTimeKind.Unspecified).AddTicks(2146), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 6, 16, 22, 37, 15, 292, DateTimeKind.Unspecified).AddTicks(6103), "Alessandra.Jones11@yahoo.com", false, "Alessandra", false, null, "Jones", false, null, "ALESSANDRA.JONES11@YAHOO.COM", "ALESSANDRA_JONES", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Alessandra_Jones" },
                    { "2550335c-068d-491a-ae46-8b0064199ea1", 0, new DateTime(1971, 12, 9, 1, 30, 8, 78, DateTimeKind.Unspecified).AddTicks(294), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2011, 10, 7, 17, 20, 12, 716, DateTimeKind.Unspecified).AddTicks(5722), "Jeffery_Hartmann24@hotmail.com", false, "Jeffery", true, null, "Hartmann", false, null, "JEFFERY_HARTMANN24@HOTMAIL.COM", "JEFFERY_HARTMANN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Jeffery_Hartmann" },
                    { "2bc3f791-5ae6-4380-98f5-861397b6a9bd", 0, new DateTime(1985, 10, 3, 15, 51, 48, 814, DateTimeKind.Unspecified).AddTicks(5077), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2012, 1, 24, 18, 9, 30, 252, DateTimeKind.Unspecified).AddTicks(7400), "Gloria.Kozey@yahoo.com", true, "Gloria", false, null, "Kozey", false, null, "GLORIA.KOZEY@YAHOO.COM", "GLORIA_KOZEY", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Gloria_Kozey" },
                    { "3cc1671c-1fe9-40af-bc37-9d5b848af786", 0, new DateTime(2000, 2, 11, 22, 12, 50, 213, DateTimeKind.Unspecified).AddTicks(1524), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2011, 2, 7, 9, 7, 49, 587, DateTimeKind.Unspecified).AddTicks(5866), "Cecelia26@yahoo.com", true, "Cecelia", false, null, "Towne", false, null, "CECELIA26@YAHOO.COM", "CECELIA_TOWNE", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Cecelia_Towne" },
                    { "3e5ab3a2-b627-437a-a13b-6b5cf131c911", 0, new DateTime(1972, 4, 8, 7, 41, 9, 465, DateTimeKind.Unspecified).AddTicks(100), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2018, 7, 30, 12, 53, 26, 125, DateTimeKind.Unspecified).AddTicks(9824), "Adah_Lockman@yahoo.com", true, "Adah", false, null, "Lockman", false, null, "ADAH_LOCKMAN@YAHOO.COM", "ADAH_LOCKMAN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Adah_Lockman" },
                    { "4382aafd-e084-4889-b4d4-23224fdd8cf0", 0, new DateTime(2000, 3, 30, 4, 50, 18, 451, DateTimeKind.Unspecified).AddTicks(2456), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2019, 4, 19, 1, 43, 50, 896, DateTimeKind.Unspecified).AddTicks(977), "Addie_Cummings@hotmail.com", false, "Addie", true, null, "Cummings", false, null, "ADDIE_CUMMINGS@HOTMAIL.COM", "ADDIE_CUMMINGS", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Addie_Cummings" },
                    { "48b69414-5fa3-4919-ae68-8c9dd2b5294a", 0, new DateTime(1997, 10, 1, 4, 52, 19, 838, DateTimeKind.Unspecified).AddTicks(2228), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2017, 6, 16, 5, 45, 4, 898, DateTimeKind.Unspecified).AddTicks(5348), "Elmer.Metz61@gmail.com", true, "Elmer", false, null, "Metz", false, null, "ELMER.METZ61@GMAIL.COM", "ELMER_METZ", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Elmer_Metz" },
                    { "5695cd4b-9832-4fab-8eef-497cb93bec68", 0, new DateTime(1992, 4, 20, 18, 50, 17, 490, DateTimeKind.Unspecified).AddTicks(6464), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 8, 6, 2, 37, 48, 501, DateTimeKind.Unspecified).AddTicks(4784), "Kelley_Boehm88@yahoo.com", false, "Kelley", false, null, "Boehm", false, null, "KELLEY_BOEHM88@YAHOO.COM", "KELLEY_BOEHM", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Kelley_Boehm" },
                    { "5f5a3af0-61e4-4289-b6c1-15542204c249", 0, new DateTime(1995, 8, 4, 7, 22, 14, 237, DateTimeKind.Unspecified).AddTicks(9722), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2010, 4, 18, 13, 53, 55, 789, DateTimeKind.Unspecified).AddTicks(912), "Anissa.Cruickshank@yahoo.com", true, "Anissa", false, null, "Cruickshank", false, null, "ANISSA.CRUICKSHANK@YAHOO.COM", "ANISSA_CRUICKSHANK", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Anissa_Cruickshank" },
                    { "964dd0dc-31cc-4a0a-b4a5-cdbb06893899", 0, new DateTime(1973, 6, 24, 15, 8, 38, 549, DateTimeKind.Unspecified).AddTicks(5328), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2019, 1, 22, 21, 51, 55, 602, DateTimeKind.Unspecified).AddTicks(272), "Blanca7@yahoo.com", true, "Blanca", false, null, "Green", false, null, "BLANCA7@YAHOO.COM", "BLANCA_GREEN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Blanca_Green" },
                    { "99b3115f-c93d-4b1a-a940-db6ee9057cb6", 0, new DateTime(1965, 12, 16, 4, 48, 39, 76, DateTimeKind.Unspecified).AddTicks(9217), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 10, 31, 6, 57, 27, 190, DateTimeKind.Unspecified).AddTicks(3310), "Joey60@yahoo.com", false, "Joey", false, null, "Ward", false, null, "JOEY60@YAHOO.COM", "JOEY_WARD", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Joey_Ward" },
                    { "a77a3b36-c3f4-4355-a388-4dd7ae96e6ea", 0, new DateTime(2000, 9, 10, 17, 48, 25, 857, DateTimeKind.Unspecified).AddTicks(8624), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 10, 16, 5, 20, 34, 909, DateTimeKind.Unspecified).AddTicks(1664), "Oceane_Hauck80@yahoo.com", true, "Oceane", true, null, "Hauck", false, null, "OCEANE_HAUCK80@YAHOO.COM", "OCEANE_HAUCK", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Oceane_Hauck" },
                    { "ae110ccb-ed7e-45cc-b707-8c376418ec85", 0, new DateTime(1982, 2, 14, 17, 28, 57, 50, DateTimeKind.Unspecified).AddTicks(2277), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2016, 6, 5, 3, 42, 38, 116, DateTimeKind.Unspecified).AddTicks(6896), "Brian.Barrows99@yahoo.com", false, "Brian", true, null, "Barrows", false, null, "BRIAN.BARROWS99@YAHOO.COM", "BRIAN_BARROWS", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Brian_Barrows" },
                    { "c0dfc08e-a6d1-4700-9784-c60d8f1efdbb", 0, new DateTime(1978, 2, 1, 18, 47, 51, 457, DateTimeKind.Unspecified).AddTicks(2742), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2018, 6, 18, 18, 28, 34, 41, DateTimeKind.Unspecified).AddTicks(4941), "Opal_Herzog46@hotmail.com", true, "Opal", false, null, "Herzog", false, null, "OPAL_HERZOG46@HOTMAIL.COM", "OPAL_HERZOG", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Opal_Herzog" },
                    { "ca500cf5-44ba-4cc0-85c7-58dd000c252a", 0, new DateTime(1960, 1, 28, 2, 49, 8, 375, DateTimeKind.Unspecified).AddTicks(237), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 7, 17, 15, 56, 32, 700, DateTimeKind.Unspecified).AddTicks(7782), "Jordi_Rutherford@yahoo.com", true, "Jordi", true, null, "Rutherford", false, null, "JORDI_RUTHERFORD@YAHOO.COM", "JORDI_RUTHERFORD", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Jordi_Rutherford" },
                    { "cf33fd83-c484-4fb4-9b73-a85c2a20e232", 0, new DateTime(1993, 6, 2, 15, 51, 3, 566, DateTimeKind.Unspecified).AddTicks(7650), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2018, 1, 21, 1, 37, 19, 771, DateTimeKind.Unspecified).AddTicks(8484), "Hank_Altenwerth97@gmail.com", true, "Hank", true, null, "Altenwerth", false, null, "HANK_ALTENWERTH97@GMAIL.COM", "HANK_ALTENWERTH", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Hank_Altenwerth" },
                    { "ebd178de-f3a2-4d18-8cc5-07d594f22e3e", 0, new DateTime(1973, 5, 7, 2, 21, 23, 613, DateTimeKind.Unspecified).AddTicks(4596), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2013, 3, 31, 22, 9, 43, 932, DateTimeKind.Unspecified).AddTicks(3673), "Vida73@hotmail.com", false, "Vida", true, null, "Cremin", false, null, "VIDA73@HOTMAIL.COM", "VIDA_CREMIN", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Vida_Cremin" },
                    { "f1e6b85f-1dab-4efa-b735-71bdf38963e0", 0, new DateTime(1994, 11, 17, 15, 31, 28, 611, DateTimeKind.Unspecified).AddTicks(4040), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2015, 2, 11, 20, 53, 17, 528, DateTimeKind.Unspecified).AddTicks(8472), "Wilson.Renner56@hotmail.com", true, "Wilson", true, null, "Renner", false, null, "WILSON.RENNER56@HOTMAIL.COM", "WILSON_RENNER", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", true, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Wilson_Renner" },
                    { "f7bc439c-8551-45fd-aba9-7d99b4219f36", 0, new DateTime(1978, 7, 25, 6, 14, 18, 825, DateTimeKind.Unspecified).AddTicks(9913), "efc45564-59cd-4bcc-a3cd-265b3cb5b6ce", new DateTime(2016, 7, 29, 7, 18, 53, 218, DateTimeKind.Unspecified).AddTicks(1106), "Gunner_Hand55@yahoo.com", true, "Gunner", false, null, "Hand", false, null, "GUNNER_HAND55@YAHOO.COM", "GUNNER_HAND", "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==", "Bogus.DataSets.PhoneNumbers", false, "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN", false, "Gunner_Hand" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "085f3d5f-7d3e-4500-8709-400c19fd5810" },
                    { "3589c061-1c60-43ff-ae06-5da838058dee", "1c06c32f-9e53-4144-a286-13ddc0ae70e4" },
                    { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "2550335c-068d-491a-ae46-8b0064199ea1" },
                    { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "2bc3f791-5ae6-4380-98f5-861397b6a9bd" },
                    { "3589c061-1c60-43ff-ae06-5da838058dee", "3cc1671c-1fe9-40af-bc37-9d5b848af786" },
                    { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "3e5ab3a2-b627-437a-a13b-6b5cf131c911" },
                    { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "4382aafd-e084-4889-b4d4-23224fdd8cf0" },
                    { "4ded5da6-4674-41cf-a83c-74fe04d869bb", "48b69414-5fa3-4919-ae68-8c9dd2b5294a" },
                    { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "5695cd4b-9832-4fab-8eef-497cb93bec68" },
                    { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "5f5a3af0-61e4-4289-b6c1-15542204c249" },
                    { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "964dd0dc-31cc-4a0a-b4a5-cdbb06893899" },
                    { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "99b3115f-c93d-4b1a-a940-db6ee9057cb6" },
                    { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "a77a3b36-c3f4-4355-a388-4dd7ae96e6ea" },
                    { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "ae110ccb-ed7e-45cc-b707-8c376418ec85" },
                    { "4ded5da6-4674-41cf-a83c-74fe04d869bb", "c0dfc08e-a6d1-4700-9784-c60d8f1efdbb" },
                    { "3589c061-1c60-43ff-ae06-5da838058dee", "ca500cf5-44ba-4cc0-85c7-58dd000c252a" },
                    { "3589c061-1c60-43ff-ae06-5da838058dee", "cf33fd83-c484-4fb4-9b73-a85c2a20e232" },
                    { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "ebd178de-f3a2-4d18-8cc5-07d594f22e3e" },
                    { "4ded5da6-4674-41cf-a83c-74fe04d869bb", "f1e6b85f-1dab-4efa-b735-71bdf38963e0" },
                    { "3589c061-1c60-43ff-ae06-5da838058dee", "f7bc439c-8551-45fd-aba9-7d99b4219f36" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "085f3d5f-7d3e-4500-8709-400c19fd5810" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3589c061-1c60-43ff-ae06-5da838058dee", "1c06c32f-9e53-4144-a286-13ddc0ae70e4" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "2550335c-068d-491a-ae46-8b0064199ea1" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "2bc3f791-5ae6-4380-98f5-861397b6a9bd" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3589c061-1c60-43ff-ae06-5da838058dee", "3cc1671c-1fe9-40af-bc37-9d5b848af786" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "3e5ab3a2-b627-437a-a13b-6b5cf131c911" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "4382aafd-e084-4889-b4d4-23224fdd8cf0" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4ded5da6-4674-41cf-a83c-74fe04d869bb", "48b69414-5fa3-4919-ae68-8c9dd2b5294a" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "5695cd4b-9832-4fab-8eef-497cb93bec68" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "5f5a3af0-61e4-4289-b6c1-15542204c249" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "964dd0dc-31cc-4a0a-b4a5-cdbb06893899" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "99b3115f-c93d-4b1a-a940-db6ee9057cb6" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "a77a3b36-c3f4-4355-a388-4dd7ae96e6ea" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f5e5a7f-cff6-49a8-b615-9391572b313c", "ae110ccb-ed7e-45cc-b707-8c376418ec85" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4ded5da6-4674-41cf-a83c-74fe04d869bb", "c0dfc08e-a6d1-4700-9784-c60d8f1efdbb" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3589c061-1c60-43ff-ae06-5da838058dee", "ca500cf5-44ba-4cc0-85c7-58dd000c252a" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3589c061-1c60-43ff-ae06-5da838058dee", "cf33fd83-c484-4fb4-9b73-a85c2a20e232" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "642e7f53-0381-4651-833b-f0d9d53d0e4d", "ebd178de-f3a2-4d18-8cc5-07d594f22e3e" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4ded5da6-4674-41cf-a83c-74fe04d869bb", "f1e6b85f-1dab-4efa-b735-71bdf38963e0" });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3589c061-1c60-43ff-ae06-5da838058dee", "f7bc439c-8551-45fd-aba9-7d99b4219f36" });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "3589c061-1c60-43ff-ae06-5da838058dee");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "3f5e5a7f-cff6-49a8-b615-9391572b313c");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "4ded5da6-4674-41cf-a83c-74fe04d869bb");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "642e7f53-0381-4651-833b-f0d9d53d0e4d");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "085f3d5f-7d3e-4500-8709-400c19fd5810");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "1c06c32f-9e53-4144-a286-13ddc0ae70e4");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2550335c-068d-491a-ae46-8b0064199ea1");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "2bc3f791-5ae6-4380-98f5-861397b6a9bd");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3cc1671c-1fe9-40af-bc37-9d5b848af786");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3e5ab3a2-b627-437a-a13b-6b5cf131c911");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "4382aafd-e084-4889-b4d4-23224fdd8cf0");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "48b69414-5fa3-4919-ae68-8c9dd2b5294a");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5695cd4b-9832-4fab-8eef-497cb93bec68");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5f5a3af0-61e4-4289-b6c1-15542204c249");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "964dd0dc-31cc-4a0a-b4a5-cdbb06893899");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "99b3115f-c93d-4b1a-a940-db6ee9057cb6");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a77a3b36-c3f4-4355-a388-4dd7ae96e6ea");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ae110ccb-ed7e-45cc-b707-8c376418ec85");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "c0dfc08e-a6d1-4700-9784-c60d8f1efdbb");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ca500cf5-44ba-4cc0-85c7-58dd000c252a");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "cf33fd83-c484-4fb4-9b73-a85c2a20e232");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ebd178de-f3a2-4d18-8cc5-07d594f22e3e");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f1e6b85f-1dab-4efa-b735-71bdf38963e0");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f7bc439c-8551-45fd-aba9-7d99b4219f36");
        }
    }
}
