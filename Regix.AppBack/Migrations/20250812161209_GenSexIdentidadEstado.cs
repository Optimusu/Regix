using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class GenSexIdentidadEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoCivils",
                columns: table => new
                {
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCivils", x => x.EstadoCivilId);
                });

            migrationBuilder.CreateTable(
                name: "IdentidadGeneros",
                columns: table => new
                {
                    IdentidadGeneroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentidadGeneros", x => x.IdentidadGeneroId);
                });

            migrationBuilder.CreateTable(
                name: "SexoAsignados",
                columns: table => new
                {
                    SexoAsignadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SexoAsignados", x => x.SexoAsignadoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadoCivils_Name",
                table: "EstadoCivils",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentidadGeneros_Name",
                table: "IdentidadGeneros",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SexoAsignados_Name",
                table: "SexoAsignados",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoCivils");

            migrationBuilder.DropTable(
                name: "IdentidadGeneros");

            migrationBuilder.DropTable(
                name: "SexoAsignados");
        }
    }
}
