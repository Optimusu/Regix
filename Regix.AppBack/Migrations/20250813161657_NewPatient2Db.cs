using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class NewPatient2Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient2s",
                columns: table => new
                {
                    Patient2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultationReason = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SymptomLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PainQuality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PainSeverity = table.Column<int>(type: "int", nullable: false),
                    DurationPattern = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifyingFactors = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RelatedSymptoms = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PriorEpisodes = table.Column<bool>(type: "bit", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient2s", x => x.Patient2Id);
                    table.ForeignKey(
                        name: "FK_Patient2s_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient2s_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient2s_CorporationId",
                table: "Patient2s",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient2s_Patient2Id",
                table: "Patient2s",
                column: "Patient2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patient2s_PatientId",
                table: "Patient2s",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient2s");
        }
    }
}
