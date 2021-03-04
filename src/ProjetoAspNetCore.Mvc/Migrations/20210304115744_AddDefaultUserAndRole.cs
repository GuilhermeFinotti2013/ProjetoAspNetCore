using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoAspNetCore.Mvc.Migrations
{
    public partial class AddDefaultUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3EE387F4-ADBD-42BF-A068-022D48E99021", "536d4835-0e40-4bb7-9722-7b04aa1de525", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apelido", "ConcurrencyStamp", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NomeCompleto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "F6F2A61B-4B5A-4C9C-88C9-42A473B7958D", 0, "Gui", "7383c1d2-59b6-4652-9b8b-1981cb4150cd", new DateTime(1992, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "guilhermelfinotti@gmail.com", true, false, null, "Guilherme Luis Finotti", "GUILHERMELFINOTTI@GMAIL.COM", "GUILHERMELFINOTTI@GMAIL.COM", "AQAAAAEAACcQAAAAEJKUaPDHrlQKjda+w37NNKO2LN6zfH9jWE5tR2CTPU0kHlzK/JTLJAoB6JmcGEDqxA==", null, false, "b3defa25-3425-4214-88ea-8496b6387c7c", false, "guilhermelfinotti@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "F6F2A61B-4B5A-4C9C-88C9-42A473B7958D", "3EE387F4-ADBD-42BF-A068-022D48E99021" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "F6F2A61B-4B5A-4C9C-88C9-42A473B7958D", "3EE387F4-ADBD-42BF-A068-022D48E99021" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3EE387F4-ADBD-42BF-A068-022D48E99021");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "F6F2A61B-4B5A-4C9C-88C9-42A473B7958D");
        }
    }
}
