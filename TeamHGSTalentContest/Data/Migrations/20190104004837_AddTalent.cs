using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHGSTalentContest.Data.Migrations
{
    public partial class AddTalent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Talent",
                table: "Submissions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85ef9f36-5e17-4382-9dcf-6305f8e5b4e6", "8081ac75-de9e-4e01-a4aa-7fa446818e85", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85ef9f36-5e17-4382-9dcf-6305f8e5b4e6");

            migrationBuilder.DropColumn(
                name: "Talent",
                table: "Submissions");
        }
    }
}
