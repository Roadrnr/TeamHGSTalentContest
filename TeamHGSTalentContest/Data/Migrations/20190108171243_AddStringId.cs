using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamHGSTalentContest.Data.Migrations
{
    public partial class AddStringId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d34fd983-ffff-4bb0-870a-44372686943d");

            migrationBuilder.AddColumn<Guid>(
                name: "StringId",
                table: "Submissions",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.AddColumn<Guid>(
                name: "StringId",
                table: "Locations",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Submissions_StringId",
                table: "Submissions",
                column: "StringId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Locations_StringId",
                table: "Locations",
                column: "StringId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a605abd0-0140-4acd-b324-643c404dfb9e", "7f08f385-65d7-43c6-a275-9d9f03df8b1c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Submissions_StringId",
                table: "Submissions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Locations_StringId",
                table: "Locations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a605abd0-0140-4acd-b324-643c404dfb9e");

            migrationBuilder.DropColumn(
                name: "StringId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "StringId",
                table: "Locations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d34fd983-ffff-4bb0-870a-44372686943d", "575e81ce-2c9f-445f-a6de-a140b2c1168e", "Admin", "ADMIN" });
        }
    }
}
