using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHGSTalentContest.Data.Migrations
{
    public partial class AddOrderToRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Rules",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Rules");
        }
    }
}
