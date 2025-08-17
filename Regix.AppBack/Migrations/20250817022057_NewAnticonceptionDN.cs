using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Regix.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class NewAnticonceptionDN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anticonceptions",
                columns: table => new
                {
                    AnticonceptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anticonceptions", x => x.AnticonceptionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anticonceptions_Name",
                table: "Anticonceptions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anticonceptions");
        }
    }
}
