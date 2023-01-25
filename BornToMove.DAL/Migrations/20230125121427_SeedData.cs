using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BornToMove.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "move",
                columns: new[] { "Id", "Description", "Name", "SweatRate" },
                values: new object[,]
                {
                    { 1, "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes.", "", 3 },
                    { 2, "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast.", "", 3 },
                    { 3, "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes.", "", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "move",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "move",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "move",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
