using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHGSTalentContest.Data.Migrations
{
    public partial class FixSubmissionLocationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Locations_LocationsId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_LocationsId",
                table: "Submissions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85ef9f36-5e17-4382-9dcf-6305f8e5b4e6");

            migrationBuilder.DropColumn(
                name: "LocationsId",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Submissions",
                newName: "LocationId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d34fd983-ffff-4bb0-870a-44372686943d", "575e81ce-2c9f-445f-a6de-a140b2c1168e", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_LocationId",
                table: "Submissions",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Locations_LocationId",
                table: "Submissions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Locations_LocationId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_LocationId",
                table: "Submissions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d34fd983-ffff-4bb0-870a-44372686943d");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Submissions",
                newName: "Location");

            migrationBuilder.AddColumn<int>(
                name: "LocationsId",
                table: "Submissions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85ef9f36-5e17-4382-9dcf-6305f8e5b4e6", "8081ac75-de9e-4e01-a4aa-7fa446818e85", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_LocationsId",
                table: "Submissions",
                column: "LocationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Locations_LocationsId",
                table: "Submissions",
                column: "LocationsId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
