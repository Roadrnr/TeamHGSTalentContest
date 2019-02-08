using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHGSTalentContest.Data.Migrations
{
    public partial class AddSubmissionFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContestConsent",
                table: "Submissions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Submissions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContestConsent",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Submissions");
        }
    }
}
