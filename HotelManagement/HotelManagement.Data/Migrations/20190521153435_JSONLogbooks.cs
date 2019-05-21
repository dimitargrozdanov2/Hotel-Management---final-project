using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelManagement.Data.Migrations
{
    public partial class JSONLogbooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Logbooks",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[] { "3d71d939-dc61-46f8-af46-ed6a618036c2", new DateTime(2019, 5, 4, 14, 40, 5, 0, DateTimeKind.Unspecified), false, null, "Hotel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Logbooks",
                keyColumn: "Id",
                keyValue: "3d71d939-dc61-46f8-af46-ed6a618036c2");
        }
    }
}
