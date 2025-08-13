using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class PharmaciesDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.PharmacyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PharmacyId",
                table: "Patients",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_Name",
                table: "Pharmacies",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Pharmacies_PharmacyId",
                table: "Patients",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "PharmacyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Pharmacies_PharmacyId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PharmacyId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Patients");
        }
    }
}
