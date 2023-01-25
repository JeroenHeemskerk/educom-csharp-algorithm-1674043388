using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BornToMove.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CompleteSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "move",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Push up");

            migrationBuilder.UpdateData(
                table: "move",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Planking");

            migrationBuilder.UpdateData(
                table: "move",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Squat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "move",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "");

            migrationBuilder.UpdateData(
                table: "move",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "");

            migrationBuilder.UpdateData(
                table: "move",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "");
        }
    }
}
