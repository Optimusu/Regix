using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class PatientDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(101)", maxLength: 101, nullable: true),
                    Preferido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SexoAsignadoId = table.Column<int>(type: "int", nullable: false),
                    IdentidadGeneroId = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Nro_Document = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneCell = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PhoneHome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IdiomaId = table.Column<int>(type: "int", nullable: false),
                    Interpreter = table.Column<bool>(type: "bit", nullable: false),
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false),
                    Ocupacion = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    Migrated = table.Column<bool>(type: "bit", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_EstadoCivils_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalTable: "EstadoCivils",
                        principalColumn: "EstadoCivilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_IdentidadGeneros_IdentidadGeneroId",
                        column: x => x.IdentidadGeneroId,
                        principalTable: "IdentidadGeneros",
                        principalColumn: "IdentidadGeneroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Idiomas_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idiomas",
                        principalColumn: "IdiomaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_SexoAsignados_SexoAsignadoId",
                        column: x => x.SexoAsignadoId,
                        principalTable: "SexoAsignados",
                        principalColumn: "SexoAsignadoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CorporationId",
                table: "Patients",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DocumentTypeId",
                table: "Patients",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_EstadoCivilId",
                table: "Patients",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FullName_CorporationId",
                table: "Patients",
                columns: new[] { "FullName", "CorporationId" },
                unique: true,
                filter: "[FullName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IdentidadGeneroId",
                table: "Patients",
                column: "IdentidadGeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IdiomaId",
                table: "Patients",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientId",
                table: "Patients",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_SexoAsignadoId",
                table: "Patients",
                column: "SexoAsignadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserName",
                table: "Patients",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
