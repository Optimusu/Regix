using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatient2DbTercera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Alcohol",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Caffeine",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DietaryPattern",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HousingSituation",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OccupationalHazards",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PhysicalActivity",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecreationalDrugs",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sleep",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tobacco",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alcohol",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Caffeine",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "DietaryPattern",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "HousingSituation",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "OccupationalHazards",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "PhysicalActivity",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "RecreationalDrugs",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Sleep",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Tobacco",
                table: "Patient2s");
        }
    }
}
