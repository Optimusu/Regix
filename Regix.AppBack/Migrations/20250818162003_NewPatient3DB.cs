using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class NewPatient3DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Discapacidads_DiscapacidadId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DiscapacidadId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DiscapacidadId",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "Patient3s",
                columns: table => new
                {
                    Patient3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PatientControlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MentalHealth = table.Column<bool>(type: "bit", nullable: false),
                    MentalDetail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrentTherapy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SuicidalIdeation = table.Column<bool>(type: "bit", nullable: false),
                    IdeationDetails = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phq9 = table.Column<bool>(type: "bit", nullable: false),
                    Gad7 = table.Column<bool>(type: "bit", nullable: false),
                    Cssrs = table.Column<bool>(type: "bit", nullable: false),
                    TestDetails = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MobilityAid = table.Column<bool>(type: "bit", nullable: false),
                    DailyAid = table.Column<bool>(type: "bit", nullable: false),
                    MobilityDetails = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pain = table.Column<int>(type: "int", nullable: false),
                    EtniaRazaId = table.Column<int>(type: "int", nullable: false),
                    DiscapacidadId = table.Column<int>(type: "int", nullable: false),
                    VeteranId = table.Column<int>(type: "int", nullable: false),
                    RecentTravel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExposureContagious = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient3s", x => x.Patient3Id);
                    table.ForeignKey(
                        name: "FK_Patient3s_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient3s_Discapacidads_DiscapacidadId",
                        column: x => x.DiscapacidadId,
                        principalTable: "Discapacidads",
                        principalColumn: "DiscapacidadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient3s_EtniaRazas_EtniaRazaId",
                        column: x => x.EtniaRazaId,
                        principalTable: "EtniaRazas",
                        principalColumn: "EtniaRazaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient3s_PatientControls_PatientControlId",
                        column: x => x.PatientControlId,
                        principalTable: "PatientControls",
                        principalColumn: "PatientControlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient3s_Veterans_VeteranId",
                        column: x => x.VeteranId,
                        principalTable: "Veterans",
                        principalColumn: "VeteranId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient3s_CorporationId",
                table: "Patient3s",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient3s_DiscapacidadId",
                table: "Patient3s",
                column: "DiscapacidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient3s_EtniaRazaId",
                table: "Patient3s",
                column: "EtniaRazaId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient3s_Patient3Id",
                table: "Patient3s",
                column: "Patient3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patient3s_PatientControlId",
                table: "Patient3s",
                column: "PatientControlId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient3s_VeteranId",
                table: "Patient3s",
                column: "VeteranId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient3s");

            migrationBuilder.AddColumn<int>(
                name: "DiscapacidadId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DiscapacidadId",
                table: "Patients",
                column: "DiscapacidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Discapacidads_DiscapacidadId",
                table: "Patients",
                column: "DiscapacidadId",
                principalTable: "Discapacidads",
                principalColumn: "DiscapacidadId");
        }
    }
}
