using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatien2DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Asthma",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AutoimmuneDisease",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Cancer",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CoagulationDisorder",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Diabetes",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Heartdisease",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hyperlipidemia",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hypertension",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InfectiousDiseases",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Kidneydisease",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LiverDisease",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherPersonalMedical",
                table: "Patient2s",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ThyroidDisorder",
                table: "Patient2s",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TypeCancer",
                table: "Patient2s",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Asthma",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "AutoimmuneDisease",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Cancer",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "CoagulationDisorder",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Diabetes",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Heartdisease",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Hyperlipidemia",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Hypertension",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "InfectiousDiseases",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "Kidneydisease",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "LiverDisease",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "OtherPersonalMedical",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "ThyroidDisorder",
                table: "Patient2s");

            migrationBuilder.DropColumn(
                name: "TypeCancer",
                table: "Patient2s");
        }
    }
}
