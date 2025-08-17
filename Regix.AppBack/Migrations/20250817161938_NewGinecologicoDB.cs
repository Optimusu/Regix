using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class NewGinecologicoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ginecologicos",
                columns: table => new
                {
                    GinecologicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PatientControlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Menarquia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastMenstruation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegularId = table.Column<int>(type: "int", nullable: false),
                    AnticonceptionId = table.Column<int>(type: "int", nullable: false),
                    Gravida = table.Column<int>(type: "int", nullable: false),
                    LaborComplications = table.Column<bool>(type: "bit", nullable: false),
                    WhyComplication = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastCytologyPap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Menopause = table.Column<bool>(type: "bit", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ginecologicos", x => x.GinecologicoId);
                    table.ForeignKey(
                        name: "FK_Ginecologicos_Anticonceptions_AnticonceptionId",
                        column: x => x.AnticonceptionId,
                        principalTable: "Anticonceptions",
                        principalColumn: "AnticonceptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ginecologicos_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ginecologicos_PatientControls_PatientControlId",
                        column: x => x.PatientControlId,
                        principalTable: "PatientControls",
                        principalColumn: "PatientControlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ginecologicos_Regulars_RegularId",
                        column: x => x.RegularId,
                        principalTable: "Regulars",
                        principalColumn: "RegularId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ginecologicos_AnticonceptionId",
                table: "Ginecologicos",
                column: "AnticonceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ginecologicos_CorporationId",
                table: "Ginecologicos",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ginecologicos_GinecologicoId",
                table: "Ginecologicos",
                column: "GinecologicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ginecologicos_PatientControlId",
                table: "Ginecologicos",
                column: "PatientControlId");

            migrationBuilder.CreateIndex(
                name: "IX_Ginecologicos_RegularId",
                table: "Ginecologicos",
                column: "RegularId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ginecologicos");
        }
    }
}
