using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelManagement.Data.Migrations
{
    public partial class AddedJsonNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "IsDeleted", "LogbookId", "ModifiedOn", "Text", "UserId" },
                values: new object[] { "f7257688-84ea-4327-8841-ac78f3e8d2f6", "450b6f6e-95b3-400d-a258-aafc6b6ecd07", new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified), false, null, null, "Check reception documents!", "6404c00f-c0e6-4a92-ad71-43b24f5f0e97" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: "f7257688-84ea-4327-8841-ac78f3e8d2f6");
        }
    }
}
