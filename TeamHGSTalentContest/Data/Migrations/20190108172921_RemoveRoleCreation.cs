using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHGSTalentContest.Data.Migrations
{
    public partial class RemoveRoleCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a605abd0-0140-4acd-b324-643c404dfb9e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a605abd0-0140-4acd-b324-643c404dfb9e", "7f08f385-65d7-43c6-a275-9d9f03df8b1c", "Admin", "ADMIN" });
        }
    }
}
